using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class GameOver : MonoBehaviour {

	[Header("ASSIGNABLES")]
	public TextMeshProUGUI randomTextUI;

	private int chance;
	public int minimumChance;
	public int maximumChance;
	public int chanceToGetBoldText;

	public GameObject gameOverUI;
	public GameObject randomTextUIObj;
	public GameObject controlsTextUIObj;
	public GameObject showButton;
	public GameObject hideButton;

	public Transform player;

	[Space]
	public string[] randomText;
	

	
	// Miscellaneous
	private void Start () {
		PickRandomText();
	}


	private void PickRandomText () {

		int index = Random.Range(0, randomText.Length);
		string chooseRandomText = randomText[index];

		randomTextUI.text = chooseRandomText;


		// Random Chance for Bold Text
		chance = Random.Range(minimumChance, maximumChance);
		if (chance == chanceToGetBoldText) {
			randomTextUI.text = "<b>woah the bold text thing is here too? zamn</b>";
		}

	}


	// Handle Buttons
	public void ShowControls () {
		showButton.SetActive(false);
		hideButton.SetActive(true);
		controlsTextUIObj.SetActive(true);
		randomTextUIObj.SetActive(false);
	}

	public void HideControls () {
		showButton.SetActive(true);
		hideButton.SetActive(false);
		controlsTextUIObj.SetActive(false);
		randomTextUIObj.SetActive(true);
	}

	public void Retry () {	
		gameOverUI.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}


	public void Quit () {
		
		print("Quitting Game...");
		Application.Quit();
		
	}
}