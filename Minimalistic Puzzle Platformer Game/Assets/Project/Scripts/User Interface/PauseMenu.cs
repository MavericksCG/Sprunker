using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;

	public GameObject pausedUI;

	private void Update () {
		if (Input.GetKeyDown(Keybinds.instance.pauseOrResume)) {
			if (GameIsPaused) {
				Resume();
			}
			else {
				Pause();
			}
		}
	}

	private void Resume () {
		pausedUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	private void Pause () {
		pausedUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

}
