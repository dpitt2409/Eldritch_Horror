using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterModel : MVC
{
    public Dictionary<Location, List<Encounter>> locationEncounters;
    public Dictionary<LocationType, List<Encounter>> generalEncounters;

    public Dictionary<Location, List<Encounter>> locationEncounterDiscard;
    public Dictionary<LocationType, List<Encounter>> generalEncounterDisard;

    public List<ComplexEncounter> otherWorldlyEncounters;
    public List<ComplexEncounter> otherWorldlyEncountersDiscard;

    public List<ComplexEncounter> expeditionEncounters;
    public List<ComplexEncounter> expeditionEncountersDiscard;

    void Start()
    {
        locationEncounters = new Dictionary<Location, List<Encounter>>();
        locationEncounterDiscard = new Dictionary<Location, List<Encounter>>();
        foreach (Location l in App.Model.locationModel.locations)
        {
            if (l.majorLocation)
            {
                locationEncounters.Add(l, new List<Encounter>());
                locationEncounterDiscard.Add(l, new List<Encounter>());
            }
        }
        generalEncounters = new Dictionary<LocationType, List<Encounter>>();
        generalEncounters.Add(LocationType.City, new List<Encounter>());
        generalEncounters.Add(LocationType.Sea, new List<Encounter>());
        generalEncounters.Add(LocationType.Wilderness, new List<Encounter>());

        generalEncounterDisard = new Dictionary<LocationType, List<Encounter>>();
        generalEncounterDisard.Add(LocationType.City, new List<Encounter>());
        generalEncounterDisard.Add(LocationType.Sea, new List<Encounter>());
        generalEncounterDisard.Add(LocationType.Wilderness, new List<Encounter>());

        InitializeEncounters(); // Location / General Encounters

        // OtherWorldly Encounters
        otherWorldlyEncounters = new List<ComplexEncounter>();
        otherWorldlyEncountersDiscard = new List<ComplexEncounter>();

        otherWorldlyEncounters.Add(new OtherWorldlyEncounter8());

        // Expedition Encounters
        expeditionEncounters = new List<ComplexEncounter>();
        expeditionEncountersDiscard = new List<ComplexEncounter>();

        expeditionEncounters.Add(new AntarcticaExpeditionEncounter2());
        expeditionEncounters.Add(new ThePyramidsExpeditionEncounter3());
        expeditionEncounters.Add(new TheHeartOfAfricaExpeditionEncounter1());
    }

    private void InitializeEncounters()
    {
        // Location Encounters
        // Tokyo
        List<Encounter> tokyoEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("Tokyo")];
        tokyoEncounters.Add(new TokyoEncounter10());

        // Shanghai
        List<Encounter> shanghaiEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("Shanghai")];
        shanghaiEncounters.Add(new ShanghaiEncounter10());

        // Syndey
        List<Encounter> sydneyEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("Sydney")];
        sydneyEncounters.Add(new SydneyEncounter9());

        // London
        List<Encounter> londonEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("London")];
        londonEncounters.Add(new LondonEncounter31());
        
        // Rome
        List<Encounter> romeEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("Rome")];
        romeEncounters.Add(new RomeEncounter2());

        // Istanbul
        List<Encounter> istanbulEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("Istanbul")];
        istanbulEncounters.Add(new IstanbulEncounter2());

        // Arkham
        List<Encounter> arkhamEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("Arkham")];
        arkhamEncounters.Add(new ArkhamEncounter11());

        // San Francisco
        List<Encounter> sfEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("San Francisco")];
        sfEncounters.Add(new SanFranciscoEncounter16());

        // Buenos Aires
        List<Encounter> baEncounters = locationEncounters[App.Model.locationModel.FindLocationByName("Buenos Aires")];
        baEncounters.Add(new BuenosAiresEncounter11());

        // General Encounters
        // City
        List<Encounter> cityGeneralEncounters = generalEncounters[LocationType.City];
        cityGeneralEncounters.Add(new CityGeneralEncounter25());
        // Sea
        List<Encounter> seaGeneralEncounters = generalEncounters[LocationType.Sea];
        seaGeneralEncounters.Add(new SeaGeneralEncounter1());
        // Wilderness
        List<Encounter> wildernessGeneralEncounters = generalEncounters[LocationType.Wilderness];
        wildernessGeneralEncounters.Add(new WildernessGeneralEncounter14());
    }

    public Encounter DrawLocationEncounter(Location l)
    {
        List<Encounter> encounters = locationEncounters[l];
        if (encounters.Count == 0)
        {
            locationEncounters[l] = new List<Encounter>(locationEncounterDiscard[l]);
            locationEncounterDiscard[l].Clear();
            encounters = locationEncounters[l];
        }

        int index = Random.Range(0, encounters.Count);
        Encounter e = encounters[index];
        locationEncounterDiscard[l].Add(e);
        encounters.RemoveAt(index);
        return e;
    }

    public Encounter DrawGeneralEncounter(LocationType type)
    {
        List<Encounter> encounters = generalEncounters[type];
        if (encounters.Count == 0)
        {
            generalEncounters[type] = new List<Encounter>(generalEncounterDisard[type]);
            generalEncounterDisard[type].Clear();
            encounters = generalEncounters[type];
        }

        int index = Random.Range(0, encounters.Count);
        Encounter e = encounters[index];
        generalEncounterDisard[type].Add(e);
        encounters.RemoveAt(index);
        return e;
    }

    public ComplexEncounter DrawOtherWorldlyEncounter()
    {
        if (otherWorldlyEncounters.Count == 0)
        {
            otherWorldlyEncounters = new List<ComplexEncounter>(otherWorldlyEncountersDiscard);
            otherWorldlyEncountersDiscard.Clear();
        }

        int index = Random.Range(0, otherWorldlyEncounters.Count);
        ComplexEncounter e = otherWorldlyEncounters[index];
        otherWorldlyEncountersDiscard.Add(e);
        otherWorldlyEncounters.RemoveAt(index);
        return e;
    }

    public ComplexEncounter DrawExpeditionEncounter()
    {
        if (expeditionEncounters.Count == 0)
        {
            expeditionEncounters = new List<ComplexEncounter>(expeditionEncountersDiscard);
            expeditionEncountersDiscard.Clear();
        }

        int index = Random.Range(0, expeditionEncounters.Count);
        ComplexEncounter e = expeditionEncounters[index];
        expeditionEncountersDiscard.Add(e);
        expeditionEncounters.RemoveAt(index);
        return e;
    }
}
