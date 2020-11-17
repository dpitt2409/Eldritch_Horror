using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YigSeaResearchEncounter17 : Encounter
{
    public YigSeaResearchEncounter17()
    {
        title = "Sea Research Encounter";
        testStat = TestStat.Observation;
        testModifier = -1;
        encounterText = "Yig grants you a vision of a location as it was eons ago when the serpent people ruled. You study this brief image closely. \n\r Test Observation -1 \n\r If you pass, you see vital details; gain this Clue. If you fail, Yig punishes you; gain a Hallucinations Condition and move this Clue to the nearest Expedition space.";
        passText = "Pass \n\r Gain this Clue";
        failText = "Fail \n\r Gain a Hallucinations Condition and move this Clue to the nearest Expedition space";
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
            // Gain a Hallucinations Condition and move this Clue to the nearest Expedition space

            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void ResearchClueClaimed()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }

}
