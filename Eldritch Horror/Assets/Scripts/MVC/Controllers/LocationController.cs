using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MVC
{
    public void EnableMap()
    {
        App.View.locationView.EnableMap();
    }

    public void DisableMap()
    {
        App.View.locationView.DisableMap();
    }

    public void AncientOneFlipped()
    {
        App.View.locationView.AncientOneFlipped();
    }

    public void EnterStartingLocation(Investigator i, Location l)
    {
        l.InvestigatorEnteredLocation(i);
        App.View.locationView.LocationUpdated(l);
        i.EnterLocation(l);
    }

    public void MoveInvestigator(Investigator i, Location l)
    {
        i.currentLocation.InvestigatorLeftLocation(i);
        App.View.locationView.LocationUpdated(i.currentLocation);
        l.InvestigatorEnteredLocation(i);
        App.View.locationView.LocationUpdated(l);

        i.EnterLocation(l);
    }

    public void MoveClueToLocation(Clue c, Location l)
    {
        l.ClueEnteredLocation(c);
        App.View.locationView.LocationUpdated(l);
    }

    public void MoveClueFromLocation(Clue c, Location l)
    {
        l.ClueLeftLocation(c);
        App.View.locationView.LocationUpdated(l);
    }

    public void SpawnMonsterOnLocation(Monster m, Location l)
    {
        l.MonsterEnteredLocation(m);
        App.View.locationView.LocationUpdated(l);
    }

    public void RemoveMonsterFromLocation(Monster m, Location l)
    {
        l.MonsterLeftLocation(m);
        App.View.locationView.LocationUpdated(l);
    }

    public void SpawnGate(Location l)
    {
        l.activeGate = true;
        App.View.locationView.LocationUpdated(l);
    }

    public void CloseGate(Location l)
    {
        App.Model.gateModel.CloseGate(l);
        l.activeGate = false;
        App.View.locationView.LocationUpdated(l);
    }

    public void SpawnNewExpedition()
    {
        ComplexEncounter newExpedition = App.Model.encounterModel.DrawExpeditionEncounter();
        Location l = App.Model.locationModel.FindLocationByName(newExpedition.title);
        l.ExpeditionEnteredLocation(new Expedition(newExpedition));
        App.View.locationView.LocationUpdated(l);
    }

    public void ResolveExpedition(Location l, Expedition e)
    {
        l.ExpeditionLeftLocation(e);
        App.View.locationView.LocationUpdated(l);
        SpawnNewExpedition();
    }

    public void SpawnOngoingEffect(OngoingEffect effect, Location l)
    {
        l.OngoingEffectEnteredLocation(effect);
        App.View.locationView.LocationUpdated(l);
    }

    public void ResolveOngoingEffect(OngoingEffect effect, Location l)
    {
        l.OngoingEffectLeftLocation(effect);
        App.View.locationView.LocationUpdated(l);
    }

    public void InvestigatorDiedOnLocation(Investigator i, Location l)
    {
        l.InvestigatorDiedOnLocation(i);
        App.View.locationView.LocationUpdated(l);
    }

    public void DeadInvestigatorTokenClaimed(Investigator i, Location l)
    {
        l.DeadInvestigatorLeftLocation(i);
        App.View.locationView.LocationUpdated(l);
    }

    public void SpawnEldritchTokenOnLocation(EldritchToken et, Location l)
    {
        l.EldritchTokenEnteredLocation(et);
        App.View.locationView.LocationUpdated(l);
    }

    public void EldritchTokenRemoved(EldritchToken et, Location l)
    {
        l.EldritchTokenLeftLocation(et);
        App.View.locationView.LocationUpdated(l);
    }

    public List<Location> FindNearestSpaceOfType(Location start, LocationType type)
    {
        List<Location> closest = new List<Location>();
        List<Connection> currentConnections = new List<Connection>(start.connections);

        if (start.type == type)
        {
            closest.Add(start);
            return closest;
        }

        while (closest.Count == 0)
        {
            List<Connection> nextConnections = new List<Connection>();
            foreach (Connection connection in currentConnections)
            {
                if (connection.destination.type == type)
                {
                    closest.Add(connection.destination);
                }
                else
                {
                    foreach (Connection c in connection.destination.connections)
                    {
                        if ((c.destination.locationName != start.locationName) 
                            && !HasDestination(nextConnections, c.destination)
                            && !HasDestination(currentConnections, c.destination))
                            nextConnections.Add(c);
                    }
                }
            }
            currentConnections = new List<Connection>(nextConnections);
        }

        return closest;
    }

    private bool HasDestination(List<Connection> connections, Location destination)
    {
        bool has = false;
        foreach(Connection c in connections)
        {
            if (c.destination.locationName == destination.locationName)
                has = true;
        }
        return has;
    }
}
