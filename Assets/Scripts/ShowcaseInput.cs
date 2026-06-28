using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ShowcaseInput : MonoBehaviour
{
    private PlayerJoinManager joinManager;

    public Camera player1Camera;
    public Camera player2Camera;

    private void Start()
    {
        joinManager = GetComponent<PlayerJoinManager>();
    }

    public void SkipCutscene(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        var director = CutsceneManager.ActiveDirector;

        if (director == null)
        {
            Debug.Log("No active cutscene to skip");
            return;
        }

        // Jump to end
        director.time = director.duration;

        // Force evaluation (VERY IMPORTANT)
        director.Evaluate();

        // Stop Playback
        director.Stop();

        Debug.Log("Cutscene Skipped");
    }

    public void RestartScene(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void NextScene(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void SwitchInputDevice(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        InputDevice player1InputDevice = joinManager.player1.devices[0];
        InputDevice player2InputDevice = joinManager.player2.devices[0];

        joinManager.player1.SwitchCurrentControlScheme(player2InputDevice);
        joinManager.player2.SwitchCurrentControlScheme(player1InputDevice);
    }

    public void SwitchCurrentMonitor(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        int display1 = player1Camera.targetDisplay;
        int display2 = player2Camera.targetDisplay;

        player1Camera.targetDisplay = display2;
        player2Camera.targetDisplay = display1;
    }
}
