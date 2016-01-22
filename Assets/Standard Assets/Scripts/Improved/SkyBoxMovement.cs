using UnityEngine;
using System.Collections;

public class SkyBoxMovement : MonoBehaviour {

    [SerializeField] float rotationSpeed;
	
    void Update()
    {
        rotationSpeed += 0.25f * Time.deltaTime;
    }
	void LateUpdate () {
        
        GetComponent<Skybox>().material.SetFloat("_Rotation", rotationSpeed);
	}
}
