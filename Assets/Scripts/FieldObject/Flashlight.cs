using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : FieldObject
{
	public Light flashlight;
	public Transform cameraTransform;

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

	protected override void Start()
	{
		base.Start();
		interactType = InteractType.Flashlight;
	}

	void Update()
	{
		//if (textTransform != null && textTransform.gameObject.activeSelf)
		//{
		//	Debug.Log("플레이어 바라보기");
		//	textTransform.rotation = Camera.main.transform.rotation;
		//}

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
		if (sender != this || key != "Flashlight" || !IsPickUp)
			return;

		flashlight.enabled = !flashlight.enabled;
	}

	void HandlePickup(string key, FieldObject sender)
	{
		if (sender != this || key != "Flashlight")
			return;

		// TODO : 손전등 획득 ui 띄워야할듯
	}

	public void SetPickup()
	{
		IsPickUp = true;
		gameObject.SetActive(true);
		flashlight.enabled = false;
		DataTable.PlayerData.interactText.enabled = false;
		Debug.Log("손전등 획득");
	}
}
