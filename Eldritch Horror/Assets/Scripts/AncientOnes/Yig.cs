using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yig : AncientOne
{
    public Yig()
    {
        ancientOneName = "Yig";
        portrait = GameManager.SingleInstance.App.Model.ancientOneSpritesModel.yigSprite;
        title = "The Father of Serpents";
        doom = 10;
        numMysteries = 3;

        flavorText = "Many know that Yig's punishment for those who harm his progeny is a terrible curse, but few know that long ago, the serpent people betrayed Yig and suffered his wrath. Now the survivors return, eager to conquer for their true master.";
        ancientOneTexts = new string[3];
        ancientOneTexts[0] = "Spawn 1 Cultist Monster on the Active Expedition space. Then, if there are 2 or more Monsters on that space, advance Doom by 1.";
        ancientOneTexts[1] = "When 3 Mysteries have been solved, investigators win the game";
        ancientOneTexts[2] = "When Yig awakens, flip this sheet and resolve the 'Yig Awakens!' effect on the back.";
        frontReckoningTextIndex = 0;

        awakenText = "Each investigator that has a Poisoned Condition gains a Cursed Condition. \n\r Place 8 Eldritch tokens on this sheet.";
        finalMysteryTitle = "Serpent's Nest";
        finalMysteryFlavorText = "The serpent people have regained the favor of Yig and now call upon him to rid their kingdom of the wretched humans that infest it.";
        finalMysteryText = "When 3 Mysteries have been solved, you have discovered Yig's hiding place in Central America; move each Cultist Monster on the game board to space 7 and spawn the Yig Epic Monster on that space. \n\r When the Yig Epic Monster is defeated, the Final Mystery is solved and investigators win the game.";

        flipInfoTitle = "The Father of Serpents";
        flipTexts = new string[4];
        flipTexts[0] = "When an investigator is defeated or devoured, that player is not eliminated.";
        flipTexts[1] = "Each time Doom would advance, discard 1 Eldritch token from this sheet instead. Then, if there are no Eldritch tokens on this sheet, investigators lose the game.";
        flipTexts[2] = "Each time Doom would retreat, 1 investigator may discard a Cursed Condition instead.";
        flipTexts[3] = "Spawn 1 Cultist Monster on a space containing a Mystery token Eldritch token, or Epic Monster.";
        backReckoningTextIndex = 2;

        cultist1 = new YigCultist1();
        cultist2 = new YigCultist2();
    }

    public override Dictionary<LocationType, List<Encounter>> CreateResearchDeck()
    {
        Dictionary<LocationType, List<Encounter>> researchDecks = new Dictionary<LocationType, List<Encounter>>();

        // City
        List<Encounter> cityEncounters = new List<Encounter>();
        cityEncounters.Add(new YigCityResearchEncounter11());

        // Sea
        List<Encounter> seaEncounters = new List<Encounter>();
        seaEncounters.Add(new YigSeaResearchEncounter17());

        List<Encounter> wildernessEncounters = new List<Encounter>();
        wildernessEncounters.Add(new YigWildernessResearchEncounter11());

        researchDecks.Add(LocationType.City, cityEncounters);
        researchDecks.Add(LocationType.Sea, seaEncounters);
        researchDecks.Add(LocationType.Wilderness, wildernessEncounters);

        return researchDecks;
    }

    public override void EnterPlay()
    {
        base.EnterPlay();
    }

    public override void LeavePlay()
    {
        base.LeavePlay();
    }

    // Only called when all mysteries are solved and the Ancient One has already flipped
    public override void AllMysteriesSolved()
    {

    }
}
