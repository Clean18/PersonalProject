using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material darkSkybox;
    public Material lightSkybox;

	void Start()
	{
		//SetDarkSkybox();	// 어둡게
		SetLightSkybox();	// 밝게
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			Debug.Log("상태 변경");
			if (RenderSettings.skybox == darkSkybox)
			{
				SetLightSkybox();
			}
			else
			{
				SetDarkSkybox();
			}
		}
	}

	public void SetDarkSkybox()
	{
		DataTable.OffLights();

		RenderSettings.skybox = darkSkybox;
		RenderSettings.ambientIntensity = 0f;
		RenderSettings.reflectionIntensity = 0f;
		RenderSettings.subtractiveShadowColor = Color.black;
		DynamicGI.UpdateEnvironment();
	}

	public void SetLightSkybox()
	{
		DataTable.OnLights();

		RenderSettings.skybox = lightSkybox;
		RenderSettings.ambientIntensity = 1f; // 필요에 따라 조절
		RenderSettings.reflectionIntensity = 1f;
		RenderSettings.subtractiveShadowColor = Color.gray; // 밝은 그림자 느낌
		DynamicGI.UpdateEnvironment();
	}
}
