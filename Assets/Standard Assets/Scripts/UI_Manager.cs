using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

    
    [SerializeField]
    Text TXT_Score;
    public Image fingerMove;
    public static bool fingerTouch;
    public int score;
    static int level = 0;
  
    void Start()
    {
        level++;
        Time.timeScale = 1;
        score = 0;
        SetGravity();
        fingerTouch = false;
        fingerMove.enabled = false;
        StartCoroutine(FingerDisplay());
    }

    public IEnumerator FingerDisplay ()
	{
		yield return new WaitForSeconds(3);
		if(fingerTouch == false)
		{
			fingerMove.enabled = true;
		}
		StopCoroutine(FingerDisplay());
	}

	
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
	}	
	
	
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
                break;

            case "Venus":
                gravity3.y = -8.87f;
                break;

            case "Mars":
                gravity3.y = -3.711f;
                break;

            case "Moon": 
				gravity3.y = -1.622f;
				break;
				
			case "EarthArtic": case "EarthDesert": 
				gravity3.y = -9.81f;
				break;
        
        }

        Physics.gravity = gravity3;
	}

}
