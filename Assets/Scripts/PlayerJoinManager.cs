using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerJoinManager : MonoBehaviour
{
    public PlayerInput player1;
    public PlayerInput player2;

    private int playersJoined = 0;

    private void OnEnable()
    {
        InputSystem.onAnyButtonPress.CallOnce(control =>
        {
            var device = control.device;
            AssignNextPlayer(device);
        });
    }
                                
    void AssignNextPlayer(InputDevice device)
    {
        if (player1.devices.Count == 0)
        {
            player1.SwitchCurrentControlScheme(device);
            player1.ActivateInput();
        } else if (player2.devices.Count == 0)
        {
            player2.SwitchCurrentControlScheme(device);
            player2.ActivateInput();
        }
    }
}
