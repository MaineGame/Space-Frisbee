using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour {
	
	public GameObject MainMenu, PlanetSelections;
	public Image MuteIcon;
	public Sprite SP_MusicOn, SP_MusicOff;
	public Text TitleLabel, MuteButtonText;
	private int touches = 0;
	private int buttons = 0;
    
	
	void Awake()
	{
		PlayerPrefs.SetInt ("buttons", 0);
		PlayerPrefs.SetInt ("touches", 0);
		PlayerPrefs.SetString("timeStart", "");
	}
	
	// Use this for initialization
	void Start () 
	{	
		TitleLabel.text = "PLANET FRISBEE";
		MainMenu.SetActive (true);
		PlanetSelections.SetActive (false);
	}
	
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			touches++;
			PlayerPrefs.SetInt ("touches", touches);                   
		}
	}
	
	
	public void LoadLevel(string name)
	{
		Application.LoadLevel(name);
	}
	
	public void EnablePlanetButtons()
	{
		MainMenu.SetActive (false);
		PlanetSelections.SetActive (true);
		TitleLabel.text = "CHOOSE A PLANET";
	}
	
	public void DisablePlanetButtons()
	{
		Start ();
	}
	
	public void RandomButton()
	{
		Application.LoadLevel(Random.Range(1,4));
        
	}
	
	public void MuteButton()
	{
		if(AudioListener.volume == 1)
		{
			AudioListener.volume = 0;
			MuteIcon.sprite = SP_MusicOff;
			MuteButtonText.text = "UN-MUTE";
		}
		else{
		
			AudioListener.volume = 1;
			MuteIcon.sprite = SP_MusicOn;
			MuteButtonText.text = "MUTE";
			
		}
	}
	public void ButtonPressed()
	{
		buttons++;
		PlayerPrefs.SetInt ("buttons", buttons);
		
	}
	
	public void QuitButton()
	{
		Application.Quit();
	}
}
