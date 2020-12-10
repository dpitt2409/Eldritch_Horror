using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluWildernessResearchEncounter4 : Encounter
{

    public CthuluWildernessResearchEncounter4()
    {
        title = "Wilderness Research Encounter";
        testStat = TestStat.Observation;
        testModifier = 0;
        encounterText = "You try to track down an old hermit who supposedly knows the secrets of Cthulu's cults. \n\r Test Observation \n\r If you pass, the man is insane, but his stories prove accurate. Gain this Clue. \n\r If you fail, the man has been taken by pirates; move this Clue to the nearest Sea space.";
        passText = "Pass \n\r Gain this Clue";
        failText = "Fail \n\r Move this Clue to the nearest sea space.";
    }

    public override void FinishEncounter(bool passed)
    {
        Investigator active = GameManager.SingleInstance.App.Model.encounterMenuModel.currentInvestigator;
        Location l = active.currentLocation;
        Clue c = l.cluesOnLocation[0];
        if (passed)
        {
            // Gain this Clue
            GameManager.SingleInstance.App.Controller.locationController.MoveClueFromLocation(c, l);
            active.GainClue(c);
            GameManager.SingleInstance.App.Model.clueModel.ClueClaimed(c);

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(ResearchClueClaimed); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.ResearchClueClaimedEvent(); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            // Move this Clue to the nearest sea space
            List<Location> nearestSeaSpaces = GameManager.SingleInstance.App.Controller.locationController.FindNearestSpaceOfType(l, LocationType.Sea);
            // Send this to menu to pick from closest locations
            GameManager.SingleInstance.App.Controller.locationController.MoveClueFromLocation(c, l);
            GameManager.SingleInstance.App.Controller.locationController.MoveClueToLocation(c, nearestSeaSpaces[0]);

            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void ResearchClueClaimed()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }

}
