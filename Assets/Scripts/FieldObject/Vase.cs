using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{

	void Awake()
	{
		GameEvent.OnLightOn += OnFront;
		GameEvent.OnLightOff += OnBack;
	}

	void OnFront()
	{
		// y 252
		transform.rotation = Quaternion.Euler(0, 252, 0);
	}

	void OnBack()
	{
		// y 114f
		transform.rotation = Quaternion.Euler(0, 114, 0);
	}
}
