using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAsset : Asset
{
    public AxeAsset()
    {
        assetName = "Axe";
        assetPortrait = GameManager.SingleInstance.App.Model.spriteModel.axeSprite;
        text = "Gain +2 Strength during Combat Encounters.  You may spend 2 Sanity to reroll any number of dice when resolving a Strength test during a Combat Encounter.";
        type = AssetType.Item;
        subTypes = new AssetSubType[1];
        subTypes[0] = AssetSubType.Weapon;
        cost = 2;
        artifact = false;
        magical = false;
        reckoningText = "";

        ownedInvestigator = null;
    }

    public override void Gained()
    {

    }

    public override void Lost()
    {

    }
}
