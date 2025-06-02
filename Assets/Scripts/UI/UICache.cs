using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICache : MonoBehaviour
{
	void Start()
	{
		UIMain.UICaching.TryAdd(gameObject.name, gameObject);
	}
}
