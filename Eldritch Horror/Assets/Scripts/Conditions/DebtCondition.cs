using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebtCondition : Condition
{
    public DebtCondition(int index)
    {
        conditionName = "Debt";
        type = ConditionType.Deal;
        conditionPortrait = GameManager.SingleInstance.App.Model.spriteModel.debtSprite;
        conditionText = "Local Action: Test Influence. If you pass, dicard this Condition.";
        reckoningText = "Some men have come to collect your debt. Flip this card.";
        copyIndex = index;
    }

    public override void Gained()
    {
        GameManager.SingleInstance.App.Model.eventModel.actionListEvent += ActionListEvent;
        GameManager.SingleInstance.App.Model.eventModel.reckoningEvent += ReckoningEvent;
    }

    public override void Lost()
    {
        GameManager.SingleInstance.App.Model.eventModel.actionListEvent -= ActionListEvent;
        GameManager.SingleInstance.App.Model.eventModel.reckoningEvent -= ReckoningEvent;
    }

    public void ActionListEvent()
    {
        string activeLocation = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator.currentLocation.locationName;
        string ownerLocation = owner.currentLocation.locationName;

        if (activeLocation == ownerLocation)
        {
            string actionName = owner.investigatorName + " Debt Local Action";
            GameManager.SingleInstance.App.Controller.actionPhaseController.AddActionToList(actionName, LocalAction);
        }
    }

    public void LocalAction()
    {
        GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator.ActionPerformed(owner.investigatorName + " Debt Local Action");
        GameManager.SingleInstance.App.Controller.conditionEffectController.StartConditionEffect(this, LocalActionComplete);
        GameManager.SingleInstance.App.Controller.testController.StartTest(TestStat.Influence, 0, TestType.Test, InfluenceTested);
    }

    public void InfluenceTested(List<int> results)
    {
        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator.SUCCESS)
            {
                numSuccesses++;
            }
        }

        if (numSuccesses > 0)
        {
            GameManager.SingleInstance.App.Controller.conditionEffectController.SetResultText("Pass \n\r Discard Debt Condition");

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(ConditionLostEventComplete); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.ConditionLostEvent(this); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.conditionEffectController.SetResultText("Fail");
            GameManager.SingleInstance.App.Controller.conditionEffectController.ConditionEffectFinished();
        }
    }

    public void ConditionLostEventComplete()
    {
        owner.LoseCondition(this);
        GameManager.SingleInstance.App.Controller.conditionEffectController.ConditionEffectFinished();
    }

    public void LocalActionComplete()
    {
        GameManager.SingleInstance.App.Controller.actionPhaseController.ActionPerformed();
    }

    public void ReckoningEvent()
    {
        string title = owner.investigatorName + " Debt Reckoning";
        ReckoningEvent re = new ReckoningEvent(title, reckoningText, StartReckoning, ReckoningSource.Investigator, owner);
        GameManager.SingleInstance.App.Model.reckoningMythosModel.AddReckoningEvent(re);
    }

    public void StartReckoning()
    {
        GameManager.SingleInstance.App.Controller.reckoningMythosController.EnableCurrentReckoningNextButton(FlipCard);
    }

    public void FlipCard()
    {
        //GameManager.SingleInstance.App.View.ReckoningMythosView.ReckoningMinimized();
        GameManager.SingleInstance.App.Controller.openMenuController.HideOpenMenu();
        switch (copyIndex)
        {
            case 0:
                FlipEffect0();
                break;
            case 1:
                FlipEffect1();
                break;
            default:
                break;
        }
    }

    public void FlipEffect0()
    {
        string title = "Hitmen";
        string text = "You see now that it was no ordinary bank you borrowed from. Some armed men confront you and demand that you repay what you owe. \n\r Test Strength \n\r If you fail, lose 3 Health. \n\r Then discard this card.";
        GameManager.SingleInstance.App.Controller.conditionEffectController.StartFlipEffect(title, text, FinishFlipEffect0);
        GameManager.SingleInstance.App.Controller.testController.StartTest(TestStat.Strength, 0, TestType.Test, Effect0Tested);
    }

    public void Effect0Tested(List<int> results)
    {
        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;

        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= active.SUCCESS)
            {
                numSuccesses++;
            }
        }

        if (numSuccesses == 0)
        {
            GameManager.SingleInstance.App.Controller.conditionEffectController.SetResultText("Fail \n\r Lose 3 Health and discard this Condition");
            active.SetIncomingDamage(3);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(LoseHealth); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.LoseHealthEvent(active, 3); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.conditionEffectController.SetResultText("Pass \n\r Discard this Condition");

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(ConditionLostEventComplete0); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.ConditionLostEvent(this); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void LoseHealth()
    {
        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;
        active.LoseHealth(active.GetIncomingDamage());
        active.SetIncomingDamage(0);

        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(HealthLost); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void HealthLost()
    {
        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(ConditionLostEventComplete0); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.ConditionLostEvent(this); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void ConditionLostEventComplete0()
    {
        owner.LoseCondition(this);
        GameManager.SingleInstance.App.Controller.conditionEffectController.ConditionEffectFinished(); // enable condition effect continue button
    }

    public void FinishFlipEffect0()
    {
        //GameManager.SingleInstance.App.View.ReckoningMythosView.ReckoningMaximized();
        GameManager.SingleInstance.App.Controller.openMenuController.EnableOpenMenu();

        GameManager.SingleInstance.App.Controller.reckoningMythosController.FinishReckoning();
        //GameManager.SingleInstance.App.Controller.reckoningMythosController.Next();
    }

    public void FlipEffect1()
    {
        string title = "Beyond Riches";
        string text = "The man wears a hat and a brown trenchcoat. 'We do not want money,' he hisses and grabs your throat. You feel as if part of your identity is being stolen from you. \n\r Test Will \n\r If you fail, a fragment of your soul is ripped away; lose 3 Sanity. \n\r Then discard this card.";
        GameManager.SingleInstance.App.Controller.conditionEffectController.StartFlipEffect(title, text, FinishFlipEffect1);
        GameManager.SingleInstance.App.Controller.testController.StartTest(TestStat.Will, 0, TestType.Test, Effect1Tested);
    }

    public void Effect1Tested(List<int> results)
    {
        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;

        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= active.SUCCESS)
            {
                numSuccesses++;
            }
        }

        if (numSuccesses == 0)
        {
            GameManager.SingleInstance.App.Controller.conditionEffectController.SetResultText("Fail \n\r Lose 3 Sanity and discard this Condition");
            active.SetIncomingDamage(3);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(LoseSanity); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.LoseSanityEvent(active, 3); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.conditionEffectController.SetResultText("Pass \n\r Discard this Condition");

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(ConditionLostEventComplete1); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.ConditionLostEvent(this); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void LoseSanity()
    {
        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;
        active.LoseSanity(active.GetIncomingDamage());
        active.SetIncomingDamage(0);

        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(SanityLost); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void SanityLost()
    {
        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(ConditionLostEventComplete1); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.ConditionLostEvent(this); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void ConditionLostEventComplete1()
    {
        owner.LoseCondition(this);
        GameManager.SingleInstance.App.Controller.conditionEffectController.ConditionEffectFinished(); // enable condition effect continue button
    }

    public void FinishFlipEffect1()
    {
        //GameManager.SingleInstance.App.View.ReckoningMythosView.ReckoningMaximized();
        GameManager.SingleInstance.App.Controller.openMenuController.EnableOpenMenu();

        GameManager.SingleInstance.App.Controller.reckoningMythosController.FinishReckoning();
        //GameManager.SingleInstance.App.Controller.reckoningMythosController.Next();
    }
}
