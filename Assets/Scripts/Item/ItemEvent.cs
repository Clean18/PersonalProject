using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ItemEvent
{
    public static Action<string> OnToggle;
    public static Action<string> OnPickup;
    public static Action<string> OnInteract;
}
