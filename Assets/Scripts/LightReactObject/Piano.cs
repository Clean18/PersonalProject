using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : LightReactObject
{
	public AudioSource audioSource;
	public AudioClip scareClip;
	public AudioClip scareClip2;

	public bool CanFirst = true;

	void Start()
	{
		if (audioSource == null) audioSource = GetComponent<AudioSource>();
		audioSource.outputAudioMixerGroup = DataTable.VFXGroup;
	}

	protected override void HandleLightOff()
	{
		// 피아노 재생
		audioSource.loop = true;
		audioSource.clip = scareClip;
		audioSource.Play();

		// 플레이어가 가까워지면 종료
	}

	protected override void HandleLightOn()
	{
		audioSource.Stop();
		audioSource.loop = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && CanFirst)
		{
			HandleLightOn();
			CanFirst = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && !CanFirst)
		{
			audioSource.PlayOneShot(scareClip2);
		}
	}
}
