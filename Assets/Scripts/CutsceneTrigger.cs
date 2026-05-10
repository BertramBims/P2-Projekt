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
                nextTriggerToEnable.SetActive(true);
            PlayCutscene();
        }
    }

    public void PlayCutscene()
    {
        if (cutscene == null)
        {
            return;
        }

        cutscene.Play();
        hasPlayed = true;
    }
}
