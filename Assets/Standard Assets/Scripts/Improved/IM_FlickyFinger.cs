using UnityEngine;
using System.Collections;

public class IM_FlickyFinger : MonoBehaviour {

	Vector3 worldMousePosition,firstMousePosition, secondMousePosition, startPos;
	float speed; 
	float curve;

    public UI_Manager stopFinger, score;
	Rigidbody rb;
    AudioSource sound;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        anim = transform.GetComponentInParent<Animator>();

    }

	void Start () 
	{
        startPos = transform.localPosition;
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
				if(transform.parent == Camera.main.transform)
				{
                    anim.SetTrigger("ZoomInCam");
                    transform.parent = null;

                    // Gets the mouse position when the mouse button is released
                    secondMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					//Debug.Log ("Second Pos: " + secondMousePosition.x);
					
					moveSpeed();
					curveSpeed();

					rb.AddRelativeForce(curve * 1.5f, speed * 1.2f, speed * 3f);
					rb.useGravity = true;
					sound.Play ();
				}
			}
			
			if(Input.GetMouseButtonDown(1))
			{
				ResetFrisbee();
			}
//		}
	}

	void moveSpeed()
	{
		speed = secondMousePosition.y - firstMousePosition.y;

		if(speed >= 10000f)
		{
			speed = 10000f;
		}
	}
	
	void curveSpeed()
	{
		curve = secondMousePosition.x - firstMousePosition.x; 
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
			//other.GetComponent<AudioSource>().Play();
		}
	}
	
	public void ResetFrisbee()
	{
        anim.SetTrigger("ZoomOutCam");
        transform.parent = Camera.main.transform;
        transform.localPosition = startPos;
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        rb.useGravity = false;
	    rb.velocity = new Vector3(0,0,0);
	}
}
