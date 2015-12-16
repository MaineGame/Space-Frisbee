using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighscoreManager : MonoBehaviour {

	public List<int> scores = new List<int>();
	public int NUM_SCORES_TO_STORE = 10;
	public UI_Manager score;
	public Text TXT_Highscore;
	public string levelName;
	
	// Use this for initialization
	void Start () {
		if(!System.IO.Directory.Exists("Scores"))
		{
			System.IO.Directory.CreateDirectory("Scores");
		}
		levelName = Application.loadedLevelName;
		score = GetComponent<UI_Manager>();
		loadScores ();
		
	}

	void OnEnable() {
	}

	public void saveScore() {
		scores.Add (score.score);
		trimScores ();
		writeScores ();
		TXT_Highscore.text = getScoresString();

	}

	private void writeScores () {
		string scoreString = "";
		int count = 0;
		foreach (int score in scores) {
			scoreString += "," + score + (count == 9 ? "" : ",");
			count ++;
		}
		System.IO.File.WriteAllText ("Scores/" + levelName + "-SCORES.DAT", scoreString);
	}

	private void trimScores() {
		if (scores.Count > NUM_SCORES_TO_STORE) {
			scores.Sort();
			int toDelete = scores.Count - NUM_SCORES_TO_STORE;
			for(int i = 0; i < toDelete; i ++) {
				scores.RemoveAt(0);
			}
		}
	}

	public List<int> getScores() {
		return scores;
	}

	public string getScoresString() {
		scores.Sort();
		scores.Reverse();
		string scoreString = "";
		int count = 0;
		foreach (int score in scores) {
			scoreString += score + (count == 9 ? "" : "\n");
			count ++;
		}
		return scoreString;
	}

	private void writeDefaultScores() {
		for (int i = 0; i < NUM_SCORES_TO_STORE; i ++) {
			scores.Add(0);
		}
		scores.Sort ();
		scores.Reverse ();
		writeScores ();
	}

	private void loadScores() {
		if (!System.IO.File.Exists ("Scores/" + levelName + "-SCORES.DAT")) {
			writeDefaultScores();
		}

		scores.Clear ();

		//System.IO.File.WriteAllText ("./SCORES.DAT", "ASDFASFGWERGABU FUGSEU GOUSEB US TUGHS UON SRN");
		string ScoresList = System.IO.File.ReadAllText ("Scores/" + levelName + "-SCORES.DAT");

		string[] parts = ScoresList.Split (',');
		for (int i = 0; i < NUM_SCORES_TO_STORE; i ++) {

			int score = int.Parse(parts[(i * 2) + 1]);
			scores.Add(score);
		}
	}
}
