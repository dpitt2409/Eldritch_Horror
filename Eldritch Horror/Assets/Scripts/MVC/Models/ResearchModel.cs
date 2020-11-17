using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchModel : MVC
{
    public Dictionary<LocationType, List<Encounter>> researchDecks;
    public Dictionary<LocationType, List<Encounter>> researchDiscards;

    // Start is called before the first frame update
    void Start()
    {
        researchDiscards = new Dictionary<LocationType, List<Encounter>>();
        researchDiscards.Add(LocationType.City, new List<Encounter>());
        researchDiscards.Add(LocationType.Sea, new List<Encounter>());
        researchDiscards.Add(LocationType.Wilderness, new List<Encounter>());
    }

    public void SetResearchDecks(Dictionary<LocationType, List<Encounter>> decks)
    {
        researchDecks = decks;
    }

    public Encounter DrawResearchEncounter(LocationType type)
    {
        List<Encounter> encounters = researchDecks[type];
        if (encounters.Count == 0)
        {
            researchDecks[type] = new List<Encounter>(researchDiscards[type]);
            researchDiscards[type].Clear();
            encounters = researchDecks[type];
        }

        int index = Random.Range(0, encounters.Count);
        Encounter e = encounters[index];
        researchDiscards[type].Add(e);
        encounters.RemoveAt(index);
        return e;
    }
}
