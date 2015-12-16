using UnityEngine;
using System.Collections;

public class IM_TargetSpawn : MonoBehaviour
{

    private AudioSource audio;
    private Animation anim;


    [SerializeField]
    Transform target;
    [SerializeField]
    float RotationSpeed = 4;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private float _angle;

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
            audio.Play();
            anim.Play("Target_DOWN");
            StartCoroutine(WaitAndSpawn(1F));
        }
    }

    void ResetSpawn()
    {
        transform.parent.position = new Vector3(Random.Range(0,10), 0, Random.Range(-10, 10));
        transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 3.4f, Camera.main.transform.position.z));
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
        //transform.rotation = Quaternion.
        anim.Play("Target_UP");
        

    }

    IEnumerator WaitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetSpawn();

    }
}
