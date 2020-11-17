using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulMonster : Monster
{
    public GhoulMonster()
    {
        monsterName = "Ghoul";
        toughness = 1;
        horror = 1;
        damage = 2;
        monsterText = "If you take damage from the Strength test, gain a Paranoia Condition";
        reckoningText = "";
        monsterSprite = GameManager.SingleInstance.App.Model.spriteModel.ghoulSprite;

        tests = new TestStat[2];
        tests[0] = TestStat.Will;
        tests[1] = TestStat.Strength;

        testMods = new int[2];
        testMods[0] = 1;
        testMods[1] = 0;
    }
}
