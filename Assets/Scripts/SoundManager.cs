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
    public AudioSource ambience3;

    private float targetVolume = 0.5f;

    public AudioClip[] ambienceClips; // Background Sounds
    public AudioClip[] musicClips; // Music Sounds

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

        musicInGame.Stop();

        int levelIndex = scene.buildIndex;

        if (levelIndex < musicClips.Length && musicClips[levelIndex] != null)
        {
            musicInGame.clip = musicClips[levelIndex];
            musicInGame.volume = 0f;
            musicInGame.Play();
        }

        switch (levelIndex) // 3 Ambient sounds for each level
        {
            case 0:
                PlayAmbience(0, 1, 2); // Level 1
                break;

            case 1:
                PlayAmbience(3, 4, 5); // Level 2
                break;

            case 2:
                PlayAmbience(6, 7, 8); // Level 3
                break;

            case 3:
                PlayAmbience(9, 10, 11); // Level 4
                break;

            default:
                Debug.LogWarning("Ingen ambience sat op for scene index: " + levelIndex);
                break;
        }   
    }
    void PlayAmbience(int Amb1, int Amb2, int Amb3)
    {
        if (Amb3 < ambienceClips.Length)
        {
            ambience1.clip = ambienceClips[Amb1];
            ambience2.clip = ambienceClips[Amb2];
            ambience3.clip = ambienceClips[Amb3];

            ambience1.Play();
            ambience2.Play();
            ambience3.Play();
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
        
        if (ambience3 != null)
            ambience3.Stop();
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

    private void Update()
    {
        if (ambience1.clip == ambienceClips[0])
        {
            if (Mathf.Abs(ambience1.volume - targetVolume) < 0.05f) // Hvis den når 0.05f indenfor target, skifter den target
            {
                targetVolume = Random.Range(0.2f, 0.8f); // Nyt target
            }
            
            ambience1.volume = Mathf.Lerp(ambience1.volume, targetVolume, Time.deltaTime * 0.5f); // Start lyd til tarrget, med en hastighed
        }

        if (musicInGame.isPlaying)
        {
            float timeLeft = musicInGame.clip.length - musicInGame.time;

            if (timeLeft < 6f) // Fade OUT (sidste 6 sek)
            {
                musicInGame.volume = Mathf.Lerp(musicInGame.volume, 0f, Time.deltaTime * 0.5f);
            }

            else // Fade IN (start)
            {
                musicInGame.volume = Mathf.Lerp(musicInGame.volume, 0.15f, Time.deltaTime * 0.5f);
            }
        }
    }   
}
