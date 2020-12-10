using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluCityResearchEncounter5 : Encounter
{   
    public CthuluCityResearchEncounter5()
    {
        title = "City Research Encounter";
        testStat = TestStat.Observation;
        testModifier = 0;
        encounterText = "You dream of a city built from massive green stones set at non-Euclidean angles. You try to retain as many details as you can to investigate when you wake up. \n\r Test Observation \n\r If you pass, you identify the locatin by the stars overhead; gain this Clue and 1 additional Clue. \n\r If you fail, the details of the dream are too horrible to remember; lose 1 Sanity.";
        passText = "Pass \n\r Gain this Clue and 1 additional Clue";
        failText = "Fail \n\r Lose 1 Sanity";
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
            // Lose 1 Sanity
            active.SetIncomingDamage(1);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(LoseSanity); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.LoseSanityEvent(active, 1); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
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

    public void LoseSanity()
    {
        Investigator active = GameManager.SingleInstance.App.Model.encounterMenuModel.currentInvestigator;
        active.LoseSanity(active.GetIncomingDamage());
        active.SetIncomingDamage(0);

        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(FinishEncounter); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void FinishEncounter()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }

}
