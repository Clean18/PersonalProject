using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;

public class UITitle : MonoBehaviour
{
	// UI 오브젝트 미리 캐싱
	//public static Dictionary<string, GameObject> UICaching = new();

	// Camera
	public CinemachineBrain brain;
	public CinemachineVirtualCamera mainCamera;
	public CinemachineVirtualCamera startCamera;
	public CinemachineVirtualCamera settingsCamera;

	// 메인 UI
	public GameObject main;
	public Button startButton;
	public Button settingsButton;
	public Button exitButton;

	// 세팅 UI
	public GameObject settings;

	// 사운드
	public Slider soundSlider;
	public TMP_Text soundValueText;
	// 마우스 민감도
	public Slider sensitivitySlider;
	public TMP_Text sensitivityValueText;
	// Close 버튼
	public Button settingsCloseButton;

	// 문
	public Transform anchor;


	void Start()
	{
		CameraChange(null, mainCamera);

		// 세팅 이벤트
		startButton.onClick.AddListener(OnStart);
		settingsButton.onClick.AddListener(OnSettings);
		exitButton.onClick.AddListener(OnExit);

		soundSlider.onValueChanged.AddListener(OnSoundSliderEvent);
		sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderEvent);
		settingsCloseButton.onClick.AddListener(OnSettingsCloseEvent);

		// 초기값 세팅
		OnSoundSliderEvent(0.5f);
		//soundSlider.value = 0.5f;

		OnSensitivitySliderEvent(0.5f);
		//sensitivitySlider.value = 0.5f;
	}

	public void OnStart()
	{
		// 게임시작
		Debug.Log("게임시작");

		// 비활성화
		ToggleMain();

		CameraChange(mainCamera, startCamera);

		StartCoroutine(GameStart());
	}

	public void ToggleMain()
	{
		main.SetActive(!main.activeSelf);
	}

	public void ToggleSettings()
	{
		settings.SetActive(!settings.activeSelf);
	}

	public void OnSettings()
	{
		// 비활성화
		ToggleMain();

		// 카메라 변경
		CameraChange(mainCamera, settingsCamera);

		// 카메라 이동 후 활성화
		StartCoroutine(WaitBlend(ToggleSettings));
	}

	public void OnExit()
	{
		Debug.Log("게임 종료");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
#else
		Application.Quit(); // 빌드에서 게임 종료
#endif
	}

	public void OnSoundSliderEvent(float value)
	{
		float Value = value * 100;
		soundValueText.text = $"{(int)Value}";
		DataTable.SoundValue = value;
		soundSlider.value = value;
	}

	public void OnSensitivitySliderEvent(float value)
	{
		float Value = value * 100;
		sensitivityValueText.text = $"{(int)Value}";
		DataTable.Sensitivity = Value;
		sensitivitySlider.value = value;
	}

	public void OnSettingsCloseEvent()
	{
		// 비활성화
		ToggleSettings();

		// 카메라 변경
		CameraChange(settingsCamera, mainCamera);

		// 카메라 이동 후 활성화
		StartCoroutine(WaitBlend(ToggleMain));
	}

	public void CameraChange(CinemachineVirtualCamera prev, CinemachineVirtualCamera next)
	{
		if (prev != null)
			prev.Priority = 10;

		if (next != null)
			next.Priority = 20;
	}

	IEnumerator WaitBlend(Action callback)
	{
		yield return null;
		while (brain.IsBlending)
			yield return null;
		callback?.Invoke();
	}

	IEnumerator GameStart()
	{
		yield return null;
		
		Image blackImage = GetComponent<Image>();
		Color color = blackImage.color;

		float duration = 2f;
		float time = 0f;

		yield return new WaitForSeconds(1f);

		// 문닫기
		float startZ = anchor.localEulerAngles.z;
		float targetZ = 360f;

		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;
			float z = Mathf.Lerp(startZ, targetZ, t);

			anchor.localRotation = Quaternion.Euler(0, 0, z);

			yield return null;
		}
		anchor.localRotation = Quaternion.Euler(0, 0, targetZ);

		yield return null;

		// Fade Out
		time = 0f;

		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;

			color.a = Mathf.Lerp(0f, 1f, t); // 점점 불투명
			blackImage.color = color;

			yield return null;
		}

		color.a = 1f;
		blackImage.color = color;

		Debug.Log("씬 전환");
		// 씬 비동기 로딩
		AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");
		operation.allowSceneActivation = false;
		yield return new WaitUntil(() => operation.progress >= 0.9f);
		yield return new WaitForSeconds(1f);
		operation.allowSceneActivation = true;
	}
}
