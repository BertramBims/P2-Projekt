using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] Tilemap worldTileMap;
    [SerializeField] Transform player;

    void Update()
    {
        // Compress bounds to only include existing tiles
        worldTileMap.CompressBounds();

        //Get the boundaries of the tilemap
        Bounds cameraBounds = worldTileMap.localBounds;
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, playerCam.transform.position.z);

        // Define the camera dimensions
        float halfHeight = playerCam.orthographicSize;
        float halfWidth = halfHeight * playerCam.aspect;

        // Clamp the target position to stay within bounds
        targetPos.x = Mathf.Clamp(targetPos.x, cameraBounds.min.x + halfWidth, cameraBounds.max.x - halfWidth);
        targetPos.y = Mathf.Clamp(targetPos.y, cameraBounds.min.y + halfHeight, cameraBounds.max.y - halfHeight);
        Debug.Log($"Camera Bounds: {cameraBounds.min.x}, {cameraBounds.min.y} to {cameraBounds.max.x}, {cameraBounds.max.y}");
        // Apply the target position
        playerCam.transform.position = targetPos;
    }
}