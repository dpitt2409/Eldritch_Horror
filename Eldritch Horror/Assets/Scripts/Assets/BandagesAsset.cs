using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandagesAsset : Asset
{
    private Investigator ownerReference;
    private Investigator targetReference;

    public BandagesAsset()
    {
        assetName = "Bandages";
        assetPortrait = GameManager.SingleInstance.App.Model.assetSpritesModel.bandagesSprite;
        text = "You may discard this card to prevent an investigator on your space from losing up to 2 Health.";
        type = AssetType.Item;
        subTypes = new AssetSubType[0];
        cost = 1;
        artifact = false;
        magical = false;
        reckoningText = "";

        ownedInvestigator = null;
    }

    public override void Gained()
    {
        GameManager.SingleInstance.App.Model.eventModel.loseHealthEvent += LoseHealthEvent;
    }

    public override void Lost()
    {
        GameManager.SingleInstance.App.Model.eventModel.loseHealthEvent -= LoseHealthEvent;
    }

    public void LoseHealthEvent(Investigator i, int amount)
    {
        targetReference = i;
        ownerReference = ownedInvestigator;
        EventAction e = new EventAction(EventType.Optional, UseAsset, EventValid, this);
        GameManager.SingleInstance.App.Controller.queueController.AddCallBack(e);
    }

    public void UseAsset()
    {
        int incoming = targetReference.GetIncomingDamage();
        incoming -= 2;
        if (incoming < 0)
            incoming = 0;
        targetReference.SetIncomingDamage(incoming);
        ownerReference.LoseAsset(this);
        GameManager.SingleInstance.App.Controller.queueController.NextCallBack();
    }

    public bool EventValid() // Event is valid if the Asset ownership hasnt changed (owner hasn't died) and the inital target is still going to take damage
    {
        return (ownedInvestigator.investigatorName == ownerReference.investigatorName && targetReference.GetIncomingDamage() > 0);
    }
}
