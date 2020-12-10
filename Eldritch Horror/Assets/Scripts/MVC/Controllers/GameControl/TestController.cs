using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MVC
{
    public void StartTest(TestStat stat, int testMod, TestType testType, Investigator i, TestCallBack callBack)
    {
        App.Model.testModel.StartTest(stat, testMod, testType, i, callBack);
        App.Controller.queueController.CreateCallBackQueue(FinishPreTestEvent); // Create Queue
        App.Model.eventModel.PreTestEvent(); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void StartSingleDieTest(Investigator i, TestCallBack callBack)
    {
        App.Model.testModel.StartSingleRollTest(i, callBack);
    }

    public void FinishPreTestEvent()
    {
        Investigator active = App.Model.testModel.testingInvestigator;
        if (active.deathEncounter != null) // The Testing Investigator died during the pretest
        {
            App.Model.testModel.Continue();
        }
        else
        {
            App.Model.testModel.FinishPreTestEvent();
        }
    }

    public void RollDice()
    {
        List<int> results = new List<int>();
        for (int i = 0; i < App.Model.testModel.currentNumRolls; i++)
        {
            int roll = Random.Range(0, 6);
            results.Add(roll + 1);
        }
        if (App.Model.testModel.activeTestType != TestType.SingleRoll)
        {
            App.Model.testModel.DiceRolled(results);
            App.Controller.queueController.CreateCallBackQueue(FinishPostTestEvent); // Create Queue
            App.Model.eventModel.PostTestEvent(); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            App.Model.testModel.SingleDieRolled(results);
        }
    }

    public void FinishPostTestEvent()
    {
        App.View.testView.PostTestEventComplete();
    }

    public void RerollDie(int index)
    {
        App.Model.testModel.RerollDie(index);
    }

    public void Continue()
    {
        App.Model.testModel.Continue();
    }

    public void MinimizeTest()
    {
        App.View.testView.MinimizeTest();
    }

    public void MaximizeTest()
    {
        App.View.testView.MaximizeTest();
    }

    public void UseFocusToken(GameObject go)
    {
        App.Model.testModel.testingInvestigator.SpendFocusToken();
        App.View.testView.ItemUsed(go);
        App.Model.testModel.SetReroll();
    }

    public void UseClueToken(GameObject go)
    {
        App.Model.testModel.testingInvestigator.SpendClue();
        App.View.testView.ItemUsed(go);
        App.Model.testModel.SetReroll();
    }

    public void UseAsset(GameObject go, UseableItemCallBack callBack)
    {
        App.View.testView.ItemUsed(go);
        callBack();
    }
}
