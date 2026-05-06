using System.Collections;
using TMPro;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    private bool isTyping = false;
    public TMP_Text text;
    [SerializeField] private AudioClip typeSound;
    [SerializeField] private AudioSource audioSource;
    public string textEntry;

    private void Start()
    {
        StartCoroutine(PlayText());
    }

    IEnumerator PlayText()
    {
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
