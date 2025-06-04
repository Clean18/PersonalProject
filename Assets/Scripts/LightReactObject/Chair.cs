using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : LightReactObject
{
	public AudioSource audioSourece;
	public AudioClip scareClip;
	Coroutine chairCo;

	void Start()
	{
		if (audioSourece == null) audioSourece = GetComponent<AudioSource>();
		audioSourece.outputAudioMixerGroup = DataTable.VFXGroup;
	}

	protected override void HandleLightOff()
	{
		// 불이 꺼져있을 때만 의자 Roation.x 가 -6 ~ 12 왓다갔다하면서 소리냄 끼익끼익
		if (chairCo == null)
			chairCo = StartCoroutine(Move());
			
	}

	protected override void HandleLightOn()
	{
		if (chairCo != null)
		{
			StopCoroutine(chairCo);
			chairCo = null;
		}
	}

	IEnumerator Move()
	{
		float minX = -6f;
		float maxX = 12f;
		float speed = 20f;

		bool toMax = true;

		while (true)
		{
			float targetX = toMax ? maxX : minX;
			float currentX = transform.localEulerAngles.x;
			currentX = (currentX > 180f) ? currentX - 360f : currentX;

			// 회전
			float newX = Mathf.MoveTowards(currentX, targetX, speed * Time.deltaTime);
			transform.localEulerAngles = new Vector3(newX, transform.localEulerAngles.y, transform.localEulerAngles.z);

			// 도달했으면 반대로
			if (Mathf.Approximately(newX, targetX))
			{
				toMax = !toMax;

				// 소리 재생
				if (!audioSourece.isPlaying)
					audioSourece.PlayOneShot(scareClip);
				yield return new WaitForSeconds(0.3f);
			}

			yield return null;
		}
	}
}
