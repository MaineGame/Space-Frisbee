using UnityEngine;
using System.Collections;

public class LerpCamera : MonoBehaviour {
    [SerializeField] Transform Target;
    [SerializeField] float RotationSpeed;
    [SerializeField] float time;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;
    

    void Awake()
    {
        Time.timeScale = 1;
    }

    public IEnumerator LookAtTarget(float lookTime)
    {
        while(lookTime >= 0f)
        {
            LerpCamera2Target();

            lookTime -= Time.fixedDeltaTime;

            yield return new WaitForSeconds(1f / 60f);
        }
    }


    void LerpCamera2Target()
    {

        //find the vector pointing from our position to the target
        _direction = (Target.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, 0.05f * RotationSpeed);

    }
    void LateUpdate()
    {
        Time.timeScale = time;
    }
}
