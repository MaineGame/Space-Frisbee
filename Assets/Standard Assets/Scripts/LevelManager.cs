using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject FadeOut;
	public GameObject FadeIn;
	
	private string sceneName;
	private	string sceneNameLoad;
	
	void Start () {
		FadeOut.SetActive(false);
	}
	
	
	void Update(){
	}
	
	public void LevelLoads(string sceneName){
		//sceneNumber = sceneNumber;
		sceneNameLoad = sceneName;
		StartCoroutine(LoadLevel());

		
	}
	IEnumerator LoadLevel(){
		FadeOut.SetActive(true);
		yield return new WaitForSeconds(1);
		Application.LoadLevel(sceneNameLoad);
	}
	
	public void QuitGame(){
		StartCoroutine(QuitGameIE());
		
	}
	IEnumerator QuitGameIE(){
		FadeOut.SetActive(true);
		yield return new WaitForSeconds(1);
		Application.Quit();
	}
}
