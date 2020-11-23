using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location
{
    public string locationName;

    public Connection[] connections;

    public LocationType type;

    public bool majorLocation;

    public GateColor gate;

    public bool activeGate = false;

    public List<Investigator> investigatorsOnLocation;

    public List<Monster> monstersOnLocation;

    public List<Clue> cluesOnLocation;

    public List<Expedition> expeditionsOnLocation;

    public List<Investigator> deadInvestigatorsOnLocation;

    public List<OngoingEffect> ongoingEffectsOnLocation;

    public List<EldritchToken> eldritchTokensOnLocation;

    public Location(string location)
    {
        locationName = location;
        investigatorsOnLocation = new List<Investigator>();
        monstersOnLocation = new List<Monster>();
        cluesOnLocation = new List<Clue>();
        expeditionsOnLocation = new List<Expedition>();
        deadInvestigatorsOnLocation = new List<Investigator>();
        ongoingEffectsOnLocation = new List<OngoingEffect>();
        eldritchTokensOnLocation = new List<EldritchToken>();
    }

    public void InvestigatorEnteredLocation(Investigator i)
    {
        investigatorsOnLocation.Add(i);
    }

    public void InvestigatorLeftLocation(Investigator i)
    {
        investigatorsOnLocation.Remove(i);
    }

    public void MonsterEnteredLocation(Monster m)
    {
        monstersOnLocation.Add(m);
    }

    public void MonsterLeftLocation(Monster m)
    {
        monstersOnLocation.Remove(m);
    }

    public void ClueEnteredLocation(Clue c)
    {
        cluesOnLocation.Add(c);
    }

    public void ClueLeftLocation(Clue c)
    {
        cluesOnLocation.Remove(c);
    }

    public void ExpeditionEnteredLocation(Expedition e)
    {
        expeditionsOnLocation.Add(e);
    }

    public void ExpeditionLeftLocation(Expedition e)
    {
        if (expeditionsOnLocation.Contains(e))
            expeditionsOnLocation.Remove(e);
    }

    public void OngoingEffectEnteredLocation(OngoingEffect effect)
    { 
        // Shouldn't need this, same rumor should never be added again
        foreach (OngoingEffect e in ongoingEffectsOnLocation)
        {
            if (e.effectTitle == effect.effectTitle)
                return;
        }


        ongoingEffectsOnLocation.Add(effect);
    }

    public void OngoingEffectLeftLocation(OngoingEffect effect)
    {
        if (ongoingEffectsOnLocation.Contains(effect))
            ongoingEffectsOnLocation.Remove(effect);
    }

    public void InvestigatorDiedOnLocation(Investigator i)
    {
        if (investigatorsOnLocation.Contains(i))
            investigatorsOnLocation.Remove(i);

        deadInvestigatorsOnLocation.Add(i);
    }

    public void DeadInvestigatorLeftLocation(Investigator i)
    {
        if (deadInvestigatorsOnLocation.Contains(i))
            deadInvestigatorsOnLocation.Remove(i);
    }

    public void EldritchTokenEnteredLocation(EldritchToken et)
    {
        eldritchTokensOnLocation.Add(et);
    }

    public void EldritchTokenLeftLocation(EldritchToken et)
    {
        if (eldritchTokensOnLocation.Contains(et))
            eldritchTokensOnLocation.Remove(et);
    }
}
