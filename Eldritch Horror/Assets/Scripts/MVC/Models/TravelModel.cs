using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelModel : MVC
{
    public List<EventAction> travelActionCallBacks;
    public int callBackIndex = -1;

    void Start()
    {
        travelActionCallBacks = new List<EventAction>();
        callBackIndex = -1;
    }
}
