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
			Debug.Log("캐비넷 트리거 인");
			// 첫 트리거: 문 열기
			foreach (CabinetDoor value in DataTable.CachingCabinet)
			{
				value.OpenDoor();
			}
			CanFirstTrigger = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Player"))
			return;

		if (CanSecondTrigger && DataTable.IsLight)
		{
			Debug.Log("캐비넷 트리거 아웃");
			// 두 번째 트리거: 문 닫고 불끄기
			foreach (CabinetDoor value in DataTable.CachingCabinet)
			{
				value.CloseDoor();
			}
			SkyboxChanger.SetDarkSkybox();
			CanSecondTrigger = false;
		}
	}
}
