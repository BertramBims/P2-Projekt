using UnityEngine;
using UnityEngine.Playables;

public static class CutsceneManager
{
    public static PlayableDirector ActiveDirector;
}

public class CutsceneRegister : MonoBehaviour
{
    private PlayableDirector director;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    void OnEnable()
    {
        director.played += OnPlayed;
    }

    void OnDisable()
    {
        director.played -= OnPlayed;
    }

    void OnPlayed(PlayableDirector d)
    {
        CutsceneManager.ActiveDirector = d;
    }
}
