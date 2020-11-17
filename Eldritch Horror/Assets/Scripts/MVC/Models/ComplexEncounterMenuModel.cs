using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexEncounterMenuModel : MVC
{
    public ComplexEncounter currentComplexEncounter;

    public bool testing = false;
    public int testIndex = 0;
    public bool passed = false;

    public void StartComplexEncounter(ComplexEncounter encounter)
    {
        currentComplexEncounter = encounter;
        testIndex = 0;
        App.View.complexEncounterMenuView.StartComplexEncounter(encounter);
    }

    public void TestStarted()
    {
        testing = true;
    }

    public void Test1Finished()
    {
        testing = false;
    }

    public void Test2Finished(bool p)
    {
        testing = false;
        passed = p;
    }

    public void UpdateTestIndex(int newIndex)
    {
        testIndex = newIndex;
    }
}
