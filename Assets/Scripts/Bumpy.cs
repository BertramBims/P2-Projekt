using UnityEngine;

public class BumpyRoadEffect : MonoBehaviour
{
    public float bumpForce = 0.4f;
    public float movementThreshold = 0.1f;

    private bool onBumpyRoad = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (onBumpyRoad && rb.linearVelocity.magnitude > movementThreshold)
        {
            Vector2 bump = Random.insideUnitCircle * bumpForce;
            rb.AddForce(bump, ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BumpyRoad"))
            onBumpyRoad = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("BumpyRoad"))
            onBumpyRoad = false;
    }
}