using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : FieldObject
{
	public Light flashlight;
	public Transform cameraTransform;
	public TMP_Text gameInfoText;

	static bool isPickup;
	public bool IsPickUp
	{
		get => isPickup;
		set
		{
			isPickup = value;
			GameEvent.OnPickup?.Invoke("Flashlight", this);
		}
	}

	void Awake()
	{
		flashlight = GetComponentInChildren<Light>();
		cameraTransform = Camera.main.transform;
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

		GameEvent.OnPickup += HandlePickup;
		GameEvent.OnToggle += HandleToggle;
	}

	void OnDisable()
	{
		GameEvent.OnPickup -= HandlePickup;
		GameEvent.OnToggle -= HandleToggle;
	}

	void HandleToggle(string key, FieldObject sender)
	{
		if (sender != this)
			return;

		if (key != "Flashlight")
			return;

		if (!IsPickUp)
			return;

		flashlight.enabled = !flashlight.enabled;
	}

	void HandlePickup(string key, FieldObject sender)
	{
		if (sender != this)
			return;

		if (key != "Flashlight")
			return;

		// 필드 손전등은 참조안되어있음
		if (gameInfoText != null)
			gameInfoText.text = DataTable.CachingFlashlightText[isPickup];
	}

	public void SetPickup()
	{
		IsPickUp = true;
		gameObject.SetActive(true);
		flashlight.enabled = false;
		Debug.Log("손전등 획득");
	}
}
