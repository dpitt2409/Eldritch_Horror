using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class LilyChenHealthDeathEncounter : Encounter
{
    public LilyChenHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Strength;
        testModifier = 0;
        encounterText = "When you reach the hospital, you find Lily's things, but she is not in her bed. Gain all of her possessions. You quickly find that she's been kidnapped by cultists! You fight to free her \n\r Test Strength \n\r If you pass, interrogate the cultists, and Lily is safely returned to the hospital to begin the slow healing process; retreat Doom by 1. If you fail, Lily, disappears into the darkness.";
        passText = "Pass \n\r Retreat Doom by 1";
        failText = "Fail";
    }

    public override void FinishEncounter(bool passed)
    {
        if (passed)
        {
            // Retreat doom by 1
            GameManager.SingleInstance.App.Model.doomModel.RetreatDoom(1);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DoomRetreated); // Create Queue
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