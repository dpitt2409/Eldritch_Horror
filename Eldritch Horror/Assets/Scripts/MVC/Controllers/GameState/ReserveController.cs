using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReserveController : MVC
{
    public void EnableReserve()
    {
        App.Model.reserveModel.InitializeReserve();
    }

    public void ToggleReserveList()
    {
        App.View.reserveView.ToggleReserveList();
    }

    public void AssetPurchased(int index)
    {
        App.Model.reserveModel.BuyReserveAsset(index);
    }
}
