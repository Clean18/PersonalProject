using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DataTable
{
	static float sfxValue;
	public static float SFXValue
	{
		get { return sfxValue; }
		set
		{
			sfxValue = value;
			//if (PlayerData != null)
			//	PlayerData.sfxSource.volume = sfxValue;
		}
	}

	static float vfxValue;
	public static float VFXValue
	{
		get { return vfxValue; }
		set
		{
			vfxValue = value;
		}
	}

	static float sensitivity;
	public static float Sensitivity
	{
		get { return sensitivity; }
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
		[InteractType.OpenDoor] = "E : 문 닫기",
		[InteractType.CloseDoor] = "E : 문 열기",
		[InteractType.TV] = "E : TV 끄기",
	};

	
	////////////////////////////////////////////////////////////////////////////////

	// 빛 오브젝트 딕셔너리
	public static Dictionary<Transform, LightController> CachingLight = new();

	static bool isLight;
	public static bool IsLight
	{
		get { return isLight; }
		set
		{
			if (isLight == value) return;

			isLight = value;

			foreach (var light in CachingLight.Values)
			{
				if (isLight)
					light.OnLight();
				else
					light.OffLight();
			}

			if (isLight)
				GameEvent.OnLightOn?.Invoke();
			else
				GameEvent.OnLightOff?.Invoke();
		}
	}
}
