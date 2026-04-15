using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    public bool goToSpawn;

    void Start()
    {
        AssignCamera();
    }

    void AssignCamera()
    {
        Camera cam = GetComponentInChildren<Camera>();

        if (cam == null)
        {
            Debug.Log("No camera found!");
            return;
        }

        int playerIndex = GetComponent<PlayerInput>().playerIndex;
        cam.targetDisplay = playerIndex;

        if (goToSpawn) transform.position = GameObject.Find("startPosition").transform.position;
        Debug.Log("Assigned camera to display: " + playerIndex);
    }
}
