using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region CollectibleEvents
    public static UnityEvent onItemDropped = new UnityEvent();
    #endregion
}
public class CollectEvent : UnityEvent<int> { }