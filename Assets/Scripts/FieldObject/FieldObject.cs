using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public enum InteractType
{
	None, Flashlight, Book, OpenDoor, CloseDoor, Mirror
}

public class FieldObject : MonoBehaviour, IInteractable
{
	public InteractType interactType;
	public Transform textTransform;

	protected virtual void Start()
	{
		// key : Transform
		// value : FiendObject
		DataTable.CachingFieldObject.Add(transform, this);

		if (textTransform != null)
			textTransform?.gameObject.SetActive(false);
	}

	public void SetText(bool enable)
	{
		// Destroy 예외처리
		if (textTransform == null || textTransform.gameObject == null)
			return;

		Debug.Log($"필드 오브젝트 텍스트 {(enable == true ? "활성화" : "비활성화")}");
		//textTransform.gameObject.SetActive(enable);

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

			default:
				Debug.Log("상호작용할 수 없는 오브젝트");
				break;
		}
	}
}
