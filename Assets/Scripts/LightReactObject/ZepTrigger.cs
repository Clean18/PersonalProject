using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZepTrigger : LightReactObject
{
	public bool CanTrigger = true;
	bool isFocused = true; // 포커스 상태 기록

	public AudioClip scareClip;


	void Update()
	{
		if (Application.isFocused != isFocused)
		{
			isFocused = Application.isFocused;

			if (!Application.isFocused)
			{
				if (!DataTable.UIGame.settings.activeSelf)
				{
					Debug.Log("Zep : 알탭 Settings 활성화");
					DataTable.UIGame.ToggleSettings();
				}
			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		// 젭 찌르기 3번
		if (!DataTable.IsLight && CanTrigger)
			StartCoroutine(Zep());
	}

	protected override void HandleLightOff()
	{
		
	}

	protected override void HandleLightOn()
	{
		
	}

	IEnumerator Zep()
	{
		CanTrigger = false;
		AudioClip sound = DataTable.PlayerData.zepSound;
		AudioSource source = DataTable.PlayerData.audioSource;
		yield return null;
		// 찌르기 3번
		for (int i = 0; i < 3; i++)
		{
			source.PlayOneShot(sound);
			yield return new WaitForSeconds(0.3f);
		}

		// 세팅창 열릴 때까지 대기
		yield return new WaitUntil(() => DataTable.UIGame.settings.activeSelf);

		Debug.Log("Zep : Settings 열림");


		// 세팅창 닫힐 때까지 대기
		yield return new WaitUntil(() => !DataTable.UIGame.settings.activeSelf);

		Debug.Log("Zep : Settings 닫힘");
		source.PlayOneShot(scareClip);
		DataTable.UIGame.scareImage.SetActive(true);


		yield return new WaitForSeconds(3f);

		DataTable.UIGame.scareImage.SetActive(false);
	}
}
