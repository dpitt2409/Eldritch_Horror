using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluSeaResearchEncounter4 : Encounter
{
    public CthuluSeaResearchEncounter4()
    {
        title = "Sea Research Encounter";
        testStat = TestStat.Observation;
        testModifier = 0;
        encounterText = "Your ship encounters another vessel engaged in deep-sea excavation. You sneak aboard their craft to see what they've found. \n\r Test Observation \n\r If you pass, you see strangely-crafted golden jewelry; gain this Clue. \n\r If you fail, the divers catch you and throw you into their brig; gain a Detained Condition.";
        passText = "Pass \n\r Gain this Clue";
        failText = "Fail \n\r Gain a Detained Condition";
    }

    public override void FinishEncounter(bool passed)
    {
        Investigator active = GameManager.SingleInstance.App.Model.encounterMenuModel.currentInvestigator;
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
            // Gain a Detained Condition

            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void ResearchClueClaimed()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }
}
