using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquireModel : MVC
{
    public bool testing = false;
    public bool buying = false;
    public bool discarding = false;
    public int remainingPoints = 0;

    public void StartAcquireAction()
    {
        buying = false;
        discarding = false;
        testing = false;
        remainingPoints = 0;
        App.View.acquireView.AcquireActionStarted();
    }

    public void StartInfluenceTest()
    {
        testing = true;
        App.View.acquireView.StartInfluenceTest();
    }

    public void InfluenceTested(int points)
    {
        testing = false;
        buying = true;
        remainingPoints = points;
        App.View.acquireView.InfluenceTested();
    }

    public void Discarding()
    {
        discarding = true;
        buying = false;
        App.View.acquireView.Discarding();
    }

    public void DiscardAsset()
    {
        remainingPoints = 0;
        discarding = false;
        App.View.acquireView.UpdateAssets();
    }

    public void BuyAsset(int cost)
    {
        remainingPoints -= cost;
        App.View.acquireView.HasBought();
    }

    public void DebtTaken()
    {
        remainingPoints += 2;
        App.View.acquireView.DebtTaken();
    }
}
