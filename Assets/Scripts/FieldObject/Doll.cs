using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : LightReactObject
{
	protected override void HandleLightOn()
	{
		gameObject.SetActive(true);
	}

	protected override void HandleLightOff()
	{
		gameObject.SetActive(false);
	}
}
