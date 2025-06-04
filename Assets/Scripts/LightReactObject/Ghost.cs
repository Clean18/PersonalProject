using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : LightReactObject
{
	public AudioSource audioSource;
	public AudioClip scareClip;

	public bool isLooked;
	public bool isPlayerInToilet;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.outputAudioMixerGroup = DataTable.VFXGroup;
	}

	void Update()
	{
		Debug.DrawRay(transform.position, transform.forward, Color.red, 1f);

		if (isPlayerInToilet			// 플레이어가 화장실에 들어갔는지
			&& gameObject.activeSelf	// 활성화 상태일 때
			&& !DataTable.IsLight		// 전체 조명이 꺼져있을 때
			&& DataTable.PlayerData.flashLight.flashlight.enabled)	// 손전등이 켜져있을 때
			ScarePlay();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Ghost : 플레이어 인");
			isPlayerInToilet = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Ghost : 플레이어 아웃");
			isPlayerInToilet = false;
		}
	}

	public void ScarePlay()
	{
		Transform cam = Camera.main.transform;
		Vector3 toPlayer = cam.position - transform.position;

		float angle = Vector3.Angle(cam.forward, toPlayer.normalized);

		float distance = toPlayer.magnitude;

		// 거울	35도 이하
		// 유령 150도 이상
		bool inRange = distance <= 7f;
		bool isLooking = angle > 35f;
		bool isBackTurned = angle < 150f;

		// 조건 3. 거울봤다가 뒤돌아봤을 때

		if (!isLooked && inRange && isLooking)
		{
			Debug.Log("Ghost : 범위 안 isLooked = true");
			isLooked = true;
			return;
		}

		if (isLooked && inRange && isBackTurned)
		{
			// 거울보고 다시 쳐다보면 소리나고 사라짐
			Debug.Log("Ghost : 유령 사라짐");
			audioSource.PlayOneShot(scareClip);
			transform.position = new Vector3(transform.position.x, transform.position.y + 30, transform.position.z);
		}

	}

	protected override void HandleLightOn()
	{
		gameObject.SetActive(false);
	}

	protected override void HandleLightOff()
	{
		gameObject.SetActive(true);
	}
}
