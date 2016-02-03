using UnityEngine;
using System.Collections;

public class IM_FlickyFinger : MonoBehaviour {

	Vector3 worldMousePosition,firstMousePosition, secondMousePosition, startPos;
	float speed;
    float curve;
    int noDecimals;

    public UI_Manager stopFinger, score;
	Rigidbody rb;
    AudioSource sound;
    Animator anim;
    Transform tf;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        anim = transform.GetComponentInParent<Animator>();
        tf = GetComponent<Transform>();
    }

	void Start () 
	{
        startPos = transform.localPosition;
    }
	
	void Update () 
	{
        //tf.position += new Vector3(0, 0.5f, 0);
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
                    //transform.parent = null;

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
		speed = (secondMousePosition.y - firstMousePosition.y) / Screen.height * 1750;

		if(speed >= 10000f)
		{
			speed = 10000f;
		}
	}
	
	void curveSpeed()
	{
		curve = (secondMousePosition.x - firstMousePosition.x) / Screen.width * 1000; 
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Target")
		{
			ResetFrisbee();
			score.AddScore();
			Metrics.hits++;
		}
        if (other.gameObject.tag == "Floor")
        {
            ResetFrisbee();
            score.ShowGameOverUI();
            //other.GetComponent<AudioSource>().Play();
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

	[SerializeField]
	Material whiteOutlineMaterial;

	private void physicsbree() {
		  
		//make a new instance of this physics frisbee dealio
		GameObject boop = Instantiate (physicsFrisbee);
		//bring it here
		boop.transform.position = new Vector3 (gameObject.transform.position.x,
		                                       gameObject.transform.position.y,
		                                       gameObject.transform.position.z);
		boop.transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 0));
		boop.GetComponent<Rigidbody> ().velocity = gameObject.GetComponent<Rigidbody> ().velocity;
		//add some force for amusement
		boop.GetComponent<Rigidbody> ().AddTorque (new Vector3 (
			Random.Range(50, 100),
			Random.Range(50, 100),
			Random.Range(50, 100)
		));
		boop.GetComponent<Rigidbody> ().AddForce  (Camera.main.transform.forward * Random.Range(200, 500));

		boop.transform.GetChild (0).GetChild (0).gameObject.GetComponent<Renderer> ().material = whiteOutlineMaterial;
		boop.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Renderer>().material = whiteOutlineMaterial;

		//boop.GetComponentInChildren<Renderer>().material.SetColor("Outline Color", Color.white);
		//boop.GetComponentInChildren<Renderer>()[1].material.SetColor("Outline Color", Color.white);
	}

	public GameObject physicsFrisbee;
	public void ResetFrisbee()
	{
		physicsbree ();

        anim.SetTrigger("ZoomOutCam");
        transform.parent = Camera.main.transform;
        transform.localPosition = startPos;
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        rb.useGravity = false;
	    rb.velocity = new Vector3(0,0,0);
	}
}
