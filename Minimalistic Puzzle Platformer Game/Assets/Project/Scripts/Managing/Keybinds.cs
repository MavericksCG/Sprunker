using UnityEngine;

namespace Sprunker.Managing {
    public class Keybinds : MonoBehaviour {

        #region Singleton 

        public static Keybinds instance;


        private void Awake () {
            instance = this;
        }

        #endregion

        [Header("Keybinds")] 
        public KeyCode jump = KeyCode.Space;
        public KeyCode altJump = KeyCode.UpArrow;
        public KeyCode secretJump = KeyCode.W;

        public KeyCode sprint = KeyCode.LeftShift;
        public KeyCode altSprint = KeyCode.LeftControl;

        public KeyCode superJump = KeyCode.Mouse2;
        /* Keyboard */ public KeyCode superJumpKeyboard = KeyCode.Tab;

        public KeyCode slowMotion = KeyCode.Mouse1;
        // fix later ---> /* Keyboard */ public KeyCode slowMotionKeyboard = KeyCode.Quote;

        public KeyCode quickRestart = KeyCode.R;

        public KeyCode interact = KeyCode.E;

        public KeyCode pauseOrResume = KeyCode.Escape;

        public KeyCode dashKey = KeyCode.Mouse3;
        /* Keyboard */ public KeyCode dashKeyKeyboard = KeyCode.LeftAlt;

        public KeyCode openCloseAdvancedMenu = KeyCode.F1;
        public KeyCode openCloseStandardMenu = KeyCode.F2;
        public KeyCode openCloseSecondPage = KeyCode.F3;
    }
}