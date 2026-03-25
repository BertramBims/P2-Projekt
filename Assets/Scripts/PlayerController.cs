using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement...")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 lastMove;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //HandleAnimation();
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
    }

    /*void HandleAnimation()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
        animator.SetBool("IsMoving", isMoving);
    }*/

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
}
