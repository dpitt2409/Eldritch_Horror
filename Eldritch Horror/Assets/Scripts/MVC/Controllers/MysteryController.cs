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

    public void DrawFirstMystery()
    {
        App.Model.mysteryModel.DrawNewMystery();
    }

    public void CheckMystery()
    {
        Mystery m = App.Model.mysteryModel.activeMystery;
        if (App.Model.mysteryModel.mysteryProgress >= m.requirement)
        {
            if (App.Model.ancientOneModel.ancientOne.finalMysteryActive)
            {
                GameManager.SingleInstance.MysteryChecked();
            }
            else
            {
                // Mystery Solved
                App.Model.mysteryModel.MysterySolved();
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

    public void NewMysteryContinue()
    {
        App.View.mysteryView.NewMysteryContinue();
        if (App.Model.mysteryModel.mysteryProgress > 0) // New Mystery has not been drawn
        {
            if (App.Model.mysteryModel.mysteriesSolved == App.Model.ancientOneModel.ancientOne.numMysteries)
            {
                AncientOne ao = App.Model.ancientOneModel.ancientOne;
                if (ao.flipped)
                {
                    ao.AllMysteriesSolved();
                    GameManager.SingleInstance.MysteryChecked();
                }
                else
                {
                    App.Controller.endGameController.EndGame(true);
                    App.Model.ancientOneModel.ancientOne.LeavePlay();
                }
            }
            else
            {
                App.Model.mysteryModel.DrawNewMystery();
            }
        }
        else // New Mystery already drawn
        {
            GameManager.SingleInstance.MysteryChecked();
        }
    }
}
