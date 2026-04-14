using UnityEngine;
using UnityEngine.SceneManagement;

public enum Soundtype
{
    Select,
    Deselect,
    Collision,
    Pickup
}

public class SoundManager : MonoBehaviour

{
    public static SoundManager instance;

    public AudioSource selectUI;
    public AudioSource deselectUI;
    public AudioSource collisionBump;
    public AudioSource pickupSound;
    public AudioSource musicInGame;
    public AudioSource ambience1;
    public AudioSource ambience2;

    public AudioClip[] ambienceClips; // Background Sounds

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
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

        switch (levelIndex)
        {
            case 1:
                PlayAmbience(0, 1); // Level 1
                break;

            case 2:
                PlayAmbience(2, 3); // Level 2
                break;

            case 3:
                PlayAmbience(4, 5); // Level 3
                break;

            case 4:
                PlayAmbience(6, 7); // Level 4
                break;

            default:
                Debug.LogWarning("Ingen ambience sat op for scene index: " + levelIndex);
                break;
        }
    }
    void PlayAmbience(int startIndex, int endIndex)
    {
        if (endIndex < ambienceClips.Length)
        {
            ambience1.clip = ambienceClips[startIndex];
            ambience2.clip = ambienceClips[endIndex];

            ambience1.Play();
            ambience2.Play();
        }
        else
        {
            Debug.LogWarning("Ambience mangler");
        }
    }

    void StopAllAmbience()
    {
        if (ambience1 != null)
            ambience1.Stop();

        if (ambience2 != null)
            ambience2.Stop();
    }

    public void PlaySound(Soundtype type)
    {
        switch (type)
        {
            case Soundtype.Select:
                if (selectUI != null)
                    selectUI.PlayOneShot(selectUI.clip);
                break;

            case Soundtype.Deselect:
                if (deselectUI != null)
                    deselectUI.PlayOneShot(deselectUI.clip);
                break;

            case Soundtype.Collision:
                if (collisionBump != null)
                    collisionBump.PlayOneShot(collisionBump.clip);
                break;

            case Soundtype.Pickup:
                if (pickupSound != null)
                    pickupSound.PlayOneShot(pickupSound.clip);
                break;
        }
    }
}
