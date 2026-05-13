using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector cutscene;

    [Header("Settings")]
    public bool playOnEnter = true;
    public bool playOnlyOnce = true;
    public string playerTag = "Player";

    private bool hasPlayed = false;
    [SerializeField] private bool enableCameraOnTrigger;

    public Camera playerVisualCamera;
    public Camera playerMobilityCamera;

    public GameObject nextTriggerToEnable;

    [Header("Optional Objects to Disable or Enable...")]
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playOnEnter) return;
        if (hasPlayed && playOnlyOnce) return;

        if (collision.CompareTag(playerTag))
        {
            if (enableCameraOnTrigger)
            {
                playerVisualCamera.gameObject.SetActive(true);
                playerMobilityCamera.gameObject.SetActive(true);
            }

            if(nextTriggerToEnable != null)
            {
                nextTriggerToEnable.SetActive(true);
                Debug.Log(nextTriggerToEnable.name + " should be enabled now");
            }
            PlayCutscene();
        }
    }

    public void PlayCutscene()
    {
        if (cutscene == null)
        {
            return;
        }

        Debug.Log("should play " + cutscene.name);
        cutscene.Play();
        hasPlayed = true;
    }

    public void DisableObjects()
    {
        if (objectsToDisable.Length == 0)
            return;

        for (int i = 0; i < objectsToDisable.Length; i++)
        {
            objectsToDisable[i].SetActive(false);
        }
    }

    public void EnableObjects()
    {
        if (objectsToEnable.Length == 0)
            return;

        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            objectsToEnable[i].SetActive(true);
        }
    }
}
