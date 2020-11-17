using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AncientOne
{
    public string ancientOneName;

    public Sprite portrait;

    public string title;

    public int doom;

    public int numMysteries;

    public string flavorText;

    public string[] ancientOneTexts;

    public int frontReckoningTextIndex;

    public string awakenText;

    // Probably move mystery info into its own thing
    public string finalMysteryTitle;
    public string finalMysteryFlavorText;
    public string finalMysteryText;

    public string flipInfoTitle;

    public string[] flipTexts;

    public int backReckoningTextIndex;

    public Monster cultist1;

    public Monster cultist2;

    public abstract Dictionary<LocationType, List<Encounter>> CreateResearchDeck();

}
