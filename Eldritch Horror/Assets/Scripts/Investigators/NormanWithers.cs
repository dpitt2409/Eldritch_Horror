using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormanWithers : Investigator
{
    void Start()
    {
        base.Start();

        investigatorName = "Norman Withers";

        occupation = "The Astronomer";
        flavorText = "Let them call me a crackpot! Something is happening to the stars, and I am not imagining it!";
        actionAbilityText = "Spend 2 Clues to discard 1 Monster on a space containing a Gate.";
        passiveAbilityText = "Once per round, you may spend 1 Sanity in place of spending 1 Clue.";
        bioText = "The scientific community ridiculed Norman for his claim that six stars disappeared from the sky. After exhausting every plausible astronomical explanation for answers, he took a position at Miskatonic University and began exploring more improbable possibilities in the restricted section of their library. While reading an ancient text of dark prophecies, Noman found an exact description of the phenomenon he'd observed. If the tome is to be believed, a terrible incursion into our world is imminent.";

        health = 5;
        sanity = 7;
        currentHealth = health;
        currentSanity = sanity;

        strength = 2;
        will = 4;
        influence = 1;
        observation = 3;
        lore = 3;

        startingLocation = App.Model.locationModel.FindLocationByName("Arkham");
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
            return new NormanWithersHealthDeathEncounter();
        }
        else
        {
            return new NormanWithersSanityDeathEncounter();
        }
    }
}

public class NormanWithersHealthDeathEncounter : Encounter
{
    public NormanWithersHealthDeathEncounter()
    {
        title = "In case of Crippling Injury or Death";
        testStat = TestStat.Will;
        testModifier = 0;
        encounterText = "Norman can barely move in his hospital bed and points to his notes and suitcase. Gain all of his possessions. He begs you to use his telescope to record the current locations of the stars. The work is slow and demanding, but you focus as best you can to give him accurate results. \n\r Test Will \n\r If you pass, Norman's insight into the Ancient One proves accurate; retreat Doom by 1. \n\r If you fail, Norman can make no sense of the night sky.";
        passText = "Pass \n\r Retreat Doom by 1";
        failText = "Fail";
    }

    public override void FinishEncounter(bool passed)
    {
        if (passed)
        {
            // Retreat doom by 1
            GameManager.SingleInstance.App.Model.doomModel.RetreatDoom(1);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DoomRetreated); // Start Queue
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

public class NormanWithersSanityDeathEncounter : Encounter
{
    public NormanWithersSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "The doctors would like you to look at Norman's notes and belongings. Gain all of his possessions. They are trying to gain some insight into his catatonic state. You try to convince them to move Norman to a room with a view of the night sky. \n\r Test Influence \n\r If you pass, Norman becomes slightly responsive, and you are able to question him; retreat Doom by 1. \n\r If you fail, Norman's condition grows steadily worse.";
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
