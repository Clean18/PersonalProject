using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public FieldObject fieldObject;

	public Quaternion doorOpen;
	public Quaternion doorClose;
	public float doorSpeed = 3f;
	public float openAngle = 90;
	public bool isMoving = false;
	
	public InteractType DoorState
	{
		get
		{
			return fieldObject.interactType;
		}
		set
		{
			if (isMoving)
				return;

			if (fieldObject.interactType == value)
				return;

			fieldObject.interactType = value;

			if (fieldObject.interactType == InteractType.OpenDoor)
			{
				GameEvent.OnInteract?.Invoke("OpenDoor");
			}
			else if (fieldObject.interactType == InteractType.CloseDoor)
			{
				GameEvent.OnInteract?.Invoke("CloseDoor");
			}
		}
	}

	void Start()
	{
		// 맨 처음은 닫힌 상태
		fieldObject = GetComponent<FieldObject>();
		fieldObject.interactType = InteractType.CloseDoor;

		doorOpen = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y , transform.localEulerAngles.z + openAngle);
		doorClose = Quaternion.Euler(transform.localEulerAngles);
	}

	void OnEnable()
	{
		GameEvent.OnInteract += DoorEventHandler;
	}

	void OnDestroy()
	{
		GameEvent.OnInteract -= DoorEventHandler;
	}

	public void DoorEventHandler(string action)
	{
		if (isMoving)
			return;

		if (action == "OpenDoor")
		{
			// 문열림
			StartCoroutine(DoorMove(doorOpen));
		}
		else if (action == "CloseDoor")
		{
			// 문 닫힘
			StartCoroutine(DoorMove(doorClose));
		}
	}

	IEnumerator DoorMove(Quaternion target)
	{
		isMoving = true;
		while (Quaternion.Angle(transform.localRotation, target) > 0.5f)
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * doorSpeed);
			yield return null;
		}
		transform.localRotation = target;
		yield return null;
		isMoving = false;
	}
}
