using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGatesMythosModel : MVC
{
    public List<Location> gatesToSpawn;
    public List<Location> gatesSpawned;
    public List<Monster> monstersSpawned;

    public void SpawnedGates(List<Location> gates)
    {
        gatesToSpawn = gates;
        gatesSpawned = new List<Location>();
        monstersSpawned = new List<Monster>();
    }

    public void GateSpawned(Monster m)
    {
        Location l = gatesToSpawn[0];
        gatesToSpawn.RemoveAt(0);
        gatesSpawned.Add(l);
        monstersSpawned.Add(m);
    }
}
