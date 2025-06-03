using System.Reflection.Emit;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] PlayerData data;

	void Start()
	{
		// 플레이어 카메라 정면 바라보기
		data.cameraTransform.rotation = Quaternion.Euler(0, 0, 0);

		data.interactText.gameObject.SetActive(false);
	}

	void Update()
	{
		// 카메라 방향
		Look();

		// 에임 위치에 레이로 아이템 이름 표시
		ShowItemName();

		// 손전등 활성화 / 비활성화
		if (Input.GetKeyDown(KeyCode.R))
			GameEvent.OnToggle?.Invoke("Flashlight", data.flashLight);

		// 상호작용
		if (Input.GetKeyDown(KeyCode.E))
			Interact();
	}

	void FixedUpdate()
	{
		Move();
		data.rigid.angularVelocity = Vector3.zero;
	}

	public void Look()
	{
		Vector2 lookInput = GetLookInput();

		// 수직 : 카메라
		data.rotationX -= lookInput.y;
		data.rotationX = Mathf.Clamp(data.rotationX, -90f, 90f);
		data.cameraTransform.localRotation = Quaternion.Euler(data.rotationX, 0, 0);

		// 수평 : 플레이어
		float newY = transform.eulerAngles.y + lookInput.x;
		transform.rotation = Quaternion.Euler(0, newY, 0);
	}

	Vector2 GetLookInput()
	{
		float mouseX = 0;
		float mouseY = 0;
		if (Cursor.lockState == CursorLockMode.Locked)
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

		bool isWalking = moveInput != Vector3.zero;

		data.anim.SetBool("IsWalk", isWalking);
		data.anim.SetFloat("Z", moveInput.z);
		data.anim.SetFloat("X", moveInput.x);

		Vector3 forward = Vector3.ProjectOnPlane(data.cameraTransform.forward, Vector3.up).normalized;
		Vector3 right = Vector3.ProjectOnPlane(data.cameraTransform.right, Vector3.up).normalized;
		Vector3 moveDir = right * moveInput.x + forward * moveInput.z;

		// 바닥의 경사에 따라 이동벡터 보정
		Vector3 fixedDir = IsSlope(out RaycastHit hit) ? Vector3.ProjectOnPlane(moveDir, hit.normal).normalized : moveDir;

		// velocity
		Vector3 velocity = fixedDir * data.moveSpeed;
		velocity.y = data.rigid.velocity.y;

		data.rigid.velocity = velocity;
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

	Vector3 GetMoveInput()
	{
		float x = 0;
		float z = 0;
		if (Cursor.lockState == CursorLockMode.Locked)
		{
			x = Input.GetAxis("Horizontal");
			z = Input.GetAxis("Vertical");
		}
		return new Vector3(x, 0, z).normalized;
	}

	public void Interact()
	{
		Ray ray = new Ray(data.cameraTransform.position, data.cameraTransform.forward);
		Debug.DrawRay(ray.origin, ray.direction * data.interactRayDistance, Color.red, 2f);
		RaycastHit[] hits = Physics.RaycastAll(ray, data.interactRayDistance);
		foreach (RaycastHit hit in hits)
		{
			if (DataTable.CachingFieldObject.TryGetValue(hit.transform, out FieldObject obj))
				obj.Use(data);
		}
	}

	public void ShowItemName()
	{
		Ray ray = new Ray(data.cameraTransform.position, data.cameraTransform.forward);
		Debug.DrawRay(ray.origin, ray.direction * data.interactRayDistance, Color.green, 1f);
		if (Physics.Raycast(ray, out RaycastHit hit, data.interactRayDistance))
		{
			// 인터렉터블 오브젝트인지 체크
			if (hit.collider.CompareTag("FieldObject"))
			{
				FieldObject newTarget;

				if (DataTable.CachingFieldObject.TryGetValue(hit.transform, out newTarget))
				{
					if (data.textTarget != newTarget)
					{
						data.textTarget = newTarget;
						data.interactText.text = DataTable.CachingString[data.textTarget.interactType];
						data.interactText.gameObject.SetActive(true);
					}
					return;
				}
			}
		}

		if (data.textTarget != null)
		{
			data.textTarget = null;
			data.interactText.gameObject.SetActive(false);
		}
	}

	public void OnFootstepSound()
	{
		// 애니메이션 이벤트
		//Debug.Log("발걸음 재생");
		data.sfxSource.PlayOneShot(data.walkSound, DataTable.SFXValue);
	}
}
