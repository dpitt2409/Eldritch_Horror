using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YigWildernessResearchEncounter11 : Encounter
{
    public YigWildernessResearchEncounter11()
    {
        title = "Wilderness Research Encounter";
        testStat = TestStat.Observation;
        testModifier = 0;
        encounterText = "Lost in the caverns, you feel you've been here before in your dreams. You try to find your way out. \n\r Test Observation \n\r If you pass, you know this passage leads to the Dreamlands; gain this Clue. \n\r If you fail, you fall into the void; gain a Lost in Time and Space Condition.";
        passText = "Pass \n\r Gain this Clue";
        failText = "Fail \n\r Gain a Lost in Time and Space Condition";
    }

    public override void FinishEncounter(bool passed)
    {

        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;
        if (passed)
        {
            // Gain this Clue
            Location l = active.currentLocation;
            Clue c = l.cluesOnLocation[0];

            GameManager.SingleInstance.App.Controller.locationController.MoveClueFromLocation(c, l);
            active.GainClue(c);
            GameManager.SingleInstance.App.Model.clueModel.ClueClaimed(c);

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(ResearchClueClaimed); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.ResearchClueClaimedEvent(); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            // Gain a Lost in Time and Space Condition

            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void ResearchClueClaimed()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }
}
