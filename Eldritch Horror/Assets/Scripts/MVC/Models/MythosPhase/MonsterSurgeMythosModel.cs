using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSurgeMythosModel : MVC
{
    public Location gateSpawnLocation;
    public Monster singleMonsterSpawn;

    public Dictionary<Location, List<Monster>> surged;
    public List<Location> locationsToSurge;
    public int monstersSpawnedOnLocation;
    public int currentLocation;

    public void GateSpawned(Location l)
    {
        gateSpawnLocation = l;
    }

    public void SingleGateMonsterSpawned(Monster m)
    {
        singleMonsterSpawn = m;
    }

    public void StartSurge(List<Location> matchingGates)
    {
        locationsToSurge = new List<Location>(matchingGates);
        monstersSpawnedOnLocation = 0;
        currentLocation = 0;

        surged = new Dictionary<Location, List<Monster>>();
        foreach (Location l in matchingGates)
        {
            surged.Add(l, new List<Monster>());
        }
    }

    public void NextLocation()
    {
        currentLocation++;
        monstersSpawnedOnLocation = 0;
    }

    public void MonsterSpawnedOnSpace(Monster m, Location l)
    {
        monstersSpawnedOnLocation++;
        surged[l].Add(m);
    }
}
