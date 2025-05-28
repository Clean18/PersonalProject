using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] PlayerData data;

	void Start()
	{
		SetMouseHold();
	}

	void Update()
	{
		Look();

		if (Input.GetKeyDown(KeyCode.Escape))
			SetMouseHold();
			
	}

	void FixedUpdate()
	{
		Move();
	}

	public void Look()
	{

		Vector2 lookInput = GetLookInput();

		data.rotationX -= lookInput.y;
		data.rotationX = Mathf.Clamp(data.rotationX, -90f, 90f);

		data.cameraTransform.localRotation = Quaternion.Euler(data.rotationX, 0, 0);
		transform.Rotate(Vector3.up * lookInput.x, Space.World);
	}

	private Vector2 GetLookInput()
	{
		float mouseX = 0;
		float mouseY = 0;
		if (data.mouseHold)
		{
			mouseX = Input.GetAxis("Mouse X") * data.mouseSensitivity * Time.deltaTime;
			mouseY = Input.GetAxis("Mouse Y") * data.mouseSensitivity * Time.deltaTime;
		}
		return new Vector2(mouseX, mouseY);
	}

	public void Move()
	{
		if (!data.canMove)
			return;

		Vector3 movementInput = GetMoveInput();

		Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.z;

		Vector3 currentVelocity = data.rigid.velocity;

		Vector3 targetVelocity = move.normalized * data.moveSpeed;
		data.rigid.velocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);
	}

	private Vector3 GetMoveInput()
	{
		float x = 0;
		float z = 0;
		if (data.mouseHold)
		{
			x = Input.GetAxis("Horizontal");
			z = Input.GetAxis("Vertical");
		}

		return new Vector3(x, 0, z);
	}

	public void SetMouseHold()
	{
		data.mouseHold = !data.mouseHold;
		if (data.mouseHold)
		{
			Cursor.lockState = CursorLockMode.Locked; // 마우스를 화면 중앙에 고정
			Cursor.visible = false;                   // 마우스 커서를 숨김
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
