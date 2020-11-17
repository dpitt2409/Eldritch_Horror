using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter
{
    public string title;

    public string encounterText;

    public TestStat testStat;

    public int testModifier;

    public string passText;

    public string failText;

    public virtual void StartEncounter() { GameManager.SingleInstance.App.Controller.encounterMenuController.StartTest(); }

    public virtual void FinishEncounter(bool passed) { GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); }
}
