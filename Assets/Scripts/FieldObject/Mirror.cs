using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

public class Mirror : MonoBehaviour
{
	public Transform playerTransform;
	public Transform mirrorTransform;
	public Camera mirrorCamera;
	public Light mirrorLight;

	void Start()
	{
		playerTransform = Camera.main.transform;
		mirrorLight.enabled = false;
	}

	void Update()
	{
		if (mirrorLight == null || DataTable.PlayerData == null)
			return;

		mirrorLight.enabled = DataTable.PlayerData.flashLight.flashlight.enabled;
	}

	void LateUpdate()
	{
		if (playerTransform == null || mirrorTransform == null || mirrorCamera == null)
			return;

		// 카메라 높이 지정
		mirrorCamera.transform.position = new Vector3(mirrorCamera.transform.position.x, playerTransform.position.y, mirrorCamera.transform.position.z);

		// 카메라가 거울 기준으로 반사된 플레이어를 바라보게
		// 플레이어의 방향을 거울 기준에서의 방향으로 변환
		Vector3 localDir = mirrorTransform.InverseTransformDirection(playerTransform.forward);

		// 방향을 반전(거울처럼)
		localDir.z *= -1f;

		// 변환된 방향벡터를 다시 월드 방향벡터로 변환
		Vector3 reflectedDir = mirrorTransform.TransformDirection(localDir);

		// 거울이 기울어져 있어도 거울 기준
		mirrorCamera.transform.rotation = Quaternion.LookRotation(reflectedDir, mirrorTransform.up);
	}
}

