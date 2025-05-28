using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
	None, TV, Test
}

public class FieldObject : MonoBehaviour, IInteractable
{
	public InteractType interactType;


	public void Use(PlayerData data)
	{
		switch (interactType)
		{
			case InteractType.Test:
				Debug.Log("상호작용 테스트");
				break;

			default:
				Debug.Log("상호작용할 수 없는 오브젝트");
				break;
		}
	}
}
