using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCluesMythosModel : MVC
{
    public List<Clue> spawnedClues;
    public int cluesToSpawn;


    public void StartSpawningClues(int num)
    {
        cluesToSpawn = num;
        spawnedClues = new List<Clue>();
    }

    public void ClueSpawned(Clue c)
    {
        spawnedClues.Add(c);
        cluesToSpawn--;
    }
}
