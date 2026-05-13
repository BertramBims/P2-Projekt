using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReconnectUI : MonoBehaviour
{
    public GameObject reconnectUI;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void Update()
    {
        CheckConnection();
    }

    void CheckConnection()
    {
        if (playerInput.devices.Count == 0)
        {
            ShowReconnectUI();
        } else
        {
            HideReconnectUI();
        }
    }

    void ShowReconnectUI()
    {
        if(!reconnectUI.activeSelf)
            reconnectUI.SetActive(true);
    }

    void HideReconnectUI()
    {
        if(reconnectUI.activeSelf)
            reconnectUI.SetActive(false);
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Disconnected)
        {
            if (playerInput.devices.Contains(device))
            {
                playerInput.user.UnpairDevices();
                ShowReconnectUI();
            }
        }
    }
}
