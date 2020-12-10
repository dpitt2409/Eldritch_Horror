using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YigCityResearchEncounter11 : Encounter
{
    public YigCityResearchEncounter11()
    {
        title = "City Research Encounter";
        testStat = TestStat.Lore;
        testModifier = -2;
        encounterText = "Every snake in this city has been killed, and you fear Yig's wrath. You recite the chant that you hope will placate the snake god. \n\r Test Lore -2 \n\r If you pass, Yig looks upon you favorably; gain this Clue and 1 additional Clue. \n\r If you fail, Yig will have his revenge; advance Doom by 1 unless you gain a Cursed Condition.";
        passText = "Pass \n\r Gain this Clue and 1 additional Clue";
        failText = "Fail \n\r Advance Doom by 1 unless you gain a Cursed Condition.";
    }
    public override void FinishEncounter(bool passed)
    {

        Investigator active = GameManager.SingleInstance.App.Model.encounterMenuModel.currentInvestigator;
        if (passed)
        {
            // Gain this Clue and 1 additional Clue
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
            // Advance Doom by 1 unless you gain a Cursed Condition

            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void ResearchClueClaimed()
    {
        Clue c = GameManager.SingleInstance.App.Model.clueModel.DrawClue();
        GameManager.SingleInstance.App.Model.encounterMenuModel.currentInvestigator.GainClue(c);

        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(FinishEncounter); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.ClueGainedEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void FinishEncounter()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }

}
