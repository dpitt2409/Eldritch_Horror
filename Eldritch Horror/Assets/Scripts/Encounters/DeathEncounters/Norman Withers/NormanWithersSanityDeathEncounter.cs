using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class NormanWithersSanityDeathEncounter : Encounter
{
    public NormanWithersSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "The doctors would like you to look at Norman's notes and belongings. Gain all of his possessions. They are trying to gain some insight into his catatonic state. You try to convince them to move Norman to a room with a view of the night sky. \n\r Test Influence \n\r If you pass, Norman becomes slightly responsive, and you are able to question him; retreat Doom by 1. \n\r If you fail, Norman's condition grows steadily worse.";
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