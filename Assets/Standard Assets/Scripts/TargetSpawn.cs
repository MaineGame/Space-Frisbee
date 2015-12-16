using UnityEngine;
using System.Collections;

public class TargetSpawn : MonoBehaviour {
	
	private AudioSource audioLol;
	private Animation animationLol;

    void Start () 
	{
		audioLol = GetComponent<AudioSource>();
		animationLol = GetComponent<Animation>();
		ResetSpawn();
	}

	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Frisbee")
		{
            animationLol.Play ("Target_DOWN");
			audioLol.Play ();
			StartCoroutine(WaitAndSpawn(0.3F));		
		}
	}
	
	void ResetSpawn()
	{
		float x = Random.Range(-15,15);
		float z = Random.Range(0,15);

		transform.position = new Vector3(x,0,z);
		GetComponent<Animation>().Play ("Target_UP");

    }
	
	IEnumerator WaitAndSpawn(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		ResetSpawn();
		
	}
}
