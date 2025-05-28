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
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
			SetMouseHold(true);
			
	}

	void FixedUpdate()
	{
		Move();
	}

	public void Look()
	{
		Vector2 lookInput = GetLookInput();

		// 처음 0f 에서 상하
		data.rotationX -= lookInput.y;
		data.rotationX = Mathf.Clamp(data.rotationX, -90f, 90f);

		// 수직
		data.cameraTransform.localRotation = Quaternion.Euler(data.rotationX, 0, 0);

		// 수평
		Quaternion deltaRotation = Quaternion.Euler(0f, lookInput.x, 0f);
		data.rigid.MoveRotation(data.rigid.rotation * deltaRotation);
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

		Vector3 moveInput = GetMoveInput();
		Vector3 moveDir = transform.right * moveInput.x + transform.forward * moveInput.z;
		
		// 바닥의 경사에 따라 이동벡터 보정
		Vector3 fixedDir = IsSlope(out RaycastHit hit) ? Vector3.ProjectOnPlane(moveDir, hit.normal).normalized : moveDir;

		Vector3 moveDelta = fixedDir * data.moveSpeed * Time.fixedDeltaTime;
		data.rigid.MovePosition(data.rigid.position + moveDelta);
	}


	bool IsSlope(out RaycastHit hit)
	{
		// 바닥으로 레이쏴서 언덕 구분
		Vector3 origin = transform.position + Vector3.up;
		if (Physics.Raycast(origin, Vector3.down, out hit, 1.5f, data.groundLayer))
		{
			float angle = Vector3.Angle(hit.normal, Vector3.up); // 바닥의 기울기 각도
			return angle > 0.1f; // 경사도 0.1도 이상이면 경사로 간주
		}
		return false;
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

	public void SetMouseHold(bool enable)
	{
		data.mouseHold = enable;
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
