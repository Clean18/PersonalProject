using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvent
{
    public static Action<string, FieldObject> OnToggle;
    public static Action<string, FieldObject> OnPickup;
    public static Action<string, FieldObject> OnInteract;
}
