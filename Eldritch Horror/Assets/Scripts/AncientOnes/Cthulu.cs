using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cthulu : AncientOne
{
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
}
