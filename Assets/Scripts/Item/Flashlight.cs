using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
	public Transform cameraTransform;
	public bool isPickup = false;

	void Awake()
	{
		flashlight = GetComponentInChildren<Light>();
		cameraTransform = Camera.main.transform;
	}

	void Update()
	{
		if (!isPickup || !flashlight.enabled)
			return;
		
		transform.forward = cameraTransform.forward;
		transform.rotation = cameraTransform.rotation;
	}

	void OnEnable()
	{
		// 빛 끄기
		flashlight.enabled = false;

		ItemEvent.OnToggle += HandleToggle;
	}

	void OnDisable()
	{
		ItemEvent.OnToggle -= HandleToggle;
	}

	void HandleToggle(string key)
	{
		if (key != "Flashlight")
			return;

		if (!isPickup)
			return;

		flashlight.enabled = !flashlight.enabled;
	}

	public void SetPickup()
	{
		isPickup = true;
		gameObject.SetActive(true);
		flashlight.enabled = false;
	}
}
