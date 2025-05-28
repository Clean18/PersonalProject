using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// 필드 오브젝트와 E 키로 상호작용
    /// </summary>
    public void Use(PlayerData data);
}
