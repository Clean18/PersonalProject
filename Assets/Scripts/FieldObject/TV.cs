using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TV : FieldObject
{
	bool isOn = true;
	public bool IsOn
	{
		get { return isOn; }
		set
		{
			if (isOn == value) return;

			if (isOn)
			{
				isOn = value;
				OffTV();
			}
			else
			{
				isOn = value;
				OnTV();
			}
		}
	}

	public AudioSource audioSource;
	public AudioClip noiseClip;
	public GameObject noiseMonitor;

	protected override void Start()
	{
		base.Start();

		interactType = InteractType.OnTV;
		if (audioSource == null) audioSource = GetComponent<AudioSource>();

		audioSource.outputAudioMixerGroup = DataTable.VFXGroup;

		if (isOn)
			OnTV();
		else
			OffTV();
	}

	public void OnTV()
	{
		interactType = InteractType.OnTV;
		noiseMonitor.SetActive(true);
		audioSource.clip = noiseClip;
		audioSource.loop = true;
		audioSource.Play();
	}

	public void OffTV()
	{
		// 노이즈 끄기
		// 사운드 종료
		interactType = InteractType.OffTV;
		noiseMonitor.SetActive(false);
		audioSource.Stop();
	}
}
