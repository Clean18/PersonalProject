using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
	// 세팅 UI
	public GameObject settings;

	// SFX
	public Slider sfxSlider;
	public TMP_Text sfxValueText;
	// VFX
	public Slider vfxSlider;
	public TMP_Text vfxValueText;
	// 마우스 민감도
	public Slider sensitivitySlider;
	public TMP_Text sensitivityValueText;
	// Close 버튼
	public Button settingsCloseButton;
	// 게임종료 버튼
	public Button quitGameButton;

	public GameObject scareImage;

	// 오디오믹서
	public AudioMixer mixer;

	void Start()
	{
		DataTable.UIGame = this;

		sfxSlider.onValueChanged.AddListener(OnSFXSliderEvent);
		vfxSlider.onValueChanged.AddListener(OnVFXSliderEvent);
		sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderEvent);
		settingsCloseButton.onClick.AddListener(OnSettingsCloseEvent);

		quitGameButton.onClick.AddListener(OnExit);

		// 초기값 세팅
		OnSFXSliderEvent(DataTable.sfxValue);
		OnVFXSliderEvent(DataTable.vfxValue);

		OnSensitivitySliderEvent(DataTable.Sensitivity / 100);

		scareImage.SetActive(false);

		ToggleSettings();


		// 시작 연출
		StartCoroutine(GameStart());
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ToggleSettings();
		}
	}

	public void OnSFXSliderEvent(float value)
	{
		float Value = value * 100;
		sfxValueText.text = $"{(int)Value}";
		sfxSlider.value = value;

		mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
	}

	public void OnVFXSliderEvent(float value)
	{
		float Value = value * 100;
		vfxValueText.text = $"{(int)Value}";
		vfxSlider.value = value;

		mixer.SetFloat("VFXVolume", Mathf.Log10(value) * 20);
	}

	public void OnSensitivitySliderEvent(float value)
	{
		float Value = value * 100;
		sensitivityValueText.text = $"{(int)Value}";
		DataTable.Sensitivity = Value;
		sensitivitySlider.value= value;
	}

	public void OnSettingsOpenEvent()
	{
		ToggleSettings();
	}

	public void OnSettingsCloseEvent()
	{
		ToggleSettings();
		StartCoroutine(CursorHold());
	}

	public void ToggleSettings()
	{
		// 토글 후 상태
		bool isOpen = !settings.activeSelf;

		Debug.Log($"{(isOpen == true ? "세팅 활성화" : "세팅 비활성화")}");
		settings.SetActive(isOpen);

		Time.timeScale = isOpen ? 0f : 1f;

		Cursor.visible = isOpen;
		Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;

		// 사운드 토글
		mixer.SetFloat("MasterVolume", isOpen ? -80f : 0f);
	}

	IEnumerator CursorHold()
	{
		yield return null;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	IEnumerator GameStart()
	{
		yield return null;
		// 마우스 고정
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.None;

		Image blackImage = GetComponent<Image>();
		Color color = blackImage.color;

		float duration = 2f;
		float time = 0f;

		yield return new WaitForSeconds(3f);

		// 조명키기
		SkyboxChanger.SetLightSkybox();

		// 숨쉬는 소리 재생
		DataTable.PlayerData.audioSource.PlayOneShot(DataTable.PlayerData.breathSound);
		while (time < duration)
		{
			time += Time.deltaTime;
			float t = time / duration;

			color.a = Mathf.Lerp(1f, 0f, t); // 점점 불투명
			blackImage.color = color;

			yield return null;
		}

		color.a = 0f;
		blackImage.color = color;

		yield return null;

		// 마우스 고정
		StartCoroutine(CursorHold());
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
}
