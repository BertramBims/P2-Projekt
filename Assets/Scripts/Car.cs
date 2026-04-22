using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f;
    public int direction = 1;

    public Transform visual;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
       spriteRenderer = visual.GetComponent<SpriteRenderer>();
    }

   void Update()
{
    transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

    // Mirror sprite when moving in direction 1 (right)
    if (spriteRenderer != null)
    {
        spriteRenderer.flipX = (direction == 1);
    }

    if (Mathf.Abs(transform.position.x) > 10f)
    {
        Destroy(gameObject);
    }
}

}