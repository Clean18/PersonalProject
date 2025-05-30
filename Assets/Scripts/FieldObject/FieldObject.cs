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
	public TMP_Text itemText;

	void Start()
	{
		DataTable.CachingFieldObject.Add(transform, this);

		if (itemText != null)
			itemText.text = DataTable.CachingString[interactType];

		if (textTransform != null)
			textTransform?.gameObject.SetActive(false);
	}

	void OnEnable()
	{
		if (itemText != null)
			itemText.text = DataTable.CachingString[interactType];
	}

	void Update()
	{
		if (textTransform != null && textTransform.gameObject.activeSelf)
		{
			textTransform.rotation = Camera.main.transform.rotation;
		}
	}

	public void SetText(bool enable)
	{
		// Destroy 예외처리
		if (textTransform == null || textTransform.gameObject == null)
			return;

		textTransform.gameObject.SetActive(enable);
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
