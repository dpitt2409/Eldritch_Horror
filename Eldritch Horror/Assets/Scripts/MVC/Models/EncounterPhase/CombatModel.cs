using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CombatCallBack();

public class CombatModel : MVC
{
    public bool testing = false;
    public int numSuccesses = 0;

    public List<Monster> normalMonstersToFight;
    public List<Monster> normalMonstersFought;
    public List<Monster> epicMonstersToFight;
    public List<Monster> epicMonstersFought;
    bool ambush = false;
    public CombatCallBack combatFinishedCallBack;
    public Investigator currentInvestigator;
    public bool fightingEpicMonsters = false;

    public List<Monster> currentMonsterOptions;
    public Monster currentMonster;

    public void CombatStarted(Investigator i, List<Monster> monsters, bool a, CombatCallBack callback)
    {
        currentInvestigator = i;
        normalMonstersFought = new List<Monster>();
        epicMonstersFought = new List<Monster>();
        ambush = a;
        combatFinishedCallBack = callback;
        fightingEpicMonsters = false;

        normalMonstersToFight = new List<Monster>();
        epicMonstersToFight = new List<Monster>();
        foreach (Monster m in monsters)
        {
            if (m.epic)
            {
                epicMonstersToFight.Add(m);
            }
            else
            {
                normalMonstersToFight.Add(m);
            }
        }
    }

    public void StartFight(Monster m)
    {
        currentMonster = m;
        App.View.combatView.StartFight(m);
    }

    public void TestStarted()
    {
        testing = true;
        numSuccesses = 0;
    }

    public void TestFinished(int num)
    {
        testing = false;
        numSuccesses = num;
    }

    public void FightFinished()
    {
        if (fightingEpicMonsters)
        {
            epicMonstersFought.Add(currentMonster);
        }
        else
        {
            normalMonstersFought.Add(currentMonster);
        }
        currentMonster = null;
        App.View.combatView.FightFinished();
    }

    public void FinishCombat()
    {
        normalMonstersToFight.Clear();
        normalMonstersFought.Clear();
        epicMonstersToFight.Clear();
        epicMonstersFought.Clear();
        currentInvestigator = null;
        numSuccesses = 0;
        combatFinishedCallBack = null;
    }

    public void SetMonsterOptions(List<Monster> options)
    {
        currentMonsterOptions = options;
    }

    public void FinishNormalMonsters()
    {
        fightingEpicMonsters = true;
    }
}
