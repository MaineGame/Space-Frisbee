using UnityEngine;
using System.Collections;

public class SkyBoxMovement : MonoBehaviour {

    [SerializeField] float rotation, rotationSpeed;
	
    void Update()
    {
        rotation += rotationSpeed * Time.deltaTime;
    }
	void LateUpdate () {
        
        GetComponent<Skybox>().material.SetFloat("_Rotation", rotation);
	}
}
