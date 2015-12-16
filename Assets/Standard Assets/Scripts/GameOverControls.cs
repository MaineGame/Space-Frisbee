using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverControls : MonoBehaviour {

	public Text mute;

	public void LoadLevel(string name){
        Application.LoadLevel(name);
	}

	// Use this for initialization
	public void PlayAgainButton () {
		Metrics.plays++;
		Application.LoadLevel(Application.loadedLevelName);
	}
	
	// Update is called once per frame
	public void MainMenuButton () {
		Metrics.OutputMetrics();
		Application.LoadLevel ("Menu");
	}
	
	public void MuteButton()
	{
		if (AudioListener.volume == 1) {
			AudioListener.volume = 0;
			mute.text = "UNMUTE";
		} else {
			AudioListener.volume = 1;
			mute.text = "MUTE";
		}
	}
	
}
