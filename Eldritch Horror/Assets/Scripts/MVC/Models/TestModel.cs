using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TestCallBack(List<int> results);
public delegate void UseableItemCallBack();

public class TestModel : MVC
{
    public TestStat activeTestStat;
    public TestType activeTestType;
    public int activeTestMod;

    public TestCallBack currentCallBack;

    public int currentNumRolls;

    public List<int> currentResults;

    public bool rerolling = false;
    public bool rerolllAll = false;
    public List<int> reRolledDice;

    public int currentAdditionalDie = 0;
    public int currentBonus = 0;

    void Start()
    {
        currentCallBack = null;
        currentResults = new List<int>();
        reRolledDice = new List<int>();
    }

    public void StartTest(TestStat stat, int testMod, TestType testType, TestCallBack callBack)
    {
        activeTestStat = stat;
        activeTestType = testType;
        activeTestMod = testMod;
        currentCallBack = callBack;
        currentAdditionalDie = 0;
        currentBonus = 0;

        App.View.testView.PreTest();
    }

    public void FinishPreTestEvent()
    {
        Investigator active = App.Model.investigatorModel.activeInvestigator;
        currentNumRolls = active.CheckStat(activeTestStat) + active.CheckMod(activeTestStat);
        currentNumRolls += currentBonus;
        currentNumRolls += currentAdditionalDie;
        currentNumRolls += activeTestMod;
        if (currentNumRolls < 1)
            currentNumRolls = 1;

        App.View.testView.StartTest();
    }

    public void AddBonus(int bonus)
    {
        if (bonus > currentBonus)
            currentBonus = bonus;
    }

    public void AddAdditionalDie(int add)
    {
        currentAdditionalDie += add;
    }

    public void DiceRolled(List<int> results)
    {
        rerolling = false;
        rerolllAll = false;
        reRolledDice.Clear();

        currentResults = results;
        App.View.testView.DiceRolled();
    }

    public void SetReroll()
    {
        rerolling = true;
    }

    public void RerollDie(int index)
    {
        if (rerolling)
        {
            int newRoll = Random.Range(0, 6);
            currentResults[index] = newRoll + 1;
            App.View.testView.DiceUpdated();
            rerolling = false;
        }
        else if (rerolllAll)
        {
            if (!reRolledDice.Contains(index))
            {
                int newRoll = Random.Range(0, 6);
                currentResults[index] = newRoll + 1;
                reRolledDice.Add(index);
                App.View.testView.DiceUpdated();
            }
        }
    }

    public void Continue()
    {
        App.View.testView.Continue();
        currentCallBack(currentResults);
    }
}
