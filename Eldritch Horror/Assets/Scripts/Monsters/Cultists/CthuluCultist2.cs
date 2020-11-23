using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluCultist2 : Monster
{
    public CthuluCultist2()
    {
        monsterName = "Cultist";
        toughness = 2;
        horror = 0;
        damage = 1;
        monsterText = "Before resolving the Strength test, lose 2 Sanity.";
        reckoningText = "";

        tests = new TestStat[2];
        tests[0] = TestStat.None;
        tests[1] = TestStat.Strength;

        testMods = new int[2];
        testMods[0] = 0;
        testMods[1] = 0;
    }

    public override void Defeated()
    {

    }

    public override void Spawned()
    {

    }
}
