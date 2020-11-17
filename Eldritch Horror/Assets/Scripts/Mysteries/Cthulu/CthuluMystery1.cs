using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluMystery1 : Mystery
{
    public CthuluMystery1()
    {
        ancientOne = "Cthulu";
        mysteryName = "The Stars Are Right!";
        mysteryDescription = "All around the world, Cthulu's worshipers and those sensitive to his dreams have been plagued by madness. They rush to the sea to witness the Ancient One's return";
        mysteryText = "When this card enters play, move each Clue on the game board to the nearest Sea Space. \n\r After an investigator resolves a Research Encounter, he may spend 1 Clue he gained from that encounter and place that Clue on this card. \n\r At the end of the Mythos Phase, if there are 3 Clues on this card, solve this Mystery.";
        requirement = 3;
    }

    public override void StartMystery()
    {
        GameManager.SingleInstance.App.Model.eventModel.researchClueClaimedEvent += ResearchClueClaimedEvent;
    }

    public override void FinishMystery()
    {
        GameManager.SingleInstance.App.Model.eventModel.researchClueClaimedEvent -= ResearchClueClaimedEvent;
    }

    public void ResearchClueClaimedEvent()
    {
        EventAction e = new EventAction("Mystery Event", ResearchClueGained);
        GameManager.SingleInstance.App.Controller.queueController.AddCallBack(e);
    }

    public void ResearchClueGained()
    {
        if (GameManager.SingleInstance.App.Model.mysteryModel.mysteryProgress < requirement)
        {
            // Minimize Encounter Window
            //GameManager.SingleInstance.App.View.encounterMenuView.EncounterWindowMinimized();
            GameManager.SingleInstance.App.Controller.openMenuController.HideOpenMenu();

            string title = mysteryName + " Mystery";
            string text = "You may spend 1 Clue gained from the Research Encounter to advance the Mystery by 1.";
            string yesText = "Spend 1 Clue to Advance the Mystery";
            string noText = "Do not spend a Clue";
            GameManager.SingleInstance.App.Controller.singleOptionController.StartSingleOption(title, text, yesText, noText, OptionChosen);
        }
        else
        {
            // Advance Queue
            GameManager.SingleInstance.App.Controller.queueController.NextCallBack();
        }
    }

    public void OptionChosen(bool response)
    {
        if (response)
        {
            // Spend 1 clue and advance mystery by 1
            Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;

            active.SpendClue();

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(ClueSpent); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.ClueSpentEvent(); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            // Maximize Encounter Window
            //GameManager.SingleInstance.App.View.encounterMenuView.EncounterWindowMaximized();
            GameManager.SingleInstance.App.Controller.openMenuController.EnableOpenMenu();

            // Advance Queue
            GameManager.SingleInstance.App.Controller.queueController.NextCallBack();
        }
    }

    public void ClueSpent()
    {
        // Advance the Mystery by 1
        GameManager.SingleInstance.App.Controller.mysteryController.AdvanceMystery();
        // Maximize Encounter Window
        //GameManager.SingleInstance.App.View.encounterMenuView.EncounterWindowMaximized();
        GameManager.SingleInstance.App.Controller.openMenuController.EnableOpenMenu();

        // Advance Queue
        GameManager.SingleInstance.App.Controller.queueController.NextCallBack();
    }
}
