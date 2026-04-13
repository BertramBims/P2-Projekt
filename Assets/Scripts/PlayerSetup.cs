using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
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

        transform.position = new Vector3(playerIndex * 2f, 0, 0);
        Debug.Log("Assigned camera to display: " + playerIndex);
    }
}
