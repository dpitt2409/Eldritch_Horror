using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterMenuController : MVC
{
    public void StartEncounter(Encounter encounter)
    {
        App.Model.encounterMenuModel.StartEncounter(encounter);
        encounter.StartEncounter(); // Should handle any pregain and then call StartTest when its done
    }

    public void StartTest()
    {
        // Check if the Investigator died during the pre test
        Investigator active = App.Model.encounterMenuModel.currentInvestigator;
        if (active.deathEncounter != null)
        {
            ContinueEncounter();
            return;
        }

        App.Model.encounterMenuModel.TestStarted();
        Encounter encounter = App.Model.encounterMenuModel.currentEncounter;
        App.Controller.testController.StartTest(encounter.testStat, encounter.testModifier, TestType.Test, active, EncounterTestComplete);
    }

    public void EncounterTestComplete(List<int> results)
    {
        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= App.Model.encounterMenuModel.currentInvestigator.SUCCESS)
            {
                numSuccesses++;
            }
        }
        Encounter e = App.Model.encounterMenuModel.currentEncounter;
        App.Model.encounterMenuModel.EncounterTested(numSuccesses > 0);
        e.FinishEncounter(numSuccesses > 0);
    }

    public void CompleteEncounter()
    {
        App.View.encounterMenuView.EncounterComplete();
    }

    public void ContinueEncounter()
    {
        App.View.encounterMenuView.EncounterContinue();
        App.Model.encounterPhaseModel.NextEncounterTurn();
    }
}
