using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneScholarAsset : Asset
{
    public ArcaneScholarAsset()
    {
        assetName = "Arcane Scholar";
        assetPortrait = GameManager.SingleInstance.App.Model.spriteModel.arcaneScholarSprite;
        text = "Gain +1 Lore. You may reroll 1 die when resolving a Lore test.";
        type = AssetType.Ally;
        subTypes = new AssetSubType[0];
        cost = 2;
        artifact = false;
        magical = false;

        ownedInvestigator = null;
    }

    public override void Gained()
    {

    }

    public override void Lost()
    {

    }
}