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

        public KeyCode sprint = KeyCode.LeftShift;
        public KeyCode altSprint = KeyCode.LeftControl;

        public KeyCode superJump = KeyCode.Tab;

        public KeyCode slowMotion = KeyCode.Mouse1;

        public KeyCode quickRestart = KeyCode.R;

        public KeyCode interact = KeyCode.E;

        public KeyCode pauseOrResume = KeyCode.Escape;

        public KeyCode dashKey = KeyCode.LeftAlt;

        public KeyCode openCloseAdvancedMenu = KeyCode.F1;
        public KeyCode openCloseStandardMenu = KeyCode.F2;

    }
}