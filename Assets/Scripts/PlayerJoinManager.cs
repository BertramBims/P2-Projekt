using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerJoinManager : MonoBehaviour
{
    public PlayerInput player1;
    public PlayerInput player2;

    private int playersJoined = 0;

    private IDisposable subscription;

    private void OnEnable()
    {
        subscription = InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);
    }

    private void OnDisable()
    {
        subscription.Dispose();
    }

    private void OnAnyButtonPress(InputControl control)
    {
        var device = control.device;

        //Ignore non-gamepads
        if (!(device is Gamepad))
            return;

        AssignNextPlayer(device);
    }

    void AssignNextPlayer(InputDevice device)
    {
        if (player1.devices.Contains(device) || player2.devices.Contains(device))
            return;

        if (player1.devices.Count == 0)
        {
            player1.SwitchCurrentControlScheme(device);
            player1.ActivateInput();
            Debug.Log("Player 1 joined with " + device);
        } else if (player2.devices.Count == 0)
        {
            player2.SwitchCurrentControlScheme(device);
            player2.ActivateInput();
            Debug.Log("Player 2 joined with " + device);
        }
    }
}
