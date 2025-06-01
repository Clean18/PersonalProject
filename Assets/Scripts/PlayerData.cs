using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public Rigidbody rigid;
	public Animator anim;

	public Flashlight flashLight;

	[SerializeField] public float moveSpeed;
	[SerializeField] public float mouseSensitivity;
	[SerializeField] public bool canMove = true;
	[SerializeField] public Transform cameraTransform;
	[SerializeField] public bool mouseHold = true;
	[SerializeField] public Vector2 mouseDir;
	[SerializeField] public float rotationX = 0f;
	[SerializeField] public LayerMask groundLayer;
	[SerializeField] public float interactRayDistance;
	[SerializeField] public FieldObject textTarget;

	void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();

		DataTable.PlayerData = this;
	}

	void Start()
	{
		// TODO : 테스트 후 false로 바꾸기
		flashLight.gameObject.SetActive(true);
		flashLight.SetPickup();
	}
}
