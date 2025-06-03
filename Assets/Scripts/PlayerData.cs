using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

	[SerializeField] public AudioSource sfxSource;
	[SerializeField] public AudioClip walkSound;
	[SerializeField] public AudioClip breathSound;


	void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		sfxSource = GetComponent<AudioSource>();

		DataTable.PlayerData = this;

		mouseSensitivity = DataTable.Sensitivity;
		// TODO : 임시코드
		DataTable.Sensitivity = 50f;
		DataTable.SFXValue = 0.6f;
		DataTable.VFXValue = 0.6f;
	}

	void Start()
	{
		// TODO : 테스트 후 false로 바꾸기
		flashLight.gameObject.SetActive(false);
		//flashLight.SetPickup();
	}
}
