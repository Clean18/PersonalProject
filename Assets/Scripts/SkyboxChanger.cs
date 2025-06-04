using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material darkSkybox;
    public Material lightSkybox;

    public static Material DarkSkybox;
    public static Material LightSkybox;

	void Start()
	{
		DarkSkybox = darkSkybox;
		LightSkybox = lightSkybox;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			Debug.Log("상태 변경");
			if (RenderSettings.skybox == DarkSkybox)
			{
				SetLightSkybox();
			}
			else
			{
				SetDarkSkybox();
			}
		}
	}

	public static void SetDarkSkybox()
	{
		DataTable.IsLight = false;

		RenderSettings.skybox = DarkSkybox;
		RenderSettings.ambientIntensity = 0f;
		RenderSettings.reflectionIntensity = 0f;
		RenderSettings.subtractiveShadowColor = Color.black;
		DynamicGI.UpdateEnvironment();
	}

	public static void SetLightSkybox()
	{
		DataTable.IsLight = true;

		RenderSettings.skybox = LightSkybox;
		RenderSettings.ambientIntensity = 1f; // 필요에 따라 조절
		RenderSettings.reflectionIntensity = 1f;
		RenderSettings.subtractiveShadowColor = Color.gray; // 밝은 그림자 느낌
		DynamicGI.UpdateEnvironment();
	}
}
