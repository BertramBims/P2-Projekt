using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource forwardsUI;
    public AudioSource backwardsUI;
    public AudioSource dirtWheel;
    public AudioSource roadWheel;
    public AudioSource dirtBlind;
    public AudioSource roadBlind;
    public AudioSource musicInGame;
    public AudioSource collisionBump;
    public AudioSource pickupSound;
    public AudioSource carDrive;
    public AudioSource[] ambienceSounds;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllAmbience();

        int levelIndex = scene.buildIndex;
        if (levelIndex >= 0 && levelIndex < ambienceSounds.Length)
        {
            if (ambienceSounds[levelIndex] != null)
            {
                ambienceSounds[levelIndex].Play();
            }
            else
            {
                Debug.LogWarning("AudioSource mangler på index: " + levelIndex);
            }
        }
        else
        {
            Debug.LogWarning("Ingen ambience til denne scene index: " + levelIndex);

        }
    }

    void StopAllAmbience()
    {
        foreach (AudioSource audio in ambienceSounds)
        {
            if (audio != null) 
            { 
                audio.Stop();
            }
        }
    }
}
