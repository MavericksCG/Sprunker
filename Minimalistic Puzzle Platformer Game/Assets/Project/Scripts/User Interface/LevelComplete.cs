using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class LevelComplete : MonoBehaviour {

	[Header("Assignables")]
	public TextMeshProUGUI randomTextUI;
	
	private int chance;
	public int minimumChance;
	public int maximumChance;
	public int chanceToGetBoldText;

	public GameObject levelCompleteUI;
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
			randomTextUI.text = "<b>poggers</b>";
		}

	}
	
	
	// Handle Buttons
	public void Continue () {
		
		levelCompleteUI.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		
	}


	public void Retry () {
		
		levelCompleteUI.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
	}


	public void Quit () {
		
		print("Quitting Game...");
		Application.Quit();
		
	}
}