using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateModel : MVC
{
    public List<Location> gatePool;
    public List<Location> activeGates;
    public List<Location> gateDiscard;

    public Location currentClosingGate;

    void Start()
    {
        gatePool = new List<Location>();
        activeGates = new List<Location>();
        gateDiscard = new List<Location>();

        foreach(Location l in App.Model.locationModel.locations)
        {
            if (l.gate != GateColor.None)
            {
                gatePool.Add(l);
            }
        }
    }

    public List<Location> DrawGates(int numGates)
    {
        List<Location> gateLocations = new List<Location>();
        for (int i = 0; i < numGates; i++)
        {
            if (gatePool.Count == 0)
            {
                gatePool = new List<Location>(gateDiscard);
                gateDiscard.Clear();
            }
            if (gatePool.Count == 0) // Cannot spawn more gates
                return gateLocations;

            int index = Random.Range(0, gatePool.Count);
            Location l = gatePool[index];
            activeGates.Add(l);
            gatePool.RemoveAt(index);
            gateLocations.Add(l);
        }
        return gateLocations;
    }

    public void SpawnGateInLocation(Location l)
    {
        Location gate = null;
        foreach (Location g in gatePool)
        {
            if (g.locationName == l.locationName)
            {
                gate = g;
            }
        }
        if (gate != null)
        {
            activeGates.Add(gate);
            gatePool.Remove(gate);
        }
    }

    public void CloseGate(Location l)
    {
        activeGates.Remove(l);
        gateDiscard.Add(l);
    }

    public void SetClosingGate(Location l)
    {
        currentClosingGate = l;
    }
}
