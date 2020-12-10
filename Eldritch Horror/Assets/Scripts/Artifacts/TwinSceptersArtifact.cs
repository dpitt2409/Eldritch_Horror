using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinSceptersArtifact : Asset
{
    public TwinSceptersArtifact()
    {
        assetName = "Twin Scepters";
        assetPortrait = GameManager.SingleInstance.App.Model.assetSpritesModel.twinSceptersSprite;
        text = "Gain +4 Strength during Combat Encounters. \n\r Gain +4 Lore when resolving Spell effects.";
        type = AssetType.Item;
        subTypes = new AssetSubType[1] { AssetSubType.Relic };
        artifact = true;
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
