using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsScript : MonoBehaviour {

	public GameObject menuBackdrop;
	[Header("Default values")]
	public int defaultTimeAmount; //0 = 1:00, 1 = 1:30, 2 = 2:00, 3 = 2:30, 4 = 3:00
	[Header("Buttons")]
	public Button moveSpeedOnButton;
	public Button shotSpeedOnButton;
	public Button moveSpeedOffButton;
	public Button shotSpeedOffButton;
	public Button doneButton;
	public Button map1Button;
	public Button map2Button;
	public Button map3Button;
	public Button settingsButton;
	[Header("Sliders")]
	public Slider timeAmountSlider;

	void Start () {
		
			//Use if you want last game settings used to remain until changed
		timeAmountSlider.value = PlayerPrefs.GetFloat("timeAmount");
			//Use if you want to be able to set default values
		//timeAmountSlider.value = defaultTimeAmount;

	}
	
	void Update () {
		
		//Slider value wouldn't convert to an Int
		PlayerPrefs.SetFloat("timeAmount", timeAmountSlider.value);

		if (PlayerPrefs.GetInt("moveSpeedModifier") == null) {
			PlayerPrefs.SetInt("moveSpeedModifier", 0);
		}
		if (PlayerPrefs.GetInt("shotSpeedModifier") == null) {
			PlayerPrefs.SetInt("shotSpeedModifier", 0);
		}
		if (PlayerPrefs.GetInt("moveSpeedModifier") == 0) {
			moveSpeedOnButton.interactable = true;
			moveSpeedOffButton.interactable = false;
		}
		if (PlayerPrefs.GetInt("shotSpeedModifier") == 0) {
			shotSpeedOnButton.interactable = true;
			shotSpeedOffButton.interactable = false;
		}
		if (PlayerPrefs.GetInt("moveSpeedModifier") == 1) {
			moveSpeedOnButton.interactable = false;
			moveSpeedOffButton.interactable = true;
		}
		if (PlayerPrefs.GetInt("shotSpeedModifier") == 1) {
			shotSpeedOnButton.interactable = false;
			shotSpeedOffButton.interactable = true;
		}

	}
	public void moveSpeedClick () {
		PlayerPrefs.SetInt("moveSpeedModifier", 1);
		moveSpeedOnButton.interactable = false;
		moveSpeedOffButton.interactable = true;
	}
	public void shotSpeedClick () {
		PlayerPrefs.SetInt("shotSpeedModifier", 1);
		shotSpeedOnButton.interactable = false;
		moveSpeedOffButton.interactable = true;
	}
	public void moveSpeedDisabledClick () {
		PlayerPrefs.SetInt("moveSpeedModifier", 0);
		moveSpeedOnButton.interactable = true;
		moveSpeedOffButton.interactable = false;
	}
	public void shotSpeedDisabledClick () {
		PlayerPrefs.SetInt("shotSpeedModifier", 0);
		shotSpeedOnButton.interactable = true;
		shotSpeedOffButton.interactable = false;
	}
	public void GameSettingsClick () {
		menuBackdrop.SetActive(true);
		settingsButton.interactable = false;
	}
	public void ClearPlayerPrefsClick () {
		PlayerPrefs.DeleteAll();
	}
	public void DoneClick () {
		menuBackdrop.SetActive(false);
		settingsButton.interactable = true;
	}
}
