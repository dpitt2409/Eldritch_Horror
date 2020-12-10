using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyTalismanAsset : Asset
{
    public LuckyTalismanAsset()
    {
        assetName = "Lucky Talisman";
        assetPortrait = GameManager.SingleInstance.App.Model.assetSpritesModel.luckyTalismanSprite;
        text = "Once per round, you may reroll all of your dice when resolving a test.";
        type = AssetType.Trinket;
        subTypes = new AssetSubType[1];
        subTypes[0] = AssetSubType.Relic;
        cost = 3;
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
