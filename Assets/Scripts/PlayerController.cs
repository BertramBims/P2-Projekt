using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Movement...")]
    public float moveSpeed = 5f;
    public Vector2 moveInput;
    private Vector2 lastMove;

    private Rigidbody2D rb;
    private Animator animator;
    private GameObject playerMobility;
    //public Transform playerMobilityPositionTarget;

    private bool canPushPlayerMobility;
    public List<Transform> pushedMovementSpots;

    public bool onBumpyRoad;
    public float bumpForce = 0.15f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMove = moveInput.normalized;
        }

        if (onBumpyRoad && moveInput.sqrMagnitude > 0.01f)
        {
            Debug.Log("Affected by Bumpy Road");
            Vector2 bump = Random.insideUnitCircle * bumpForce;
            rb.linearVelocity *= 0.5f;
            rb.linearVelocity += bump;
        }
    }

    void HandleAnimation()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
        animator.SetBool("IsMoving", isMoving);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {

        if (ctx.performed)
        {
            if (playerMobility.GetComponent<PlayerMobilityController>().beingPushed == true)
            {
                Debug.Log("Set BeingPushed to false");
                playerMobility.GetComponent<PlayerMobilityController>().beingPushed = false;
            }

            Debug.Log("Performed Interact");
            if (canPushPlayerMobility)
            {
                Debug.Log("Should overtake control of Mobility_Player now");

                //logic for overtaking control of other player
                playerMobility.GetComponent<PlayerMobilityController>().beingPushed = true;
                playerMobility.GetComponent<PlayerMobilityController>().playerVisual = gameObject;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collides with Obstacle");

        if (collision.transform.CompareTag("Obstacle"))
        {
            Debug.Log("Tag found");

            Light2D light = collision.transform.GetComponentInChildren<Light2D>(true);

            if (light != null)
            {
                Debug.Log("Light2D found");
                light.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("No Light2D found");
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Backside")
        {
            Debug.Log("Found Mobility_Player Back");
            canPushPlayerMobility = true;
            playerMobility = collision.transform.parent.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name == "Backside")
        {
            Debug.Log("Exited Area of Mobility_Player Back");
            canPushPlayerMobility = false;
        }
    }
}
