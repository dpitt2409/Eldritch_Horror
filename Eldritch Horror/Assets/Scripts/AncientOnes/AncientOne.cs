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

    public bool flipped = false;

    public bool finalMysteryActive = false;

    public abstract Dictionary<LocationType, List<Encounter>> CreateResearchDeck();

    public abstract void AllMysteriesSolved();

    public virtual void Flipped() { return; }

    public virtual void EnterPlay()
    {
        GameManager.SingleInstance.App.Model.eventModel.doomAdvancedEvent += AODoomAdvancedEvent;
    }

    public virtual void LeavePlay()
    {
        GameManager.SingleInstance.App.Model.eventModel.doomAdvancedEvent -= AODoomAdvancedEvent;
    }

    public void AODoomAdvancedEvent(int num)
    {
        if (!flipped)
        {
            if (GameManager.SingleInstance.App.Model.doomModel.currentDoom == 0)
            {
                EventAction e = new EventAction(EventType.Mandatory, AODoomAdvancedEventCallBack);
                GameManager.SingleInstance.App.Controller.queueController.AddCallBack(e);
            }
        }
    }

    public void AODoomAdvancedEventCallBack()
    {
        flipped = true;
        GameManager.SingleInstance.App.Controller.locationController.AncientOneFlipped();
        Flipped();

        GameManager.SingleInstance.App.Controller.ancientOneFlippedMenuController.AncientOneFlipped(GameManager.SingleInstance.App.Controller.queueController.NextCallBack);
    }
}
