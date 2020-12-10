using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPactCondition : Condition
{
    public DarkPactCondition(int index)
    {
        conditionName = "Dark Pact";
        type = ConditionType.Deal;
        conditionPortrait = GameManager.SingleInstance.App.Model.conditionSpritesModel.darkPactSprite;
        conditionText = "";
        reckoningText = "Roll 1 die. on a 1, it is time to fulfill your part of the bargain; flip this card.";
        copyIndex = index;
    }

    public override void Gained()
    {
        GameManager.SingleInstance.App.Model.eventModel.reckoningEvent += ReckoningEvent;
    }

    public override void Lost()
    {
        GameManager.SingleInstance.App.Model.eventModel.reckoningEvent -= ReckoningEvent;
    }

    public void ReckoningEvent()
    {
        string title = owner.investigatorName + " Dark Pact Reckoning";
        ReckoningEvent re = new ReckoningEvent(title, reckoningText, StartReckoning, ReckoningSource.Investigator, owner, conditionPortrait);
        GameManager.SingleInstance.App.Model.reckoningMythosModel.AddReckoningEvent(re);
    }

    public void StartReckoning()
    {
        GameManager.SingleInstance.App.Controller.testController.StartSingleDieTest(owner, SingleDieRolled);
    }

    public void SingleDieRolled(List<int> result)
    {
        string finishedText = "";
        if (result[0] == 1)
        {
            finishedText = "Your time has come, flip this card.";
            GameManager.SingleInstance.App.Controller.reckoningMythosController.SetReckoningText(finishedText);
            GameManager.SingleInstance.App.Controller.reckoningMythosController.EnableCurrentReckoningNextButton(FlipCard);
        }
        else
        {
            finishedText = "You survive for now.";
            GameManager.SingleInstance.App.Controller.reckoningMythosController.SetReckoningText(finishedText);
            GameManager.SingleInstance.App.Controller.reckoningMythosController.FinishReckoning();
        }
    }

    public void FlipCard()
    {
        GameManager.SingleInstance.App.Controller.openMenuController.HideOpenMenu();
        switch (copyIndex)
        {
            case 0:
                FlipEffect0();
                break;
            default:
                break;
        }
    }

    public void FlipEffect0()
    {
        string title = "One of the Thousand";
        string text = "The chanting reaches a fever pitch. The cult leader places a ritual dagger in your hand and tells you, 'The time has come. You must pay the blood you owe to the Children of the Black Goat.' Another investigator of your choice is devoured. \n\r Then discard this card.";
        GameManager.SingleInstance.App.Controller.conditionEffectController.StartFlipEffect(title, text, SelectInvestigator);
        GameManager.SingleInstance.App.Controller.conditionEffectController.ConditionEffectFinished();
    }

    public void SelectInvestigator()
    {
        List<MultipleOptionMenuObject> options = new List<MultipleOptionMenuObject>();
        foreach (Investigator i in GameManager.SingleInstance.App.Model.investigatorModel.investigators)
        {
            if (i.investigatorName != owner.investigatorName && i.deathEncounter == null)
            {
                options.Add(new MultipleOptionMenuObject(MultipleOptionType.Investigator, i));
            }
        }
        if (options.Count == 0)
        {
            string finishedText = "No other Investigators can be devoured \n\r Discard this Condition";
            GameManager.SingleInstance.App.Controller.reckoningMythosController.SetReckoningText(finishedText);
            DiscardCondition();
        }
        else
        {
            GameManager.SingleInstance.App.Controller.multipleOptionController.StartMultipleOption(options, "Select an Investigator to be devoured", InvestigatorSelected);
        }
    }

    public void InvestigatorSelected(int index)
    {
        // Devour selected Investigator
        List<Investigator> investigators = new List<Investigator>();
        foreach (Investigator i in GameManager.SingleInstance.App.Model.investigatorModel.investigators)
        {
            if (i.investigatorName != owner.investigatorName && i.deathEncounter == null)
            {
                investigators.Add(i);
            }
        }

        Investigator target = investigators[index];
        string devourText = target.investigatorName + " has been sacrificed \n\r Discard this Condition";
        GameManager.SingleInstance.App.Controller.reckoningMythosController.SetReckoningText(devourText);

        GameManager.SingleInstance.App.Controller.deadInvestigatorMenuController.InvestigatorDevoured(target, DiscardCondition);
    }

    public void DiscardCondition()
    {
        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(FinishFlipEffect0); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.ConditionLostEvent(this); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void FinishFlipEffect0()
    {
        owner.LoseCondition(this);
        GameManager.SingleInstance.App.Controller.openMenuController.EnableOpenMenu();
        GameManager.SingleInstance.App.Controller.reckoningMythosController.FinishReckoning();
    }
}
