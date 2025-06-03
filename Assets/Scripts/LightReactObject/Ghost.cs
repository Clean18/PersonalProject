using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : LightReactObject
{
	public AudioSource audioSource;
	public AudioClip scareClip;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void ScarePlay()
	{
		audioSource.PlayOneShot(scareClip, DataTable.VFXValue);
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
