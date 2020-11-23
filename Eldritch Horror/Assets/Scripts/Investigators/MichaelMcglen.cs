using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelMcglen : Investigator
{
    void Start()
    {
        base.Start();

        investigatorName = "Michael McGlen";

        occupation = "The Gangster";
        flavorText = "Don't care if it's a god. If it crosses me, it's gonna regret it.";
        actionAbilityText = "If you are on a City space, you may gain 1 Item or Service Asset of your choice from the reserve. If you do, gain a Wanted Condition.";
        passiveAbilityText = "Once per round, you may reroll 1 die when resolivng a Deal or Pursuit Condition effect.";
        bioText = "When the O'Bannion gang needs to send a message, they send the big man, Michael McGlen. Not long ago, his friend Fast Louie Farrell was attacked by a bunch of inhuman things with ugly, fish-like faces. They sliced him up and dragged him under the water. McGlen took it personally. The word has spread: McGlen has loaded up his Thompson and everyone else had best get out of the way. His first stop is London where he intends to find out what these creatures are called and where he has to go to kill every last one of them.";

        health = 8;
        sanity = 4;
        currentHealth = health;
        currentSanity = sanity;

        strength = 4;
        will = 3;
        influence = 3;
        observation = 1;
        lore = 2;

        startingLocation = App.Model.locationModel.FindLocationByName("London");
        startingItems = new StartingItem[0];
    }

    public override void JoinGame()
    {
        base.JoinGame();
    }

    public override Encounter SetDeathEncounter(bool health)
    {
        if (health)
        {
            return new MichaelMcGlenHealthDeathEncounter();
        }
        else
        {
            return new MichaelMcGlenSanityDeathEncounter();
        }
    }
}

public class MichaelMcGlenHealthDeathEncounter : Encounter
{
    public MichaelMcGlenHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Strength;
        testModifier = 0;
        encounterText = "McGlen disappeared from the hospital, leaving his things behind. Gain all of his possessions. Over the years, McGlen hurt a lot of people, and now some local thugs plan to take revenge. You track down one of the kidnappers and try to force him to tell you where they are keeping him. \n\r Test Strength \n\r If you pass, you find McGlen wounded but in good spirits; retreat Doom by 1. \n\r If you fail, the police eventually find his body after what looks to have been a very violent death.";
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

public class MichaelMcGlenSanityDeathEncounter : Encounter
{
    public MichaelMcGlenSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis.";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "You barely recognize the small woman huddled in an alleyway, her few belongings scattered next to her. Gain all of her posssessions. 'It wasn't true,' Lily sobs. 'I have no no destiny. I cannot fight the darkness.' You reassure her that she has already done her part and that others will finish the task. \n\r Test Influence \n\r If you pass, she chants over you, passing her destiny on to you; retreat Doom by 1. If you fail, Lily disappears into the darkness.";
        passText = "Pass";
        failText = "Fail";
    }

    public override void StartEncounter()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }

}
