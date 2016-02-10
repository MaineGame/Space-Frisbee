using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverControls : MonoBehaviour {

	public Text mute;

	public void LoadLevel(string name){
        SceneManager.LoadScene(name);
  
	}

	// Use this for initialization
	public void PlayAgainButton () {
		Metrics.plays++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	
	// Update is called once per frame
	public void MainMenuButton () {
		Metrics.OutputMetrics();
        SceneManager.LoadScene("Menu");
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
