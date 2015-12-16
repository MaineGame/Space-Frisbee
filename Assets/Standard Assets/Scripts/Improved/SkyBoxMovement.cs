using UnityEngine;
using System.Collections;

public class SkyBoxMovement : MonoBehaviour {

    [SerializeField] float rotationSpeed;
	
	void LateUpdate () {
        rotationSpeed += 0.25f * Time.deltaTime;
        GetComponent<Skybox>().material.SetFloat("_Rotation", rotationSpeed);
	}
}
