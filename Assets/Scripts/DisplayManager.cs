using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    void Awake()
    {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
