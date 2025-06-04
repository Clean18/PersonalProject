using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTrigger : LightReactObject
{
	public AudioSource audioSource;
	public AudioClip scareClip;

	public bool CanTrigger = true;

	void Start()
	{
		if (audioSource == null) audioSource = GetComponent<AudioSource>();

		audioSource.outputAudioMixerGroup = DataTable.VFXGroup;
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Player") || !CanTrigger) return;

		CanTrigger = false;

		audioSource.PlayOneShot(scareClip);
	}

	protected override void HandleLightOff()
	{
		gameObject.SetActive(true);
	}

	protected override void HandleLightOn()
	{
		gameObject.SetActive(false);
	}
}
