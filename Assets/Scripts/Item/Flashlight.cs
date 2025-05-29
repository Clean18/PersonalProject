using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
	public Transform cameraTransform;
	public TMP_Text gameInfoText;

	public static bool isPickup;
	public static bool IsPickUp
	{
		get => isPickup;
		set
		{
			isPickup = value;
			ItemEvent.OnPickup?.Invoke("Flashlight");
		}
	}

	void Awake()
	{
		flashlight = GetComponentInChildren<Light>();
		cameraTransform = Camera.main.transform;

		ItemEvent.OnPickup += HandlePickup;
	}

	void Update()
	{
		if (!IsPickUp || !flashlight.enabled)
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

		if (!IsPickUp)
			return;

		flashlight.enabled = !flashlight.enabled;
	}

	void HandlePickup(string key)
	{
		if (key != "Flashlight")
			return;

		gameInfoText.text = DataTable.CachingFlashlightText[isPickup];
	}

	public void SetPickup()
	{
		IsPickUp = true;
		gameObject.SetActive(true);
		flashlight.enabled = false;
	}
}
