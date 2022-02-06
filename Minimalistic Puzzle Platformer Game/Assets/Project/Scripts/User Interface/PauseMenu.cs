using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	[Header("VARIABLES")] 
	[HideInInspector] public bool paused = false;
	
	public GameObject pausedUI;
	public GameObject player;

	public float desiredTimeScale;
	
	
	// Pausing
	private void Start () {
		pausedUI.SetActive(false);
	}

	private void Update () {
		if (Input.GetKeyDown(Keybinds.instance.pauseOrResume)) {
			if (paused)
				Resume();
			else
				Pause();
		}
	}

	private void Resume () {
		paused = false;
		pausedUI.SetActive(false);
		Time.timeScale = 1f;
		player.SetActive(true);
	}

	private void Pause () {
		paused = true;
		pausedUI.SetActive(true);
		Time.timeScale = desiredTimeScale;
		player.SetActive(false);
	}
	
	
	// Button Handling 
	public void ReturnToDesktop () {
		Application.Quit();
	}

	public void ReturnToMainMenu () {
		print("placeholder");
	}

	public void RestartCurrentLevel () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
