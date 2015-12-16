using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour {

	public Color lerpedColor = Color.white;
	public Component[] imageColor;
	
	void Start()
	{
		imageColor = GetComponentsInChildren<Image>();
	}
	
	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	public float duration = 1.0F;
	
	void Update() 
	{
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		lerpedColor = Color.Lerp(colorStart, colorEnd, lerp);
		foreach (Image image in imageColor)
		{
			image.color = lerpedColor;
		}
	}
}
