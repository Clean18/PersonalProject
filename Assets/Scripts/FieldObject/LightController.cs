using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
	public Light flashlight;

	void Start()
	{
		flashlight = GetComponent<Light>();
		if (!DataTable.CachingLight.ContainsKey(transform))
		{
			DataTable.CachingLight.Add(transform, this);
		}

		OnLight();

		GameEvent.OnLightOn += OnLight;
	}

	public void OnLight()
	{
		flashlight.enabled = true;
	}

	public void OffLight()
	{
		flashlight.enabled = false;
	}
}
