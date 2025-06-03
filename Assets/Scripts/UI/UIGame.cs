using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

	void Start()
	{
		sfxSlider.onValueChanged.AddListener(OnSFXSliderEvent);
		vfxSlider.onValueChanged.AddListener(OnVFXSliderEvent);
		sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderEvent);
		settingsCloseButton.onClick.AddListener(OnSettingsCloseEvent);

		// 초기값 세팅
		OnSFXSliderEvent(DataTable.SFXValue);
		OnVFXSliderEvent(DataTable.VFXValue);
		OnSensitivitySliderEvent(DataTable.Sensitivity/ 100);

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
		DataTable.SFXValue = value;
		sfxSlider.value = value;
	}

	public void OnVFXSliderEvent(float value)
	{
		float Value = value * 100;
		vfxValueText.text = $"{(int)Value}";
		DataTable.VFXValue = value;
		vfxSlider.value = value;
	}

	public void OnSensitivitySliderEvent(float value)
	{
		// 최소치
		value = Mathf.Max(value, 0.01f);

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

		// TODO : 티비 노이즈 지지직소리

		yield return new WaitForSeconds(0.1f); // 임시로 1초, 3초로 변경하기

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
}
