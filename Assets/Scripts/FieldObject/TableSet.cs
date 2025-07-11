﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSet : MonoBehaviour
{
	void Awake()
	{
		GameEvent.OnLightOn += OffGameObject;
		GameEvent.OnLightOff += OnGameObject;
	}

	void OnGameObject()
	{
		gameObject.SetActive(true);
	}

	void OffGameObject()
	{
		gameObject.SetActive(false);
	}
}
