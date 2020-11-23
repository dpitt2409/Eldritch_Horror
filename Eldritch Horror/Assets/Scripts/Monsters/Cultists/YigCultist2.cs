using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YigCultist2 : Monster
{
    public YigCultist2()
    {
        monsterName = "Cultist";
        toughness = 2;
        horror = 2;
        damage = 3;
        monsterText = "If you lose Health from the strength test, you've been bitten by the snake-like creature; gain a Poisoned Condition.";
        reckoningText = "";

        tests = new TestStat[2];
        tests[0] = TestStat.Will;
        tests[1] = TestStat.Strength;

        testMods = new int[2];
        testMods[0] = 0;
        testMods[1] = -1;
    }

    public override void Defeated()
    {

    }

    public override void Spawned()
    {

    }
}
