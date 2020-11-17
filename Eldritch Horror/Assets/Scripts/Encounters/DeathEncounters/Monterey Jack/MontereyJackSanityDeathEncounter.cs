using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class MontereyJackSanityDeathEncounter : Encounter
{
    public MontereyJackSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "Jack has carved an arcane sigil into his flesh dozens of times. The asylum no longer allows him access to his belongings for fear he will continue. Gain all of his possessions. He says he must keep digging and tears at his skin with his fingernails. You beg him to stop. \n\r Test Influence \n\r If you pass, he calms down enough to explain the significance of the symbol and how to use it against the coming darkness; retreat Doom by 1. \n\r If you fail, the orderlies are forced to sedate him.";
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