using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public Rigidbody rigid;
	public Animator anim;

	public Flashlight flashLight;
	public Transform flashlightTransform;

	[SerializeField] public float moveSpeed;
	[SerializeField] public float mouseSensitivity;
	[SerializeField] public bool canMove = true;
	[SerializeField] public Transform cameraTransform;
	[SerializeField] public bool mouseHold = true;
	[SerializeField] public Vector2 mouseDir;
	[SerializeField] public float rotationX = 0f;
	[SerializeField] public LayerMask groundLayer;
	[SerializeField] public float interactRayDistance;

	void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}

	void Start()
	{
		flashLight.gameObject.SetActive(false);
	}
}
