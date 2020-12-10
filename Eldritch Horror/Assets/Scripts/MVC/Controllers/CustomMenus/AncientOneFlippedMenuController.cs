using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientOneFlippedMenuController : MVC
{
    public void AncientOneFlipped(AOFlipCallBack callback)
    {
        App.Controller.openMenuController.HideOpenMenu();
        App.Model.ancientOneFlippedMenuModel.AncientOneFlipped(callback);
    }

    public void Continue()
    {
        App.View.ancientOneFlippedMenuView.Continue();
        App.Controller.openMenuController.EnableOpenMenu();
        App.Model.ancientOneFlippedMenuModel.aoCallBack();
    }
}
