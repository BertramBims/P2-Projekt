using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMobilityController : MonoBehaviour
{
    [Header("Movement...")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    public bool beingPushed = false;

    private float leftInput;
    private float rightInput;

    private Rigidbody2D rb;
    private Animator animator;

    private float forward;
    public GameObject playerVisual;

    [SerializeField] private Transform visual;
    [SerializeField] private Transform cameraTransform;

    public int beingPushedMovementSpot;
    public bool debugMovementBool;

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
        if (!beingPushed && !debugMovementBool)
        {
            HandleMovement();
            HandleAnimation();
            GetComponent<BoxCollider2D>().enabled = true;
        } else if (beingPushed && debugMovementBool)
        {
            Debug.Log("Overriding");
            OverrideAnimation();
            //transform.position = playerVisual.GetComponent<PlayerController>().playerMobilityPositionTarget.position;
            transform.position = playerVisual.GetComponent<PlayerController>().pushedMovementSpots[beingPushedMovementSpot - 1].transform.position;
            GetComponent<BoxCollider2D>().enabled = false;
        }
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

    void OverrideAnimation()
    {
        animator.SetFloat("MoveX", playerVisual.GetComponent<PlayerController>().moveInput.x);
        animator.SetFloat("MoveY", playerVisual.GetComponent<PlayerController>().moveInput.y);
    }
}
