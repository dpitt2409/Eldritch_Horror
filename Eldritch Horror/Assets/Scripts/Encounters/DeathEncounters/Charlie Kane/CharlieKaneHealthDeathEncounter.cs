using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class CharlieKaneHealthDeathEncounter : Encounter
{
    public CharlieKaneHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "The nurse at the front desk hands you a parcel. Gain all of his possessions. 'Mr Kane said to give you this package, but the doctor insists that no visitors be admitted.' You try to convince her to make an exception for you. \n\r Test Influence \n\r If you pass, you find that Charlie's health is beyond recovery, but he's still in good spirits and you have a long talk; retreat Doom by 1. \n\r If you fail, Charlie spends the rest of his days cut off from all human contact.";
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
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start the Queue
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