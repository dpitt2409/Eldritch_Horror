using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalAssistantAsset : Asset
{
    public PersonalAssistantAsset()
    {
        assetName = "Personal Assistant";
        assetPortrait = GameManager.SingleInstance.App.Model.spriteModel.personalAssistantSprite;
        text = "Gain +1 Influence. You may reroll 1 die when resolving an influence test.";
        type = AssetType.Ally;
        subTypes = new AssetSubType[0];
        cost = 2;
        artifact = false;
        magical = false;

        ownedInvestigator = null;
    }

    public override void Gained()
    {
        GameManager.SingleInstance.App.Model.eventModel.preTestEvent += PreTestEvent;
        GameManager.SingleInstance.App.Model.eventModel.postTestEvent += PostTestEvent;
    }

    public override void Lost()
    {
        GameManager.SingleInstance.App.Model.eventModel.preTestEvent -= PreTestEvent;
        GameManager.SingleInstance.App.Model.eventModel.postTestEvent -= PostTestEvent;
    }

    public void PreTestEvent()
    {
        EventAction e = new EventAction(assetName, AddBonus);
        GameManager.SingleInstance.App.Controller.queueController.AddCallBack(e);
    }

    public void PostTestEvent()
    {
        EventAction e = new EventAction(assetName, UseReroll);
        GameManager.SingleInstance.App.Controller.queueController.AddCallBack(e);
    }

    public void AddBonus()
    {
        if (GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator.investigatorName == ownedInvestigator.investigatorName)
        {
            if (GameManager.SingleInstance.App.Model.testModel.activeTestStat == TestStat.Influence)
            {
                GameManager.SingleInstance.App.Model.testModel.AddBonus(1);
            }
        }
        GameManager.SingleInstance.App.Controller.queueController.NextCallBack();
    }

    public void UseReroll()
    {
        if (GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator.investigatorName == ownedInvestigator.investigatorName)
        {
            if (GameManager.SingleInstance.App.Model.testModel.activeTestStat == TestStat.Influence)
            {
                GameManager.SingleInstance.App.View.testView.AddUseableAsset(this, ItemUsed);
            }
        }
        GameManager.SingleInstance.App.Controller.queueController.NextCallBack();
    }

    public void ItemUsed()
    {
        GameManager.SingleInstance.App.Model.testModel.SetReroll();
    }

}
