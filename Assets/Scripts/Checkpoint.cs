using UnityEngine;

public class Checkpoint : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Blind"))
    {

        PlayerCheckpoint player = other.GetComponent<PlayerCheckpoint>();
        player.SetCheckpoint(transform.position);
    }
}
}