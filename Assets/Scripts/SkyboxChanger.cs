using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material darkSkybox;
    public Material lightSkybox;

	void Start()
	{
		SetDarkSkybox();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			if (RenderSettings.skybox == darkSkybox)
				SetLightSkybox();
			else
				SetDarkSkybox();
		}
	}

	public void SetDarkSkybox()
	{
		RenderSettings.skybox = darkSkybox;
		RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
		RenderSettings.ambientIntensity = 0f;
		RenderSettings.reflectionIntensity = 0f;
		RenderSettings.subtractiveShadowColor = Color.black;
		DynamicGI.UpdateEnvironment();
	}

	public void SetLightSkybox()
	{
		RenderSettings.skybox = lightSkybox;
		RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
		RenderSettings.ambientIntensity = 1f; // 필요에 따라 조절
		RenderSettings.reflectionIntensity = 1f;
		RenderSettings.subtractiveShadowColor = Color.gray; // 밝은 그림자 느낌
		DynamicGI.UpdateEnvironment();
	}
}
