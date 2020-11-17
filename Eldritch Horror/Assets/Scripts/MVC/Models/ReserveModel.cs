using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReserveModel : MVC
{
    public List<Asset> reserveAssets;

    void Start()
    {
        reserveAssets = new List<Asset>();
    }

    public void InitializeReserve()
    {
        for (int i = 0; i < 4; i++)
        {
            Asset a = App.Model.assetModel.DrawRandomAsset();
            reserveAssets.Add(a);
        }
        App.View.reserveView.EnableReserve();
        App.View.reserveView.ReserveUpdated();
    }

    public void BuyReserveAsset(int index)
    {
        reserveAssets[index] = App.Model.assetModel.DrawRandomAsset();
        App.View.reserveView.ReserveUpdated();
    }
}
