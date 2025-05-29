using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;

public enum InteractType
{
	None, TV, Flashlight, Book, 
}

public class FieldObject : MonoBehaviour, IInteractable
{
	public InteractType interactType;
	public Transform textTransform;
	public TMP_Text itemText;

	void Start()
	{
		DataTable.CachingFieldObject.Add(transform, this);

		itemText.text = DataTable.CachingString[interactType];
		textTransform.gameObject.SetActive(false);
	}

	void Update()
	{
		if (textTransform.gameObject.activeSelf)
		{
			textTransform.rotation = Camera.main.transform.rotation;
		}
	}

	public void SetText(bool enable)
	{
		textTransform.gameObject.SetActive(enable);
	}

	public void Use(PlayerData data)
	{
		switch (interactType)
		{
			case InteractType.Flashlight:

				Debug.Log("손전등 획득");
				// 플레이어 손전등 활성화
				data.flashLight.SetPickup();

				// 필드에 있던 손전등 파괴
				gameObject.SetActive(false);
				Destroy(gameObject);
				break;

			default:
				Debug.Log("상호작용할 수 없는 오브젝트");
				break;
		}
	}
}
