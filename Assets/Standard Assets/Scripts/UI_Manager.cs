using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

	public Image fingerMove;
	public static bool fingerTouch;
	
	public IEnumerator FingerDisplay ()
	{
		yield return new WaitForSeconds(3);
		if(fingerTouch == false)
		{
			fingerMove.enabled = true;
		}
		StopCoroutine(FingerDisplay());
	}
	
	void Start ()
	{
		Time.timeScale = 1;
		score = 0;
		SetGravity();
		fingerTouch = false;
		fingerMove.enabled = false;
		StartCoroutine(FingerDisplay());
	}
	
	public int score;
	public Text TXT_Score;
	
	public void AddScore()
	{
		score++;
		TXT_Score.text = score.ToString ();
	}
	
	public GameObject gameOverUI;

	public void ShowGameOverUI()
	{
		GetComponent<HighscoreManager>().saveScore();
		TXT_Score.text = score.ToString ();
		gameOverUI.SetActive(true);
		//Time.timeScale = 0;
	}	
	
	
	public Image gravityBarFill;
	private float currentGravity;
//	private float maxGravity = 24.79f;
	private string levelName;
	private Vector3 gravity3 = new Vector3(0,0,0);
	
	public Text gravityText;
	
	public void SetGravity()
	{
        levelName = Application.loadedLevelName;
		
		switch(levelName)
		{
            case "Mercury":
                gravity3.y = -3.7f;
                currentGravity = 0.06542960871f;
                gravityBarFill.color = Color.green;
                break;

            case "Venus":
                gravity3.y = -8.87f;
                currentGravity = 0.06542960871f;
                gravityBarFill.color = Color.green;
                break;

            case "Mars":
                gravity3.y = -3.711f;
                currentGravity = 0.06542960871f;
                gravityBarFill.color = Color.green;
                break;

            case "Moon": 
				gravity3.y = -1.622f;
				currentGravity = 0.06542960871f;
				gravityBarFill.color = Color.green;
				break;
				
			case "EarthArtic": case "EarthDesert": 
				gravity3.y = -9.81f;
				currentGravity = 0.39560306575f;
				gravityBarFill.color = Color.green;
				break;
        
        }


        Physics.gravity = gravity3;
		gravityBarFill.fillAmount = currentGravity;
		gravityText.text = "Gravity " + Mathf.Abs (Physics.gravity.y) + " m/s²"; 
	}

}
