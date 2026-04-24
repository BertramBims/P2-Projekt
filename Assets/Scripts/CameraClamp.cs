using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] Tilemap worldTileMap;
    [SerializeField] Transform player;

    [SerializeField] float minOffSetX;
    [SerializeField] float minOffSetY;
    [SerializeField] float maxOffSetX;
    [SerializeField] float maxOffSetY;

    void Update()
    {
        //Get the boundaries of the tilemap
        Bounds cameraBounds = worldTileMap.localBounds;

        //Attach camera to player position
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, playerCam.transform.position.z);

        //Define the camera dimensions
        float halfHeight = playerCam.orthographicSize;
        float halfWidth = halfHeight * playerCam.aspect;

        //Clamp the target position to stay within bounds
        targetPos.x = Mathf.Clamp(targetPos.x, cameraBounds.min.x + minOffSetX + halfWidth, cameraBounds.max.x - maxOffSetX - halfWidth);
        targetPos.y = Mathf.Clamp(targetPos.y, cameraBounds.min.y + minOffSetY + halfHeight, cameraBounds.max.y - maxOffSetY - halfHeight);
        Debug.Log("Camera Position: " + targetPos);

        //Apply the target position
        playerCam.transform.position = targetPos;
    }
}