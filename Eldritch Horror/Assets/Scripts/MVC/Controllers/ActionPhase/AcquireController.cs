﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquireController : MVC
{
    public void StartAcquireAction()
    {
        App.Model.acquireModel.StartAcquireAction();
    }

    public void StartInfluenceTest()
    {
        Investigator active = App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex];
        App.Model.acquireModel.StartInfluenceTest();
        App.Controller.testController.StartTest(TestStat.Influence, 0, TestType.Test, active, InfluenceTested);
    }

    public void InfluenceTested(List<int> results)
    {
        Investigator active = App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex];
        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= active.SUCCESS)
            {
                numSuccesses++;
            }
        }
        App.Model.acquireModel.InfluenceTested(numSuccesses);
    }

    public void BuyAsset(int index)
    {
        if (App.Model.acquireModel.discarding)
        {
            App.Controller.reserveController.AssetPurchased(index);
            App.Model.acquireModel.DiscardAsset();
        }
        if (App.Model.acquireModel.buying)
        {
            Asset a = App.Model.reserveModel.reserveAssets[index];
            if (a != null && App.Model.acquireModel.remainingPoints >= a.cost)
            {
                App.Controller.reserveController.AssetPurchased(index);
                App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex].GainAsset(a);
                App.Model.acquireModel.BuyAsset(a.cost);
            }
            App.View.acquireView.UpdateAssets();
        }
    }

    public void TakeDebt()
    {
        Condition debt = App.Model.conditionModel.DrawConditionByName("Debt");
        App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex].GainCondition(debt);
        App.Model.acquireModel.DebtTaken();
    }

    public void SetDiscarding()
    {
        App.Model.acquireModel.Discarding();
    }

    public void Continue()
    {
        App.View.acquireView.Done();
        App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex].ActionPerformed("" + BasicActions.Acquire);
        App.Controller.actionPhaseController.ActionPerformed();
    }

    public void CancelAcquireAction()
    {
        App.View.acquireView.Done();
        App.Controller.actionPhaseController.CancelAction();
    }
}
