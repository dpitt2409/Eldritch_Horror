using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MVC
{
    public void StartTest(TestStat stat, int testMod, TestType testType, TestCallBack callBack)
    {
        App.Model.testModel.StartTest(stat, testMod, testType, callBack);
        App.Controller.queueController.CreateCallBackQueue(FinishPreTestEvent); // Create Queue
        App.Model.eventModel.PreTestEvent(); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void FinishPreTestEvent()
    {
        App.Model.testModel.FinishPreTestEvent();
    }

    public void RollDice()
    {
        List<int> results = new List<int>();
        for (int i = 0; i < App.Model.testModel.currentNumRolls; i++)
        {
            int roll = Random.Range(0, 6);
            results.Add(roll + 1);
        }
        App.Model.testModel.DiceRolled(results);
        App.Controller.queueController.CreateCallBackQueue(FinishPostTestEvent); // Create Queue
        App.Model.eventModel.PostTestEvent(); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
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
        App.Model.investigatorModel.activeInvestigator.SpendFocusToken();
        App.View.testView.ItemUsed(go);
        App.Model.testModel.SetReroll();
    }

    public void UseClueToken(GameObject go)
    {
        App.Model.investigatorModel.activeInvestigator.SpendClue();
        App.View.testView.ItemUsed(go);
        App.Model.testModel.SetReroll();
    }

    public void UseAsset(GameObject go, UseableItemCallBack callBack)
    {
        App.View.testView.ItemUsed(go);
        callBack();
    }
}
