using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : FieldObject
{
	public Quaternion doorOpen;
	public Quaternion doorClose;
	public float doorSpeed = 3f;
	public float openAngle = 90;
	public bool isMoving = false;
	Collider coll;
	Collider doorColl;

	public bool isLock;

	public AudioSource audioSource;
	public AudioClip openClip;
	public AudioClip closeClip;
	public AudioClip lockClip;
	public AudioClip scareClip;

	public InteractType DoorState
	{
		get { return interactType; }
		set
		{
			if (isMoving || interactType == value)
				return;

			interactType = value;
			GameEvent.OnInteract?.Invoke(value.ToString(), this);
		}
	}

	protected override void Start()
	{
		base.Start();

		coll = GetComponent<Collider>();
		doorColl = transform.GetChild(0).GetComponent<Collider>();
		audioSource = GetComponent<AudioSource>();

		// 맨 처음은 닫힌 상태
		interactType = InteractType.CloseDoor;

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

	public void DoorEventHandler(string action, FieldObject sender)
	{
		if (sender != this || isMoving)
			return;

		if (isLock)
		{
			Debug.Log("못여는 문");
			audioSource.PlayOneShot(lockClip);
			return;
		}

		if (action == "OpenDoor")
		{
			// 문열림
			coll.isTrigger = true;
			doorColl.enabled = false;
			StartCoroutine(DoorMove(doorOpen));
			audioSource.PlayOneShot(openClip);
		}
		else if (action == "CloseDoor")
		{
			// 문 닫힘
			coll.isTrigger = false;
			doorColl.enabled = true;
			StartCoroutine(DoorMove(doorClose));
			audioSource.PlayOneShot(closeClip);
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
