using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public enum InteractType
{
	None, Flashlight, OpenDoor, CloseDoor, Mirror, TV
}

public class FieldObject : MonoBehaviour, IInteractable
{
	public InteractType interactType;

	protected virtual void Start()
	{
		// key : Transform
		// value : FiendObject
		DataTable.CachingFieldObject.Add(transform, this);
	}

	public void Use(PlayerData data)
	{
		switch (interactType)
		{
			case InteractType.Flashlight:
				// 플레이어 손전등 활성화
				data.flashLight.SetPickup();

				// 필드에 있던 손전등 파괴
				gameObject.SetActive(false);
				Destroy(gameObject);
				break;

			case InteractType.OpenDoor:
			case InteractType.CloseDoor:
				if (this is Door door)
				{
					if (door.DoorState == InteractType.CloseDoor)
						door.DoorState = InteractType.OpenDoor;
					else if (door.DoorState == InteractType.OpenDoor)
						door.DoorState = InteractType.CloseDoor;
				}
				break;

			case InteractType.TV:
				// 켜져있을 때만 종료
				if (this is TV tv && tv.IsOn) tv.IsOn = false;
				break;

			default:
				Debug.Log("상호작용할 수 없는 오브젝트");
				break;
		}
	}
}
