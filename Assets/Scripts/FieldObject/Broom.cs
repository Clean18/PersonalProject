using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Broom : FieldObject
{
	public AudioSource audioSource;
	public AudioClip scareClip;

	public bool isLooked;
	public static bool CanTrigger = true;

	protected override void Start()
	{
		base.Start();

		if (audioSource == null)
			audioSource = GetComponent<AudioSource>();

		audioSource.outputAudioMixerGroup = DataTable.VFXGroup;
	}

	void Update()
	{
		if (!DataTable.IsLight && CanTrigger)
		{
			ScarePlay();
		}
	}

	public void ScarePlay()
	{
		// 조건
		// 불 off
		// 레이캐스트에 플레이어가 닿았을때
		// 플레이어가 한번 바라보고 등져있을 때

		Transform cam = Camera.main.transform;

		Vector3 toPlayer = cam.position - transform.position;
		float distance = toPlayer.magnitude;

		// 1. 범위 안에 있는지
		bool inRange = distance <= 7f;

		// 2. 시선이 나를 향하고 있는지 90도 이하 등짐 / 90도 이상 시야안
		float angle = Vector3.Angle(cam.forward, toPlayer.normalized);

		bool isLooking = angle > 90f;

		// 3. 시선이 완전히 반대인지 (등짐)
		bool isBackTurned = angle < 90f;

		if (!isLooked && inRange && isLooking)
		{
			Debug.Log("Bloom : 범위 안 isLooked = true");
			isLooked = true;
			return;
		}

		if (isLooked && !inRange && isBackTurned)
		{
			StartCoroutine(Move());
			CanTrigger = false;
		}
	}

	IEnumerator Move()
	{
		Debug.Log("Bloom 이벤트 실행");
		Quaternion currentRot = transform.localRotation;
		Quaternion tartgetRot = Quaternion.Euler(85, 90, 0);

		float time = 0f;
		float duration = 0.5f;

		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;

			transform.rotation = Quaternion.Lerp(currentRot, tartgetRot, t);

			yield return null;
		}
		yield return null;
		audioSource.PlayOneShot(scareClip);
		transform.localRotation = tartgetRot;
	}

}
