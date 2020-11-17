using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class CharlieKaneSanityDeathEncounter : Encounter
{
    public CharlieKaneSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "The first thing Charlie does when you enter the restaurant is hand you all of his belongings. Gain all of his possessions. He offers you deals, promising to sell you Atlantis and introduce you to Caesar. You negotiate carefully with him. \n\r Test Influence \n\r If you pass, he tells you all he knows in exchange for your napkin ring and a salt shaker; retreat Doom by 1. \n\r If you fail, Charlie gets angry and insists that you'll be sorry when he's the President of the world.";
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