using UnityEngine;
using System.Collections;

public class FlickyFinger : MonoBehaviour {

	private Vector3 worldMousePosition,firstMousePosition, secondMousePosition, startPos;
	
	float speed; 
	public float curve;
	public UI_Manager score;
//	public High_Score_Table gameOver;
	
	private TrailRenderer trailRendererLol;
	private Rigidbody rigidbodyLol;

	public UI_Manager stopFinger;

	private AudioSource sound;

	void Start () 
	{

		score.gameOverUI.SetActive (false);
		// Sets the Vector3 pos to the frisbees position
		startPos = transform.position;
		
		//trailRendererLol = GetComponent<TrailRenderer>();
		rigidbodyLol = GetComponent<Rigidbody>();
		sound = GetComponent<AudioSource> ();
	}
	
	void Update () 
	{
		Controls();
	}
	
	public void Controls()
	{
//		if(Input.touchCount > 0)
//		{
			//StopCoroutine(stopFinger.FingerDisplay());
			if(Input.GetMouseButtonDown(0))
//			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				// Gets the mouse position when the mouse is clicked
  
				UI_Manager.fingerTouch = true;
				stopFinger.fingerMove.enabled = false;
				StopCoroutine(stopFinger.FingerDisplay());
				
				firstMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				//Debug.Log ("First Pos: " + firstMousePosition.x);
			}
			
//			if(Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
			if(Input.GetMouseButtonUp(0))
			{
				if(transform.position == startPos)
				{
					//trailRendererLol.enabled = true;
					// Gets the mouse position when the mouse button is released
					secondMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					//Debug.Log ("Second Pos: " + secondMousePosition.x);
					
					moveSpeed();
					curveSpeed();

					//Adds force to the frisbee from the speed and curve
					rigidbodyLol.AddForce(curve * 1.5f, speed * 1.2f, speed * 3f);
					rigidbodyLol.useGravity = true;
					sound.Play ();
				}
			}
			
			if(Input.GetMouseButtonDown(1))
			{
				//Resets the frisbee position 
				
				ResetFrisbee();
			}
//		}
	}
	
	public void moveSpeed()
	{
		//Gets speed of the frisbee.
		speed = secondMousePosition.y - firstMousePosition.y;
	
		//Sets max speed of the frisbee.
		if(speed >= 10000f)
		{
			speed = 10000f;
		}
		
//		Debug.Log ("Speed: " + speed);
	}
	
	public void curveSpeed()
	{
		//sets the curve of the frisbee.
		curve = secondMousePosition.x - firstMousePosition.x;
        if(curve > 0)
        {
            float x = Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, x * 10f, 0);
        }
		//Debug.Log ("Curve: " + curve);
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Target")
		{
			ResetFrisbee();
			score.AddScore();
			Metrics.hits++;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Floor")
		{
			ResetFrisbee();
			score.ShowGameOverUI();
			this.enabled = false;
			other.GetComponent<AudioSource>().Play();
		}
	}
	
	public void ResetFrisbee()
	{
        //trailRendererLol.enabled = false;
        transform.rotation = Quaternion.Euler(0,180,0);
		transform.position = startPos;
		rigidbodyLol.useGravity = false;
		rigidbodyLol.velocity = new Vector3(0,0,0);
	}
}
