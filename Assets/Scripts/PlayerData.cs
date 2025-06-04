using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerData : MonoBehaviour
{
	public Rigidbody rigid;
	public Animator anim;

	// 손전등
	public Flashlight flashLight;

	[SerializeField] public float moveSpeed;
	[SerializeField] public float mouseSensitivity;
	[SerializeField] public bool canMove = true;
	[SerializeField] public Transform cameraTransform;
	[SerializeField] public Vector2 mouseDir;
	[SerializeField] public float rotationX = 0f;
	[SerializeField] public LayerMask groundLayer;
	[SerializeField] public float interactRayDistance;
	[SerializeField] public FieldObject textTarget;
	[SerializeField] public TMP_Text interactText;

	[SerializeField] public AudioSource audioSource;
	[SerializeField] public AudioClip walkSound;
	[SerializeField] public AudioClip breathSound;
	[SerializeField] public AudioClip interactSound;

	[SerializeField] public AudioMixer mixer;
	[SerializeField] public AudioMixerGroup sfxGroup;
	[SerializeField] public AudioMixerGroup vfxGroup;


	void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		DataTable.PlayerData = this;

		SoundInit();

		mouseSensitivity = DataTable.Sensitivity;
	}

	void Start()
	{
		flashLight.gameObject.SetActive(false);
	}

	void SoundInit()
	{
		DataTable.Mixer = mixer;
		DataTable.SFXGroup = sfxGroup;
		DataTable.VFXGroup = vfxGroup;

		audioSource.outputAudioMixerGroup = sfxGroup;

	}
}
