using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IstanbulEncounter2 : Encounter
{
    public IstanbulEncounter2()
    {
        title = "Istanbul Location Encounter";
        testStat = TestStat.None;
        testModifier = 0;
        encounterText = "People from every walk of life can be found enjoying the cleansing steam of the Turkish baths. Inside, you'll eventually find an expert in any given field. You may become Delayed to improve 1 skill of your choice.";
        passText = "Pass";
        failText = "Fail";
    }

    public override void StartEncounter()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter();
    }

}
