using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : FieldObject
{
	public AudioSource audioSource;
	public AudioClip scareClip;

	public static bool CanTrigger = true;
	public bool isLooked = false;
	public bool isInRange = false;

	public Transform cam;

	protected override void Start()
	{
		base.Start();

		if (audioSource == null)
			audioSource = GetComponent<AudioSource>();

		audioSource.outputAudioMixerGroup = DataTable.VFXGroup;

		cam = Camera.main.transform;
	}

	void Update()
	{
		// 1. 불꺼짐
		// 2. 영역안에 들어오고 빗자루를 쳐다봤을 때
		// 3. Exit 에서 등지면서 나가면 이벤트 실행
		if (!DataTable.IsLight && CanTrigger && isInRange)
		{
			Trigger();
		}
	}

	void Trigger()
	{
		Vector3 toPlayer = cam.position - transform.position;
		float angle = Vector3.Angle(cam.forward, toPlayer.normalized);

		bool isLooking = angle > 150f;

		if (!isLooked && isLooking)
		{
			Debug.Log("Broom : 빗자루 바라봄");
			isLooked = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && CanTrigger)
		{
			isInRange = true;
			Debug.Log("Broom : 범위 진입");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && CanTrigger)
		{
			isInRange = false;
			Debug.Log("Broom : 범위 벗어남");

			if (isLooked)
			{
				// 나갈 때 등돌리고 있으면 실행
				Vector3 toPlayer = cam.position - transform.position;
				float angle = Vector3.Angle(cam.forward, toPlayer.normalized);
				Debug.Log(angle);
				if (30f < angle && angle < 120f) // 등진 상태
				{
					Debug.Log("Broom : 이벤트 실행 (등짐)");
					StartCoroutine(Move());
					CanTrigger = false;
				}
			}
		}
	}

	IEnumerator Move()
	{
		Quaternion currentRot = transform.localRotation;
		Quaternion targetRot = Quaternion.Euler(85f, 90f, 0f);

		float time = 0f;
		float duration = 0.5f;

		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;

			transform.rotation = Quaternion.Lerp(currentRot, targetRot, t);
			yield return null;
		}

		transform.localRotation = targetRot;
		audioSource.PlayOneShot(scareClip);
	}
}