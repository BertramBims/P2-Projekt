using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMobilityController : MonoBehaviour
{
    [Header("Movement...")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    public bool beingPushed = false;
    public bool alwaysPushedInThisScene = false;

    private float leftInput;
    private float rightInput;

    public bool onBumpyRoad;
    public float bumpForce = 0.15f;

    public bool onMudRoad;
    public float mudSlowFactor = 0.0005f;

    private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private float forward;
    public GameObject playerVisual;

    [SerializeField] private Transform visual;
    [SerializeField] private Transform cameraTransform;

    public int beingPushedMovementSpot;
    public bool debugMovementBool;
    public GameObject directionIndicator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (alwaysPushedInThisScene)
        {
            Debug.Log("Overriding");
            OverrideAnimation();
            //transform.position = playerVisual.GetComponent<PlayerController>().pushedMovementSpots[beingPushedMovementSpot - 1].transform.position;
            rb.MovePosition(
                    playerVisual.GetComponent<PlayerController>()
                    .pushedMovementSpots[beingPushedMovementSpot - 1].transform.position
                );
            GetComponent<BoxCollider2D>().enabled = false;
            directionIndicator.SetActive(false);
        } else
        {
            if (!beingPushed)
            {
                Debug.Log("NotOverriding");
                HandleMovement();
                HandleAnimation();
                GetComponent<BoxCollider2D>().enabled = true;
                directionIndicator.SetActive(true);
            }
            else if (beingPushed)
            {
                Debug.Log("Overriding");
                OverrideAnimation();
                //transform.position = playerVisual.GetComponent<PlayerController>().pushedMovementSpots[beingPushedMovementSpot - 1].transform.position;
                rb.MovePosition(
                    playerVisual.GetComponent<PlayerController>()
                    .pushedMovementSpots[beingPushedMovementSpot - 1].transform.position
                    );
                GetComponent<BoxCollider2D>().enabled = false;
                directionIndicator.SetActive(false);
            }
        }
    }

    private void LateUpdate()
    {
        visual.rotation = Quaternion.identity;
        cameraTransform.rotation = Quaternion.identity;

        /*if (beingPushed || alwaysPushedInThisScene)
        {
            cameraTransform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                -10
            );
        }*/
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
