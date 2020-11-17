using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateInvestigatorAsset : Asset
{
    public PrivateInvestigatorAsset()
    {
        assetName = "Private Investigator";
        assetPortrait = GameManager.SingleInstance.App.Model.spriteModel.privateInvestigatorSprite;
        text = "Gain +1 Observation. You may reroll 1 die when resolving an observation test.";
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
