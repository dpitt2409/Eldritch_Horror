using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyChen : Investigator
{
    public void Start()
    {
        base.Start();

        investigatorName = "Lily Chen";

        occupation = "The Martial Artist";
        flavorText = "I have been preparing to confront this evil for my entire life. My focus must be absolute.";
        actionAbilityText = "Spend any number of Health or Sanity, then recover an equal number of Health or Sanity.";
        passiveAbilityText = "When you improve a skill, you may immediately improve that skill again.";
        bioText = "Lily speaks rarely and when she does, her words are measured and wise. After a lifetime of disciplined training, every gesture is graceful, uncluttered by hesitation. When she was an infant, an obscure sect of monks believed that she was born for a special purpose, to face a great evil. Now, the monks beleive that the great evil is at hand, and they have brought Lily to Shanghai to begin fulfilling her destiny.";

        health = 6;
        sanity = 6;
        currentHealth = health;
        currentSanity = sanity;

        strength = 4;
        will = 3;
        influence = 2;
        observation = 2;
        lore = 2;

        startingLocation = App.Model.locationModel.FindLocationByName("Shanghai");
        startingItems = new StartingItem[0];
    }

    public override void JoinGame()
    {
        base.JoinGame();

        //App.Model.eventModel.actionListEvent += AddPlayerActionToList;
    }

    public override Encounter SetDeathEncounter(bool health)
    {
        if (health)
        {
            return new LilyChenHealthDeathEncounter();
        }
        else
        {
            return new LilyChenSanityDeathEncounter();
        }
    }

    public void AddPlayerActionToList()
    {
        if (!(App.Model.investigatorModel.activeInvestigator.investigatorName == investigatorName))
            return;

        App.Controller.actionPhaseController.AddActionToList("Lily Chen Action", PlayerAction);
    }

    public void PlayerAction()
    {

    }

}

public class LilyChenHealthDeathEncounter : Encounter
{
    public LilyChenHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Strength;
        testModifier = 0;
        encounterText = "When you reach the hospital, you find Lily's things, but she is not in her bed. Gain all of her possessions. You quickly find that she's been kidnapped by cultists! You fight to free her \n\r Test Strength \n\r If you pass, interrogate the cultists, and Lily is safely returned to the hospital to begin the slow healing process; retreat Doom by 1. If you fail, Lily, disappears into the darkness.";
        passText = "Pass \n\r Retreat Doom by 1";
        failText = "Fail";
    }

    public override void FinishEncounter(bool passed)
    {
        if (passed)
        {
            // Retreat doom by 1
            GameManager.SingleInstance.App.Model.doomModel.RetreatDoom(1);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DoomRetreated); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.DoomRetreatedEvent(1); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void DoomRetreated()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }
}

public class LilyChenSanityDeathEncounter : Encounter
{
    public LilyChenSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis.";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "You barely recognize the small woman huddled in an alleyway, her few belongings scattered next to her. Gain all of her posssessions. 'It wasn't true,' Lily sobs. 'I have no no destiny. I cannot fight the darkness.' You reassure her that she has already done her part and that others will finish the task. \n\r Test Influence \n\r If you pass, she chants over you, passing her destiny on to you; retreat Doom by 1. If you fail, Lily disappears into the darkness.";
        passText = "Pass \n\r Retreat Doom by 1";
        failText = "Fail";
    }

    public override void FinishEncounter(bool passed)
    {
        if (passed)
        {
            // Retreat doom by 1
            GameManager.SingleInstance.App.Model.doomModel.RetreatDoom(1);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DoomRetreated); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.DoomRetreatedEvent(1); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void DoomRetreated()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }
}