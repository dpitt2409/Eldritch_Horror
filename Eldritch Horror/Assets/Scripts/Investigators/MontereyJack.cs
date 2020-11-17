using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontereyJack : Investigator
{
    void Start()
    {
        base.Start();
        
        investigatorName = "Monterey Jack";

        occupation = "The Archaeologist";
        flavorText = "To find a treasure like that, I'll risk a few poison darts.";
        actionAbilityText = "You may discard 1 Artifcat to retreat Doom by 1; or discard the top card of the Expedition Encounter deck and perform 1 additional action.";
        passiveAbilityText = "After resolving an Expedition Encounter, gain 1 RELIC Unique Asset.";
        bioText = "Young Jack traveled the world with his father's archaeological expeditions. He acquired the nickname 'Monterey' after a bout of quinine-induced jaundice turned his skin yellow. Once grown, Jack became an accomplished archaeologist in his own right. Now he must explore his own past. His father was found murdered with an arcane sigil carved into the old man's forehead. Here in Cairo, he has seen men wearing this sigil on pendants around their neck. It is time for Jack to start digging.";

        health = 7;
        sanity = 5;
        currentHealth = health;
        currentSanity = sanity;

        strength = 4;
        will = 2;
        influence = 2;
        observation = 3;
        lore = 2;

        startingLocation = App.Model.locationModel.FindLocationByName("The Pyramids");
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
            return new MontereyJackHealthDeathEncounter();
        }
        else
        {
            return new MontereyJackSanityDeathEncounter();
        }
    }
}

public class MontereyJackHealthDeathEncounter : Encounter
{
    public MontereyJackHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Strength;
        testModifier = 0;
        encounterText = "A lifetime of venom from spiders, scorpions, and snakes is taking its final toll on Jack. Even as he struggles to breathe, he shares anecdotes about each of his belongings. Gain all of his possessions. He tells you he buried his best finds in a field, but he cannot remember where. You spend hours digging up that field. \n\r Test Strength \n\r If you pass, you discover Jack was not lying; gain 2 Relic Unique Assets. If you fail, you exhaust yourself with no result.";
        passText = "Pass \n\r Gain 2 Relic Unique Assets";
        failText = "Fail";
    }

    public override void FinishEncounter(bool passed)
    {
        if (passed)
        {
            // Gain 2 Relic Unique Assets
            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
        else
        {
            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }
}

public class MontereyJackSanityDeathEncounter : Encounter
{
    public MontereyJackSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "Jack has carved an arcane sigil into his flesh dozens of times. The asylum no longer allows him access to his belongings for fear he will continue. Gain all of his possessions. He says he must keep digging and tears at his skin with his fingernails. You beg him to stop. \n\r Test Influence \n\r If you pass, he calms down enough to explain the significance of the symbol and how to use it against the coming darkness; retreat Doom by 1. \n\r If you fail, the orderlies are forced to sedate him.";
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
            GameManager.SingleInstance.App.Model.eventModel.DoomAdvanced(); // Populate Queue
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
