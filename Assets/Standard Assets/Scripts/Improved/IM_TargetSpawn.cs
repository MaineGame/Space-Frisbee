using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IM_TargetSpawn : MonoBehaviour
{

    private AudioSource audio;
    private Animation anim;

	[SerializeField]
	GameObject physicsTargetStand;
    [SerializeField]
    Transform target;
    [SerializeField]
    Text hitScoreText;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private float _angle;
    [SerializeField] LerpCamera lc;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
        
        ResetSpawn();
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Frisbee")
        {
            //audio.Play();
            hitScoreText.transform.position = transform.position + new Vector3(0, 6, 0);
            StartCoroutine(Faded(1.0f, 1.0f));
			gameObject.GetComponentsInChildren<MeshRenderer> ()[0].enabled = false;
			gameObject.GetComponentsInChildren<SpriteRenderer> ()[0].enabled = false;

			GameObject boop = Instantiate(physicsTargetStand);
			boop.transform.position = gameObject.transform.parent.gameObject.transform.position;
//			boop.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			boop.transform.GetChild(0).gameObject.transform.rotation = gameObject.transform.rotation;
//			boop.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 100, 0));

			anim.Play("Target_DOWN");

            StartCoroutine(WaitAndSpawn(1F));
          
           
        }
    }

    void ResetSpawn()
    {
        StartCoroutine(Faded(0.0f, 1.0f));

        StartCoroutine(lc.LookAtTarget(1.0f));
        transform.parent.position = new Vector3(Random.Range(-3,10), 0, Random.Range(-10, 10));
        transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10f, Camera.main.transform.position.z));
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
        //transform.rotation = Quaternion.
		anim.Play("Target_UP");
		gameObject.GetComponentsInChildren<MeshRenderer> ()[0].enabled = true;
		gameObject.GetComponentsInChildren<SpriteRenderer> ()[0].enabled = true;

    }

    IEnumerator WaitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetSpawn();
    }

    IEnumerator Faded(float alphaValue, float time)
    {
        float alpha = hitScoreText.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color myColor = new Color(72/255f, 1f, 0f, Mathf.Lerp(alpha, alphaValue, t));
            hitScoreText.GetComponent<Text>().color = myColor;
            yield return null;
        }
    }
}
