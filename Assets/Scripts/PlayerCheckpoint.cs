using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    private Vector3 currentCheckpoint;

    void Start()
    {
        currentCheckpoint = transform.position; // start position
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
        Debug.Log("Checkpoint reached!");
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint;
    }
}