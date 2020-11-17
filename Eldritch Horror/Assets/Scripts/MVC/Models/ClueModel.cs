using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueModel : MVC
{
    public List<Clue> cluePool;
    public List<Clue> activeClues;
    public List<Clue> clueDiscard;

    void Start()
    {
        cluePool = new List<Clue>();
        activeClues = new List<Clue>();
        clueDiscard = new List<Clue>();

        foreach (Location l in App.Model.locationModel.locations)
        {
            cluePool.Add(new Clue(l));
        }
    }

    public Clue DrawClue()
    {
        if (cluePool.Count == 0)
            ReshuffleClues();
        if (cluePool.Count == 0)
            return new Clue(null);

        Clue c = cluePool[Random.Range(0, cluePool.Count)];
        cluePool.Remove(c);
        return c;
    }

    public List<Clue> SpawnClues(int numClues)
    {
        List<Clue> spawnedClues = new List<Clue>();
        for (int i = 0; i < numClues; i++)
        {
            if (cluePool.Count == 0)
                ReshuffleClues();
            if (cluePool.Count == 0)
                return spawnedClues;

            Clue c = cluePool[Random.Range(0, cluePool.Count)];
            App.Controller.locationController.MoveClueToLocation(c, c.location);
            activeClues.Add(c);
            cluePool.Remove(c);
            spawnedClues.Add(c);
        }
        return spawnedClues;
    }

    private void ReshuffleClues()
    {
        cluePool = new List<Clue>(clueDiscard);
        clueDiscard.Clear();
    }

    public void ClueClaimed(Clue c)
    {
        activeClues.Remove(c);
    }

    public void ClueSpent(Clue c)
    {
        clueDiscard.Add(c);
    }
}
