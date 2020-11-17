using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterMenuModel : MVC
{
    public Encounter currentEncounter;
    public bool testing = false;

    public void StartEncounter(Encounter encounter)
    {
        currentEncounter = encounter;
        App.View.encounterMenuView.StartEncounter(encounter);
    }

    public void TestStarted()
    {
        testing = true;
    }

    public void EncounterTested(bool passed)
    {
        testing = false;
        App.View.encounterMenuView.EncounterTested(currentEncounter, passed);
    }
}
