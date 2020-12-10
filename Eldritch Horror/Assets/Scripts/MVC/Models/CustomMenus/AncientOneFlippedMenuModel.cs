using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AOFlipCallBack();

public class AncientOneFlippedMenuModel : MVC
{

    public AOFlipCallBack aoCallBack;

    public void AncientOneFlipped(AOFlipCallBack callback)
    {
        aoCallBack = callback;
        App.View.ancientOneFlippedMenuView.Flipped();
    }
}
