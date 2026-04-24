using UnityEngine;

public class MudRoadScript : MonoBehaviour
{
    private PlayerController playerController;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("MudRoad"))
        {
            playerController.onMudRoad = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("MudRoad"))
        {
            playerController.onMudRoad = false;
            playerController.moveSpeed = 5f; // Reset to default speed when exiting mud road
        }
    }
}