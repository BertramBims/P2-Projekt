using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMobilityController : MonoBehaviour
{
    [Header("Movement...")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    private float leftInput;
    private float rightInput;

    private Rigidbody2D rb;
    private Animator animator;

    private float forward;

    [SerializeField] private Transform visual;
    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void OnLeftWheel(InputAction.CallbackContext ctx)
    {
        leftInput = ctx.ReadValue<Vector2>().y;
    }

    public void OnRightWheel(InputAction.CallbackContext ctx)
    {
        rightInput = ctx.ReadValue<Vector2>().y;
    }

    private void Update()
    {
        Vector2 velocity = rb.linearVelocity;
        Vector2 direction = velocity.normalized;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        int directionIndex = Mathf.RoundToInt(angle / 45f);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void LateUpdate()
    {
        visual.rotation = Quaternion.identity;
        cameraTransform.rotation = Quaternion.identity;
    }

    void HandleMovement()
    {
        //forward / backward movement = average of both wheels
        forward = (leftInput + rightInput) / 2;

        //rotation = difference between wheels
        float rotation = (leftInput - rightInput);

        //move forward / backward
        Vector2 movement = transform.up * forward * moveSpeed;
        rb.linearVelocity = movement;

        //rotate
        rb.angularVelocity = -rotation * rotationSpeed;
    }

    void HandleAnimation()
    {
        Vector2 velocity = rb.linearVelocity;

        if (velocity.magnitude > 0.1f && forward > 0.1f)
        {
            Vector2 dir = velocity.normalized;

            animator.SetFloat("MoveX", dir.x);
            animator.SetFloat("MoveY", dir.y);
        } else if (velocity.magnitude > 0.1f && forward < 0.1f)
        {
            Vector2 dir = velocity.normalized;

            animator.SetFloat("MoveX", -dir.x);
            animator.SetFloat("MoveY", -dir.y);
        } else
        {
            Vector2 dir = transform.up;

            animator.SetFloat("MoveX", dir.x);
            animator.SetFloat("MoveY", dir.y);
        }
    }
}
