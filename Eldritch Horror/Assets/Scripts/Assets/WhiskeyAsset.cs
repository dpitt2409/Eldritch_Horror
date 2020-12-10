using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiskeyAsset : Asset
{
    public WhiskeyAsset()
    {
        assetName = "Whiskey";
        assetPortrait = GameManager.SingleInstance.App.Model.assetSpritesModel.whiskeySprite;
        text = "You may discard this card to prevent an investigator on your space from losing up to 2 Sanity.";
        type = AssetType.Item;
        subTypes = new AssetSubType[0];
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
