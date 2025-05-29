using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataTable
{
	public static Dictionary<Transform, FieldObject> CachingFieldObject = new();
	public static Dictionary<InteractType, string> CachingString = new()
	{
		[InteractType.Flashlight] = "E : 손전등 줍기",
		[InteractType.Book] = "E : 책 읽기",
	};
}
