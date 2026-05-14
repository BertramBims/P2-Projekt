using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mobility_Player"))
        {
            PlayerCheckpoint player = other.GetComponent<PlayerCheckpoint>();
            player.SetCheckpoint(transform.position);
        }
    }
}