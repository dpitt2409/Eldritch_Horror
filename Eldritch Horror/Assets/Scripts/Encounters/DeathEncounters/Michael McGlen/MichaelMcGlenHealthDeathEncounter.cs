using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class MichaelMcGlenHealthDeathEncounter : Encounter
{
    public MichaelMcGlenHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Strength;
        testModifier = 0;
        encounterText = "McGlen disappeared from the hospital, leaving his things behind. Gain all of his possessions. Over the years, McGlen hurt a lot of people, and now some local thugs plan to take revenge. You track down one of the kidnappers and try to force him to tell you where they are keeping him. \n\r Test Strength \n\r If you pass, you find McGlen wounded but in good spirits; retreat Doom by 1. \n\r If you fail, the police eventually find his body after what looks to have been a very violent death.";
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