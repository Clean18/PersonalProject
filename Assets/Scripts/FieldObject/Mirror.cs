using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Mirror : MonoBehaviour
{
	public Transform playerTransform;
	public Transform mirrorTransform;
	public Camera mirrorCamera;

	void Start()
	{
		if (DataTable.PlayerData != null)
			playerTransform = Camera.main.transform;


	}

	void LateUpdate()
	{
		if (playerTransform == null || mirrorTransform == null || mirrorCamera == null)
			return;

		// 카메라가 거울 기준으로 반사된 플레이어를 바라보게
		mirrorCamera.transform.LookAt(playerTransform);
	}
}

