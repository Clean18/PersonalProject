using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Mirror : MonoBehaviour
{
	public Transform playerTransform;
	public Transform mirrorTransform;
	public Camera mirrorCamera;
	public Light mirrorLight;
	public float rayDistance;
	public float maxAngleValue; // 0.2f

	void Start()
	{
		playerTransform = Camera.main.transform;
		mirrorLight.enabled = false;
	}

	void Update()
	{
		if (mirrorLight == null || DataTable.PlayerData == null)
			return;

		// 플레이어가 거울 앞이고 거울을 바라보고 있을 때 카메라 라이트 활성화
		var playerLight = DataTable.PlayerData.flashLight;
		var light = DataTable.PlayerData.flashLight.flashlight;
		bool flashOn = light.enabled;

		Vector3 lightDir = light.transform.forward;
		Vector3 toMirror = mirrorTransform.position - playerLight.transform.position;

		// Ray
		Vector3 rayOrigin = transform.position + toMirror.normalized * -0.5f;
		//Debug.DrawRay(rayOrigin, toMirror.normalized * -rayDistance, Color.red, 1f);
		if (Physics.Raycast(rayOrigin, toMirror.normalized * -1, out RaycastHit hit, rayDistance))
		{
			if (!hit.collider.CompareTag("Player"))
			{
				return;
			}
		}

		float dist = toMirror.magnitude;
		float angle = Vector3.Angle(lightDir, toMirror.normalized);

		// 손전등 Outer Spot Angle의 절반 기준
		float maxAngle = light.spotAngle * maxAngleValue;
		float maxDist = rayDistance;

		bool inRange = dist < maxDist;
		bool facing = angle < maxAngle;

		mirrorLight.enabled = flashOn && inRange && facing;
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

