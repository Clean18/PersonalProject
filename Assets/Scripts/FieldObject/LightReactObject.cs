using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightReactObject : MonoBehaviour
{
	protected virtual void Awake()
	{
		GameEvent.OnLightOn += HandleLightOn;
		GameEvent.OnLightOff += HandleLightOff;
	}

	protected virtual void OnDestroy()
	{
		GameEvent.OnLightOn -= HandleLightOn;
		GameEvent.OnLightOff -= HandleLightOff;
	}

	protected abstract void HandleLightOn();
	protected abstract void HandleLightOff();
}
