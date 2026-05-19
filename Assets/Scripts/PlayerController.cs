using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Movement...")]
    public float moveSpeed = 5f;
    private float currentSpeed;
    public Vector2 moveInput;
    private Vector2 lastMove;

    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerMobility;
    //public Transform playerMobilityPositionTarget;

    private bool canPushPlayerMobility;
    public List<Transform> pushedMovementSpots;

    public bool onBumpyRoad;
    public float bumpForce = 0.15f;

    public bool onMudRoad;
    public float mudSlowFactor = 0.0005f;

    public GameObject lightsHolder;
    public GameObject lightPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        HandleAnimation();

        if (playerMobility != null &&
            playerMobility.GetComponent<PlayerMobilityController>().beingPushed || playerMobility.GetComponent<PlayerMobilityController>().alwaysPushedInThisScene)
        {
            PushedAnimator();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        rb.linearVelocity = moveInput.normalized * currentSpeed;

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

        if (onMudRoad && currentSpeed > 1f)
        {
            Debug.Log("Affected by Mud Road");
            currentSpeed -= mudSlowFactor;
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
                currentSpeed = moveSpeed;
                playerMobility.GetComponent<PlayerMobilityController>().beingPushed = false;
            }

            Debug.Log("Performed Interact");
            if (canPushPlayerMobility)
            {
                Debug.Log("Should overtake control of Mobility_Player now");
                currentSpeed *= 0.75f;

                //logic for overtaking control of other player
                playerMobility.GetComponent<PlayerMobilityController>().beingPushed = true;
                playerMobility.GetComponent<PlayerMobilityController>().playerVisual = gameObject;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            Instantiate(lightPrefab, collision.contacts[0].point, collision.transform.rotation, lightsHolder.transform);
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

    public void PushedAnimator()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y).normalized;

        int direction = GetDirectionIndex(move);
        playerMobility.GetComponent<PlayerMobilityController>().beingPushedMovementSpot = direction;
        Debug.Log(direction);
    }

    int GetDirectionIndex(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle < 0) angle += 360;

        if (angle >= 337.5f || angle < 22.5f) return 3;   // Right
        if (angle < 67.5f) return 2;                      // Up-right
        if (angle < 112.5f) return 1;                     // Up
        if (angle < 157.5f) return 8;                     // Up-left
        if (angle < 202.5f) return 7;                     // Left
        if (angle < 247.5f) return 6;                     // Down-left
        if (angle < 292.5f) return 5;                     // Down
        return 4;                                         // Down-right
    }
}
