using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharlieKane : Investigator
{
    void Start()
    {
        base.Start();

        investigatorName = "Charlie Kane";

        occupation = "The Politician";
        flavorText = "It can be arranged. It's just a matter of acceptable terms.";
        actionAbilityText = "Another investigator of your choice may immediately perform 1 additional action.";
        passiveAbilityText = "When you perform an Acquire Assets action, you may allow other investigators to gain any cards you purchase.";
        bioText = "When the press asks if Charlie is planning a run for national office, he smiles and says that he's focused on the important issues. The truth is that he would love to launch his campaign, but right now the most important issue is preventing the end of the world without causing a panic. To do this, he's been calling in fabors across the country. Most recently, Charlie's stopped in San Francisco to visit Hearst Castle. With the help of his friends and finances, Charlie believes he can fix this problem without sacrificing a single vote.";

        health = 4;
        sanity = 8;
        currentHealth = health;
        currentSanity = sanity;

        strength = 2;
        will = 2;
        influence = 4;
        observation = 3;
        lore = 2;

        startingLocation = App.Model.locationModel.FindLocationByName("San Francisco");
        startingItems = new StartingItem[1];
        startingItems[0] = new StartingItem(StartingItemType.Asset, "Personal Assistant");
    }

    public override void JoinGame()
    {
        base.JoinGame();
    }

    public override Encounter SetDeathEncounter(bool health)
    {
        if (health)
        {
            return new CharlieKaneHealthDeathEncounter();
        }
        else
        {
            return new CharlieKaneSanityDeathEncounter();
        }
    }
}

public class CharlieKaneHealthDeathEncounter : Encounter
{
    public CharlieKaneHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "The nurse at the front desk hands you a parcel. Gain all of his possessions. 'Mr Kane said to give you this package, but the doctor insists that no visitors be admitted.' You try to convince her to make an exception for you. \n\r Test Influence \n\r If you pass, you find that Charlie's health is beyond recovery, but he's still in good spirits and you have a long talk; retreat Doom by 1. \n\r If you fail, Charlie spends the rest of his days cut off from all human contact.";
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
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start the Queue
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

public class CharlieKaneSanityDeathEncounter : Encounter
{
    public CharlieKaneSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "The first thing Charlie does when you enter the restaurant is hand you all of his belongings. Gain all of his possessions. He offers you deals, promising to sell you Atlantis and introduce you to Caesar. You negotiate carefully with him. \n\r Test Influence \n\r If you pass, he tells you all he knows in exchange for your napkin ring and a salt shaker; retreat Doom by 1. \n\r If you fail, Charlie gets angry and insists that you'll be sorry when he's the President of the world.";
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
