using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Playables;

namespace AYellowpaper.SerializedCollections
{
    public class TextPlayer : MonoBehaviour
    {
        [SerializedDictionary("Key", "TextEntry")]
        public SerializedDictionary<string, string> textEntries;

        public GameObject textHolder;
        public TMP_Text text;

        public PlayableDirector playableDirector;

        private void Start()
        {
            if (playableDirector != null)
            {
                playableDirector.Play();
            }
        }

        public void ShowText(string key)
        {
            if (textEntries.TryGetValue(key, out string entry))
            {
                textHolder.SetActive(false);
                text.text = entry;
                textHolder.SetActive(true);
            } else
            {
                Debug.LogWarning($"No text entry found for key: {key}");
            }
        }
    }
}