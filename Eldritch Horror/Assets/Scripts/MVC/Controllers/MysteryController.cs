using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryController : MVC
{
    public void EnableMysteryUI()
    {
        App.Model.mysteryModel.SetupMystery();
    }

    public void ToggleMysteryInfo()
    {
        App.View.mysteryView.ToggleMysteryInfo();
    }

    public void CheckMystery()
    {
        Mystery m = App.Model.mysteryModel.activeMystery;
        if (App.Model.mysteryModel.mysteryProgress >= m.requirement)
        {
            // Mystery Solved
            App.Model.mysteryModel.MysterySolved();
            if (App.Model.mysteryModel.mysteriesSolved == App.Model.ancientOneModel.ancientOne.numMysteries)
            {
                Debug.Log("Investigators win the fucking game");
            }
            else
            {
                // Draw new Mystery
                App.Model.mysteryModel.DrawNewMystery();
                GameManager.SingleInstance.MysteryChecked();
            }
        }
        else
        {
            // Mystery Not Solved
            GameManager.SingleInstance.MysteryChecked();
        }
    }

    public void AdvanceMystery()
    {
        App.Model.mysteryModel.AdvanceMystery();
    }
}
