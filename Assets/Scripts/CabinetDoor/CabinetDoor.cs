using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorAxis
{
	X, Y, Z
}

public class CabinetDoor : MonoBehaviour
{
	public bool isOpenPlus = true;
	public float openAngle = 90f;
	public float doorSpeed = 5f;
	public DoorAxis axis = DoorAxis.Y;

	public Quaternion doorOpen;
	public Quaternion doorClose;
	public bool isMoving = false;

	public AudioSource audioSource;
	public AudioClip openClip;
	public AudioClip closeClip;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
			OpenDoor();

		if (Input.GetKeyDown(KeyCode.P))
			CloseDoor();
	}

	void Start()
	{
		DataTable.CachingCabinet.Add(this);

		if (audioSource == null) audioSource = GetComponent<AudioSource>();
		audioSource.outputAudioMixerGroup = DataTable.VFXGroup;

		doorClose = Quaternion.Euler(0f, 0f, 0f);

		float direction = isOpenPlus ? 90f : -90f;

		Vector3 openEuler = Vector3.zero;
		switch (axis)
		{
			case DoorAxis.X:
				openEuler = new Vector3(direction, 0f, 0f);
				break;
			case DoorAxis.Y:
				openEuler = new Vector3(0f, direction, 0f);
				break;
			case DoorAxis.Z:
				openEuler = new Vector3(0f, 0f, direction);
				break;
		}

		doorOpen = Quaternion.Euler(openEuler);
	}

	public void OpenDoor()
	{
		if (!isMoving)
		{
			audioSource.PlayOneShot(openClip);
			StartCoroutine(MoveDoor(doorOpen));
		}
	}

	public void CloseDoor()
	{
		if (!isMoving)
		{
			audioSource.PlayOneShot(closeClip);
			StartCoroutine(MoveDoor(doorClose));
		}
	}

	private IEnumerator MoveDoor(Quaternion target)
	{
		isMoving = true;
		while (Quaternion.Angle(transform.localRotation, target) > 0.5f)
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * doorSpeed);
			yield return null;
		}
		transform.localRotation = target;
		isMoving = false;
	}
}

