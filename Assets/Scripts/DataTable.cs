using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DataTable
{
	static float soundValue;
	public static float SoundValue { get { return soundValue; } set { soundValue = value; } }

	static float sensitivity;
	public static float Sensitivity
	{
		get
		{
			return sensitivity;
		}
		set
		{
			sensitivity = value;
			if (PlayerData != null)
				PlayerData.mouseSensitivity = value;
		}
	}

	// 플레이어
	public static PlayerData PlayerData;

	// Update에서 GetComponent를 대신할 딕셔너리
	public static Dictionary<Transform, FieldObject> CachingFieldObject = new();

	// 필드 오브젝트에 에임 조준시 활성화시킬 텍스트의 string
	public static Dictionary<InteractType, string> CachingString = new()
	{
		[InteractType.Flashlight] = "E : 손전등 줍기",
		[InteractType.Book] = "E : 책 읽기",
		[InteractType.OpenDoor] = "E : 문 닫기",
		[InteractType.CloseDoor] = "E : 문 열기",
	};

	// 빛 오브젝트 딕셔너리
	public static Dictionary<Transform, LightController> CachingLight = new();
	
	////////////////////////////////////////////////////////////////////////////////

	public static void OnLights()
	{
		foreach (var light in CachingLight)
		{
			light.Value.OnLight();
		}
		GameEvent.OnLightOn?.Invoke();
	}
	public static void OffLights()
	{
		foreach (var light in CachingLight)
		{
			light.Value.OffLight();
		}
		GameEvent.OnLightOff?.Invoke();
	}

	
}
