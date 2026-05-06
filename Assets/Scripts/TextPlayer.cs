using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace AYellowpaper.SerializedCollections
{
    public class TextPlayer : MonoBehaviour
    {
        [SerializedDictionary("Key", "TextEntry")]
        public SerializedDictionary<string, string> textEntries;

        public GameObject textHolder;
        public TMP_Text text;
        public float typingSpeed = 0.05f;

        public AudioSource audioSource;
        public AudioClip typeSound;

        public PlayableDirector playableDirector;

        private Coroutine currentRoutine;
        private bool isTyping = false;

        private void Start()
        {
            if (playableDirector != null)
            {
                playableDirector.Play();
            }
        }

        /*private void Update()
        {
            if (isTyping && //input to continue)
        }*/

        public void ShowText(string key)
        {
            if (textEntries.TryGetValue(key, out string entry))
            {
                if(currentRoutine != null)
                {
                    StopCoroutine(currentRoutine);
                }

                currentRoutine = StartCoroutine(PlayText(entry));
            } else
            {
                Debug.LogWarning($"No text entry found for key: {key}");
            }
        }

        IEnumerator PlayText (string textEntry)
        {
            textHolder.SetActive(true);
            text.text = "";
            isTyping = true;

            foreach (char c in textEntry)
            {
                text.text += c;

                if (typeSound != null)
                {
                    audioSource.PlayOneShot(typeSound);
                }

                yield return new WaitForSeconds(typingSpeed);
            }

            isTyping = false;
        }
    }
}