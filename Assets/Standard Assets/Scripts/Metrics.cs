using UnityEngine;
using System.Collections;

public class Metrics : MonoBehaviour {
	
	public static string timeStart;
	public static string timeEnd;
	public static int touches;
	public static int buttons;
	public static int hits;
	//public static string win;
	public static int plays;
	
	public static string folder = "/SpaceFrisbee_Data";
	public static string fileName;
	
	// Use this for initialization
	void Awake () {
		
		timeStart = PlayerPrefs.GetString("timeStart");
		timeEnd = "";
		touches = PlayerPrefs.GetInt ("touches");
		buttons = PlayerPrefs.GetInt("buttons");
		hits = 0;
		plays = 0;
		//win = "True";
		
		fileName = "/SF " + System.DateTime.Now.ToString("MM-dd-yyyy") + ".csv";
		
		if(!System.IO.Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + folder))
		{
			System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + folder);
		}
		if(!System.IO.File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + folder + fileName))
		{
			System.IO.File.Create(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + folder + fileName);
		}
	}
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Metrics.touches++;                                        
		}
	}
	public void ButtonPressed()
	{
		buttons++;	
	}
	public static void OutputMetrics()
	{
		timeEnd = System.DateTime.Now.ToString("HHmmss");
		string line = timeStart + "," + timeEnd + "," + touches + "," + buttons + "," + hits + "," + plays;
		System.IO.StreamWriter file = new System.IO.StreamWriter(Application.dataPath + fileName, true);
		file.WriteLine (line);
		file.Close ();
		//GoogleAnalytics.instance.LogScreen(
	}
}

