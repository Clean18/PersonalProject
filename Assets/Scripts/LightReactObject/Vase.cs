using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : LightReactObject
{
	protected override void HandleLightOn()
	{
		// y 252
		transform.rotation = Quaternion.Euler(0, 252, 0);
	}

	protected override void HandleLightOff()
	{
		// y 108
		transform.rotation = Quaternion.Euler(0, 108, 0);
	}
}
