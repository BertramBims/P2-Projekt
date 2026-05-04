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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playOnEnter) return;
        if (hasPlayed && playOnlyOnce) return;

        if (collision.CompareTag(playerTag))
        {
            PlayCutscene();
        }
    }

    public void PlayCutscene()
    {
        if (cutscene == null)
        {
            Debug.LogWarning("No PlayableDirector assigned!");
            return;
        }

        cutscene.Play();
        hasPlayed = true;
    }
}
