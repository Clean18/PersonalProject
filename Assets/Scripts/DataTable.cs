using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataTable
{
	// Update에서 GetComponent를 대신할 딕셔너리
	public static Dictionary<Transform, FieldObject> CachingFieldObject = new();

	// 필드 오브젝트에 에임 조준시 활성화시킬 텍스트의 string
	public static Dictionary<InteractType, string> CachingString = new()
	{
		[InteractType.Flashlight] = "E : 손전등 줍기",
		[InteractType.Book] = "E : 책 읽기",
	};

	public static Dictionary<bool, string> CachingFlashlightText = new()
	{
		[true] = "ESC : 메뉴\nE : 사용\nR : 손전등 ON/OFF",
		[false] = "ESC : 메뉴\nE : 사용\n "
	};
}
