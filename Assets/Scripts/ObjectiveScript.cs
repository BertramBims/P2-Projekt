using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Playables;

namespace AYellowpaper.SerializedCollections
{
    public class ObjectiveScript : MonoBehaviour
    {
        [SerializedDictionary("Int Key", "CurrentObjective")]
        public SerializedDictionary<int, string> objectives;

        public TMP_Text text;
        public int currentObjective;

        public void PlayNextObjective()
        {
            currentObjective++;

            ShowObjective(currentObjective);
        }

        public void ShowObjective(int key)
        {
            currentObjective = key;

            if (objectives.TryGetValue(key, out string entry))
            {
                text.text = entry;
            }
            else
            {
                Debug.LogWarning($"No text entry found for key: {key}");
            }
        }
    }
}