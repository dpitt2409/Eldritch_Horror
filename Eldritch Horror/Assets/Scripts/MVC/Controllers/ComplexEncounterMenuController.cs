using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexEncounterMenuController : MVC
{
    public void StartComplexEncounter(ComplexEncounter encounter)
    {
        App.Model.complexEncounterMenuModel.StartComplexEncounter(encounter);
        encounter.StartComplexEncounter(); // Should handle any pregain and then call StartComplexTest1 or StartPhase2 when its done
    }
    
    public void StartTest1()
    {
        App.Model.complexEncounterMenuModel.TestStarted();
        ComplexEncounter encounter = App.Model.complexEncounterMenuModel.currentComplexEncounter;
        App.Controller.testController.StartTest(encounter.testStats[0], encounter.testModifiers[0], TestType.Test, Test1Complete); 
    }

    public void Test1Complete(List<int> results)
    {
        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= App.Model.investigatorModel.activeInvestigator.SUCCESS)
            {
                numSuccesses++;
            }
        }

        App.Model.complexEncounterMenuModel.Test1Finished();

        if (numSuccesses > 0)
        {
            StartPhase2(1);
        }
        else
        {
            StartPhase2(2);
        }
    }

    public void StartPhase2(int newTestIndex)
    {
        App.Model.complexEncounterMenuModel.UpdateTestIndex(newTestIndex);
        App.View.complexEncounterMenuView.StartPhase2();
        ComplexEncounter encounter = App.Model.complexEncounterMenuModel.currentComplexEncounter;
        encounter.StartPhase2();
    }

    public void StartTest2()
    {
        int index = App.Model.complexEncounterMenuModel.testIndex;
        App.Model.complexEncounterMenuModel.TestStarted();
        ComplexEncounter encounter = App.Model.complexEncounterMenuModel.currentComplexEncounter;
        App.Controller.testController.StartTest(encounter.testStats[index], encounter.testModifiers[index], TestType.Test, Test2Complete);
    }

    public void Test2Complete(List<int> results)
    {
        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= App.Model.investigatorModel.activeInvestigator.SUCCESS)
            {
                numSuccesses++;
            }
        }

        App.Model.complexEncounterMenuModel.Test2Finished(numSuccesses > 0);

        ComplexEncounter encounter = App.Model.complexEncounterMenuModel.currentComplexEncounter;
        encounter.FinishPhase2(numSuccesses > 0); // encounter will call CompleteEncounter when its done
    }

    public void CompleteEncounter()
    {
        App.View.complexEncounterMenuView.ComplexEncounterComplete();
    }

    public void ContinueEncounter()
    {
        App.View.complexEncounterMenuView.ComplexEncounterContinue();
        App.Model.encounterPhaseModel.NextEncounterTurn();
    }
}
