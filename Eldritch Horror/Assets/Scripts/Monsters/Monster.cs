using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster
{
    public string monsterName;

    public Sprite monsterSprite;

    public int toughness;

    public int horror;

    public int damage;

    public string monsterText;

    public string reckoningText;

    public TestStat[] tests;

    public int[] testMods;

    public int damageTaken = 0;

    public bool epic = false;

    public virtual void StartCombat() { GameManager.SingleInstance.App.Controller.combatController.StartTest1(); }

    public virtual void FinishTest1(int numSuccesses) { GameManager.SingleInstance.App.Controller.combatController.StartTest2(); }

    public virtual void FinishTest2(int numSuccesses) { GameManager.SingleInstance.App.Controller.combatController.FinishFight(); }
}
