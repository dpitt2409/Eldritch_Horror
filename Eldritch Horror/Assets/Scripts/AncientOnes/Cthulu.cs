using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cthulu : AncientOne
{
    int postFlipSanityTokens = 0;
    int dealSanityInvestigatorIndex = 0;

    public Cthulu()
    {
        ancientOneName = "Cthulu";
        portrait = GameManager.SingleInstance.App.Model.spriteModel.cthuluSprite;
        title = "The Madness from the Sea";
        doom = 12;
        numMysteries = 3;

        flavorText = "Eons ago, Cthulu came from the stars with his Star Spawn brethren. He now sleeps in the sunken city R'lyeh, waiting for the stars to be right to rise again.";
        ancientOneTexts = new string[4];
        ancientOneTexts[0] = "When an investigator moves onto a space containing an Eldritch token, he becomes Delayed and loses 1 Sanity.";
        ancientOneTexts[1] = "Each Investigator on a Sea space that does not contain an Eldritch token places an Eldritch token on his space.";
        ancientOneTexts[2] = "When 3 Mysteries have been solved, investigators win the game.";
        ancientOneTexts[3] = "When Cthulu awakens, flip this sheet.";
        frontReckoningTextIndex = 1;

        awakenText = "";
        finalMysteryTitle = "Risen From the Sea";
        finalMysteryFlavorText = "At last the time has come, the deep ones have broken the elder sign that kept their master asleep. Cthulu rises again, and madness fills the dreams of every living thing.";
        finalMysteryText = "When 3 Mysteries have been solved, Cthulu rises from the sea; spawn the Cthulu Epic Monster on space 3. \n\r When the Cthulu Epic Monster is defeated, the Final Mystery is solved and investigators win the game.";

        flipInfoTitle = "Madness from the Sea";
        flipTexts = new string[3];
        flipTexts[0] = "When an investigator moves onto a space containing an Eldritch token, he becomes Delayed, loses 1 Sanity, and places 1 Sanity token on this sheet.";
        flipTexts[1] = "Each time Doom would advance, place 1 Sanity token on this sheet instead.";
        flipTexts[2] = "Each investigator loses 1 Sanity for each Sanity token on this sheet. Then, if all investigators have been eliminated, investigators lose the game.";
        backReckoningTextIndex = 2;
        
        cultist1 = new CthuluCultist1();
        cultist2 = new CthuluCultist2();
    }

    public override Dictionary<LocationType, List<Encounter>> CreateResearchDeck()
    {
        Dictionary<LocationType, List<Encounter>> researchDecks = new Dictionary<LocationType, List<Encounter>>();
        
        // City
        List<Encounter> cityEncounters = new List<Encounter>();
        cityEncounters.Add(new CthuluCityResearchEncounter5());
        
        // Sea
        List<Encounter> seaEncounters = new List<Encounter>();
        seaEncounters.Add(new CthuluSeaResearchEncounter4());

        List<Encounter> wildernessEncounters = new List<Encounter>();
        wildernessEncounters.Add(new CthuluWildernessResearchEncounter4());

        researchDecks.Add(LocationType.City, cityEncounters);
        researchDecks.Add(LocationType.Sea, seaEncounters);
        researchDecks.Add(LocationType.Wilderness, wildernessEncounters);

        return researchDecks;
    }

    public override void EnterPlay()
    {
        GameManager.SingleInstance.App.Model.eventModel.doomAdvancedEvent += DoomAdvancedEvent;
        GameManager.SingleInstance.App.Model.eventModel.monsterKilledEvent += MonsterKilledEvent;
        GameManager.SingleInstance.App.Model.eventModel.reckoningEvent += ReckoningEvent;
        GameManager.SingleInstance.App.Model.eventModel.travelEvent += TravelEvent;
    }

    public override void LeavePlay()
    {
        GameManager.SingleInstance.App.Model.eventModel.doomAdvancedEvent -= DoomAdvancedEvent;
        GameManager.SingleInstance.App.Model.eventModel.monsterKilledEvent -= MonsterKilledEvent;
        GameManager.SingleInstance.App.Model.eventModel.reckoningEvent -= ReckoningEvent;
        GameManager.SingleInstance.App.Model.eventModel.travelEvent -= TravelEvent;
    }

    // Only called when all mysteries are solved and the Ancient One has already flipped
    public override void AllMysteriesSolved()
    {
        finalMysteryActive = true;

        Monster m = new CthuluEpicMonster();
        Location l = GameManager.SingleInstance.App.Model.locationModel.FindLocationByName("Space 3");
        GameManager.SingleInstance.App.Controller.locationController.SpawnMonsterOnLocation(m, l);
        GameManager.SingleInstance.App.Model.monsterModel.SpawnEpicMonster(m);
    }

    public void AddSanityToken(int num)
    {
        postFlipSanityTokens += num;
        GameManager.SingleInstance.App.Controller.ancientOneController.UpdatePostFlipTokenVal(postFlipSanityTokens);
    }

    public void DoomAdvancedEvent(int num)
    {
        if (!flipped)
        {
            if (GameManager.SingleInstance.App.Model.doomModel.currentDoom == 0)
            {
                flipped = true;
                GameManager.SingleInstance.App.Controller.locationController.AncientOneFlipped();
                postFlipSanityTokens = 1;
                GameManager.SingleInstance.App.Controller.ancientOneController.EnablePostFlipTokens(GameManager.SingleInstance.App.Model.spriteModel.sanitySprite, postFlipSanityTokens);
            }
        }
        else
        {
            postFlipSanityTokens += num;
            GameManager.SingleInstance.App.Controller.ancientOneController.UpdatePostFlipTokenVal(postFlipSanityTokens);
        }
    }

    public void MonsterKilledEvent(Monster m)
    {
        if (m.monsterName == "Cthulu")
        {
            // Investigators win the game
            GameManager.SingleInstance.App.Controller.endGameController.EndGame(true);
        }
    }

    public void ReckoningEvent()
    {
        string title = ancientOneName + " Reckoning";
        if (!flipped)
        {
            ReckoningEvent re = new ReckoningEvent(title, ancientOneTexts[frontReckoningTextIndex], StartFrontReckoning, ReckoningSource.AncientOne, portrait);
            GameManager.SingleInstance.App.Model.reckoningMythosModel.AddReckoningEvent(re);
        }
        else
        {
            ReckoningEvent re = new ReckoningEvent(title, flipTexts[backReckoningTextIndex], StartBackReckoning, ReckoningSource.AncientOne, portrait);
            GameManager.SingleInstance.App.Model.reckoningMythosModel.AddReckoningEvent(re);
        }
    }

    public void StartFrontReckoning()
    {
        string screenText = ancientOneTexts[frontReckoningTextIndex] + "\n\r";
        foreach (Investigator i in GameManager.SingleInstance.App.Model.investigatorModel.investigators)
        {
            Location l = i.currentLocation;
            if (l.type == LocationType.Sea && l.eldritchTokensOnLocation.Count == 0)
            {
                GameManager.SingleInstance.App.Controller.locationController.SpawnEldritchTokenOnLocation(new EldritchToken(), l);
                screenText += "\n\r Eldritch Token Spawned in " + l.locationName;
            }
        }
        GameManager.SingleInstance.App.Controller.reckoningMythosController.SetReckoningText(screenText);
        GameManager.SingleInstance.App.Controller.reckoningMythosController.FinishReckoning();
    }

    public void StartBackReckoning()
    {
        dealSanityInvestigatorIndex = -1;
        DealNextSanityDamage();
    }

    public void DealNextSanityDamage()
    {
        dealSanityInvestigatorIndex++;
        if (dealSanityInvestigatorIndex == GameManager.SingleInstance.App.Model.investigatorModel.investigators.Count)
        {
            string screenText = "Each Investigator loses " + postFlipSanityTokens + " Sanity";
            GameManager.SingleInstance.App.Controller.reckoningMythosController.SetReckoningText(screenText);
            GameManager.SingleInstance.App.Controller.reckoningMythosController.FinishReckoning();
        }
        else
        {
            Investigator i = GameManager.SingleInstance.App.Model.investigatorModel.investigators[dealSanityInvestigatorIndex];
            if (i.deathEncounter != null) // Investigator is dead
            {
                DealNextSanityDamage();
            }
            else
            {
                i.SetIncomingDamage(postFlipSanityTokens);
                GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(FlipLoseSanity); // Create Queue
                GameManager.SingleInstance.App.Model.eventModel.LoseSanityEvent(i, postFlipSanityTokens); // Populate Queue
                GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
            }
        }
    }

    public void FlipLoseSanity()
    {
        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.investigators[dealSanityInvestigatorIndex];
        active.LoseSanity(active.GetIncomingDamage());
        active.SetIncomingDamage(0);

        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DealNextSanityDamage); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void TravelEvent(Location l)
    {
        if (l.eldritchTokensOnLocation.Count > 0)
        {
            EventAction e = new EventAction("Cthulu Travel Event", TravelEventCallBack);
            GameManager.SingleInstance.App.Controller.queueController.AddCallBack(e);
        }
    }

    public void TravelEventCallBack()
    {
        GameManager.SingleInstance.App.Model.actionPhaseModel.TurnToBeTerminated();

        postFlipSanityTokens++;
        GameManager.SingleInstance.App.Controller.ancientOneController.UpdatePostFlipTokenVal(postFlipSanityTokens);

        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;
        active.SetIncomingDamage(1);
        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(LoseSanity); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.LoseSanityEvent(active, 1); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
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
        GameManager.SingleInstance.App.Controller.queueController.NextCallBack();
    }
}
