using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
	public Light fireLight;

	// 색온도
	public float targetKelvin;
	public float currentKelvin;

	public float minKelvin = 1500f;
	public float maxKelvin = 2000f;

	// 밝기
	public float targetIntensity;
	public float currentIntensity;

	public float minIntensity = 25f;
	public float maxIntensity = 35f;

	// 색
	public Color targetColor;
	public Color currentColor;
	public Gradient colorGradient;

	public float delay = 0.3f;		// 변화 간격
	public float lerpSpeed = 3f;    // 서서히 변하는 속도


	void Start()
	{
		fireLight.useColorTemperature = true;
		targetKelvin = currentKelvin = minKelvin;
		targetIntensity = currentIntensity = minIntensity;
		targetColor = currentColor = fireLight.color;

		StartCoroutine(UpdateFireColor());
	}

	IEnumerator UpdateFireColor()
	{
		while (true)
		{
			targetKelvin = Random.Range(minKelvin, maxKelvin);
			targetIntensity = Random.Range(minIntensity, maxIntensity);
			float t = Random.value; // 0 ~ 1
			targetColor = colorGradient.Evaluate(t);
			yield return new WaitForSeconds(delay);
		}
	}
	void Update()
	{
		if (fireLight == null || !fireLight.enabled)
			return;
		
		currentKelvin = Mathf.Lerp(currentKelvin, targetKelvin, Time.deltaTime * lerpSpeed);
		currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * lerpSpeed);
		currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * lerpSpeed);

		fireLight.colorTemperature = currentKelvin;
		fireLight.intensity = currentIntensity;
		fireLight.color = currentColor;
	}
}
