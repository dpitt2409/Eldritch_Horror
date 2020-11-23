using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelModel : MVC
{
    public List<EventAction> travelActionCallBacks;
    public int callBackIndex = -1;

    public Connection currentPath;

    void Start()
    {
        travelActionCallBacks = new List<EventAction>();
        callBackIndex = -1;
    }

    public void SetPath(Connection c)
    {
        currentPath = c;
    }
}
