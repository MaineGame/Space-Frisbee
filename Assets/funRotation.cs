using UnityEngine;
using System.Collections;

public class funRotation : MonoBehaviour {

	[SerializeField]
	private Vector3 m_StartRotation;
	[SerializeField]
	private float m_XAmplitude = 0;
	[SerializeField]
	private float m_YAmplitude = 0;
	[SerializeField]
	private float m_ZAmplitude = 0;
	[SerializeField]
	private float m_XSpeed = 0;
	[SerializeField]
	private float m_YSpeed = 0;
	[SerializeField]
	private float m_ZSpeed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//TODO maybe use an animation for this?
		float x = Mathf.Sin(Time.time * m_XSpeed) * m_XAmplitude;
		float y = Mathf.Sin(Time.time * m_YSpeed) * m_YAmplitude;
		float z = Mathf.Sin(Time.time * m_ZSpeed) * m_ZAmplitude;
		x += m_StartRotation.x;
		y += m_StartRotation.y;
		z += m_StartRotation.z;
		Vector3 newRotation = new Vector3 (x, y, z);
		transform.rotation = Quaternion.Euler (newRotation);
		//transform
	}
}
