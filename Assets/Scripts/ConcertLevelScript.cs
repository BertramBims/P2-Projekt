using UnityEngine;

public class ConcertLevelScript : MonoBehaviour
{
    public PlayerMobilityController mobilityPlayer;

    public GameObject map;
    public GameObject finalSceneLoader;

    private void Start()
    {
        mobilityPlayer.enabled = false;
    }

    public void EnableMap()
    {
        map.SetActive(true);
    }

    public void DisableMap()
    {
        map.SetActive(false);
        mobilityPlayer.enabled = true;
        finalSceneLoader.SetActive(true);
    }
}
