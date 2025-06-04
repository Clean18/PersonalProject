using UnityEngine;

public class CabinetTrigger : MonoBehaviour
{
	public bool CanFirstTrigger = true;
	public bool CanSecondTrigger = true;

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player"))
			return;

		if (CanFirstTrigger && DataTable.IsLight)
		{
			// 첫 트리거: 문 열고 불 끔
			foreach (CabinetDoor value in DataTable.CachingCabinet)
			{
				value.OpenDoor();
			}
			SkyboxChanger.SetDarkSkybox();
			CanFirstTrigger = false;
		}
		else if (CanSecondTrigger && !DataTable.IsLight)
		{
			// 두 번째 트리거: 문 닫기
			foreach (CabinetDoor value in DataTable.CachingCabinet)
			{
				value.CloseDoor();
			}
			CanSecondTrigger = false;
		}
	}
}
