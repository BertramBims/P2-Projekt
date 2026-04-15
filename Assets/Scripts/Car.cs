using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f;
    public int direction = 1;

    public Transform visual;

    void Start()
    {
        if (direction == 1)
            visual.localRotation = Quaternion.Euler(0, 0, -90);
        else
            visual.localRotation = Quaternion.Euler(0, 0, 90);
    }

   void Update()
{
    transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

    if (Mathf.Abs(transform.position.x) > 10f)
    {
        Destroy(gameObject);
    }
}

    void OnBecameInvisible()
{
    Destroy(gameObject);
}
}