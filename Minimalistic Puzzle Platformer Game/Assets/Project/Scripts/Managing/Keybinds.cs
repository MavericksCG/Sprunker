using UnityEngine;

public class Keybinds : MonoBehaviour
{
    public static Keybinds instance;


    private void Awake () {
        instance = this;
    }

    [Header("KEYBINDS")]
    public KeyCode jump = KeyCode.Space;
    public KeyCode altJump = KeyCode.UpArrow;

    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode altSprint = KeyCode.LeftControl;

    public KeyCode superJump = KeyCode.Tab;

    public KeyCode slowMotion = KeyCode.Mouse1;

    public KeyCode quickRestart = KeyCode.R;

    public KeyCode interact = KeyCode.E;

    public KeyCode pauseOrResume = KeyCode.Escape;

    public KeyCode dashKey = KeyCode.LeftAlt;
}