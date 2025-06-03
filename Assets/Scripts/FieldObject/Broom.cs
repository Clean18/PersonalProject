using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : FieldObject
{
	public AudioSource audioSource;
	public AudioClip scareClip;
	public bool isPlay;

	protected override void Start()
	{
		base.Start();

		if (audioSource == null)
			audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (isPlay)
		{
			ScarePlay();
			isPlay = false;
		}

		Debug.DrawRay(transform.position, Camera.main.transform.position - transform.position, Color.red, 1f);
		
	}

	public bool RayCheck()
	{


		return false;
	}

	public void ScarePlay()
	{
		// 조건
		// 불 off
		// 레이캐스트에 플레이어가 닿았을때
		// 플레이어가 한번 바라보고 등져있을 때

		// 현재 각도에서 x85까지 이동후 소리 재생
		StartCoroutine(Move());
	}

	IEnumerator Move()
	{
		Quaternion currentRot = transform.localRotation;
		Quaternion tartgetRot = Quaternion.Euler(85, 90, 0);

		float time = 0f;
		float duration = 1f;

		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;

			transform.rotation = Quaternion.Lerp(currentRot, tartgetRot, t);

			yield return null;
		}

		transform.localRotation = tartgetRot;

		yield return null;
		audioSource.PlayOneShot(scareClip, DataTable.VFXValue);
	}
}
