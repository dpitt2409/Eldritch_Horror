using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class NormanWithersHealthDeathEncounter : Encounter
{
    public NormanWithersHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Will;
        testModifier = 0;
        encounterText = "Norman can barely move in his hospital bed and points to his notes and suitcase. Gain all of his possessions. He begs you to use his telescope to record the current locations of the stars. The work is slow and demanding, but you focus as best you can to give him accurate results. \n\r Test Will \n\r If you pass, Norman's insight into the Ancient One proves accurate; retreat Doom by 1. \n\r If you fail, Norman can make no sense of the night sky.";
        passText = "Pass \n\r Retreat Doom by 1";
        failText = "Fail";
    }

    public override void FinishEncounter(bool passed)
    {
        if (passed)
        {
            // Retreat doom by 1
            GameManager.SingleInstance.App.Model.doomModel.RetreatDoom(1);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DoomRetreated); // Start Queue
            GameManager.SingleInstance.App.Model.eventModel.DoomAdvanced(); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void DoomRetreated()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }
}
*/