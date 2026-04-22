using UnityEngine;

public class BumpyRoadEffect : MonoBehaviour
{
    private PlayerController playerController;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BumpyRoad"))
        {
            playerController.onBumpyRoad = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("BumpyRoad"))
        {
            playerController.onBumpyRoad = false;
        }
    }
}