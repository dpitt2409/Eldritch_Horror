using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComplexEncounter
{
    public string title;

    public string[] encounterTexts;

    public TestStat[] testStats;

    public int[] testModifiers;

    public string[] passTexts;

    public string[] failTexts;

    public virtual void StartComplexEncounter() { GameManager.SingleInstance.App.Controller.complexEncounterMenuController.StartTest1(); }

    public virtual void StartPhase2() { GameManager.SingleInstance.App.Controller.complexEncounterMenuController.StartTest2(); }

    public virtual void FinishPhase2(bool passed) { GameManager.SingleInstance.App.Controller.complexEncounterMenuController.CompleteEncounter(); }
}
