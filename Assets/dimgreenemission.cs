using UnityEngine;
using System.Collections;

public class dimgreenemission : MonoBehaviour {

	private float time = 1;
	[SerializeField]
	private GameObject[] partsToDim;
	private Material[] materials;

	// Use this for initialization
	void Start () {
		materials = new Material[partsToDim.Length];
		for (int i = 0; i < partsToDim.Length; i ++) {
			materials [i] = partsToDim [i].GetComponent<Renderer> ().material;
		}
	}

	// Update is called once per frame
	void Update () {
		time += .017f;
		foreach (Material material in materials) {
			material.SetColor ("_EmissionColor", new Color (0, 1f / (time), 0));
			if (time > 5) {
				//start to fizzle
				Destroy(gameObject);
			}
		}
	}
}
