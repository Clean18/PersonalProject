using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light flashLight;
	public float minDelay = 0.3f;
	public float maxDelay = 3.0f;

	Coroutine flickerCo;

	void Awake()
	{
		flashLight = GetComponent<Light>();
	}

	void OnEnable()
	{
		flickerCo = StartCoroutine(Flicker());
	}

	void OnDisable()
	{
		StopCoroutine(flickerCo);
	}

	IEnumerator Flicker()
	{
		// 랜덤으로 깜빡이기
		while (true)
		{
			float waitTime = Random.Range(minDelay, maxDelay);
			yield return new WaitForSeconds(waitTime);

			flashLight.enabled = !flashLight.enabled;
		}
	}
}
