﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedTheMindSpell : Spell
{
    private Investigator improvingInvestigator = null;
    private List<int> currentResults;
    private int currentSuccess;

    public FeedTheMindSpell(int index)
    {
        spellName = "Feed the Mind";
        spellPortrait = GameManager.SingleInstance.App.Model.spriteModel.feedTheMindSpellSprite;
        text = "Action: Test Lore -1 \n\r If you pass, choose an investigator on your space to improve 1 skill of his choice. \n\r Then flip this card.";
        type = SpellType.Ritual;
        reckoningText = "";
        copyIndex = index;
    }

    public override void Gained()
    {
        GameManager.SingleInstance.App.Model.eventModel.actionListEvent += ActionListEvent;
    }

    public override void Lost()
    {
        GameManager.SingleInstance.App.Model.eventModel.actionListEvent -= ActionListEvent;
    }

    public void ActionListEvent()
    {
        if (owner.investigatorName == GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator.investigatorName)
        {
            string actionName = "Feed the Mind Spell Action";
            GameManager.SingleInstance.App.Controller.actionPhaseController.AddActionToList(actionName, SpellAction);
        }
    }

    public void SpellAction()
    {
        GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator.ActionPerformed("Feed the Mind Spell Action");
        GameManager.SingleInstance.App.Controller.spellEffectController.StartSpellEffect(this, SpellActionComplete);
        GameManager.SingleInstance.App.Controller.testController.StartTest(TestStat.Lore, -1, TestType.Test, LoreTested);
    }

    public void LoreTested(List<int> results)
    {
        currentResults = new List<int>(results);

        currentSuccess = 0;
        foreach (int num in currentResults)
        {
            if (num >= owner.SUCCESS)
            {
                currentSuccess++;
            }
        }

        if (currentSuccess > 0)
        {
            GameManager.SingleInstance.App.Controller.openMenuController.HideOpenMenu();
            GameManager.SingleInstance.App.Controller.spellEffectController.SetFrontResultText("Pass \n\r Choose an Investigator on your space to improve 1 skill of his choice. \n\r Then flip this card.");
            // Open multi option menu to select an investigator on owner's location
            List<MultipleOptionMenuObject> options = new List<MultipleOptionMenuObject>();
            foreach (Investigator inv in owner.currentLocation.investigatorsOnLocation)
            {
                options.Add(new MultipleOptionMenuObject(MultipleOptionType.Investigator, inv));
            }
            GameManager.SingleInstance.App.Controller.multipleOptionController.StartMultipleOption(options, "Select an Investigator", InvestigatorSelected);
        }
        else
        {
            GameManager.SingleInstance.App.Controller.spellEffectController.SetFrontResultText("Fail \n\r Flip this card.");
            GameManager.SingleInstance.App.Controller.spellEffectController.SpellEffectFinished();
        }
    }

    public void InvestigatorSelected(int index)
    {
        List<Investigator> investigators = owner.currentLocation.investigatorsOnLocation;
        for (int i = 0; i < investigators.Count; i++)
        {
            if (i == index)
            {
                improvingInvestigator = investigators[i];
            }
        }
        if (improvingInvestigator == null)
        {
            Debug.LogError("Error selecting investigator");
        }
        else
        {
            List<MultipleOptionMenuObject> options = new List<MultipleOptionMenuObject>();
            foreach (TestStat stat in improvingInvestigator.ImprovableSkills())
            {
                options.Add(new MultipleOptionMenuObject(MultipleOptionType.Stat, stat));
            }
            GameManager.SingleInstance.App.Controller.multipleOptionController.StartMultipleOption(options, "Select a Skill to Improve", SkillSelected);
        }
    }

    public void SkillSelected(int index)
    {
        TestStat improve = TestStat.None;
        List<TestStat> stats = improvingInvestigator.ImprovableSkills();
        for (int i = 0; i < stats.Count; i++)
        {
            if (i == index)
            {
                improve = stats[i];
            }
        }
        if (improve == TestStat.None)
        {
            Debug.LogError("Error selecting stat");
        }
        else
        {
            improvingInvestigator.ImproveStat(improve);

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(StatImproved); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.StatImprovedEvent(improvingInvestigator, improve); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void StatImproved()
    {
        improvingInvestigator = null;
        GameManager.SingleInstance.App.Controller.spellEffectController.SpellEffectFinished();
        GameManager.SingleInstance.App.Controller.openMenuController.EnableOpenMenu();
    }

    // Called after continue button is pressed on front effect
    public void SpellActionComplete()
    {
        switch (copyIndex)
        {
            case 2:
                FlipEffect2();
                break;
            default:
                break;
        }
    }

    public void FlipEffect2()
    {
        List<string> options = new List<string>();
        options.Add("0: You were unable to sufficiently focus your mind. You wonder if you simply lack the mental discipline this spell requires. If you did not roll any 4's, discard this card.");
        options.Add("1+: Casting your thoughts into the ether, your mind returns with strange knowledge. You may not entirely be the same person you were before. Lose 1 Sanity.");
        
        if (currentSuccess == 0)
        {
            GameManager.SingleInstance.App.Controller.spellEffectController.StartFlipEffect(this, options, 0, FlipEffectComplete);
            bool rolled4 = false;
            foreach (int num in currentResults)
            {
                if (num == 4)
                    rolled4 = true;
            }
            if (rolled4)
            {
                GameManager.SingleInstance.App.Controller.spellEffectController.SpellEffectFinished();
            }
            else
            {
                GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(SpellLost); // Create Queue
                GameManager.SingleInstance.App.Model.eventModel.SpellLostEvent(this); // Populate Queue
                GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
            }
        }
        else
        {
            GameManager.SingleInstance.App.Controller.spellEffectController.StartFlipEffect(this, options, 1, FlipEffectComplete);
            owner.SetIncomingDamage(1);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(LoseHealth); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.LoseHealthEvent(owner, 1); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void SpellLost()
    {
        owner.LoseSpell(this);
        GameManager.SingleInstance.App.Controller.spellEffectController.SpellEffectFinished();
    }

    public void LoseHealth()
    {
        owner.LoseSanity(owner.GetIncomingDamage());
        owner.SetIncomingDamage(0);

        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(HealthLost); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void HealthLost()
    {
        GameManager.SingleInstance.App.Controller.spellEffectController.SpellEffectFinished();
    }

    public void FlipEffectComplete()
    {
        GameManager.SingleInstance.App.Controller.actionPhaseController.ActionPerformed();
    }
}
