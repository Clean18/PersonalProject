using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class FloorTrigger : MonoBehaviour
{
	public PlayableDirector timeline;
	public bool CanTrigger = true;
	public AudioSource audioSource;
	public AudioClip scareClip;

	void Start()
	{
		audioSource.outputAudioMixerGroup = DataTable.VFXGroup;
	}

	void OnTriggerEnter(Collider other)
	{
		// 불꺼져있을때?

		if (!other.CompareTag("Player") || !CanTrigger)
			return;

		CanTrigger = false;
		Debug.Log("타임라인 재생");
		timeline.Play();
	}
}
