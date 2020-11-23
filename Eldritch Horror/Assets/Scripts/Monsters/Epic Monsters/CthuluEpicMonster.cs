using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluEpicMonster : Monster
{
   public CthuluEpicMonster()
    {
        monsterName = "Cthulu";
        epic = true;
        toughness = 1;
        horror = 5;
        damage = 4;
        monsterText = "If you lose Sanity from the Will test, place the lost Sanity on the Ancient One sheet. \n\r Toughness is equal to the Number of Investigators + 3";
        reckoningText = "";
        monsterSprite = GameManager.SingleInstance.App.Model.spriteModel.cthuluSprite;

        tests = new TestStat[2];
        tests[0] = TestStat.Will;
        tests[1] = TestStat.Strength;

        testMods = new int[2];
        testMods[0] = -2;
        testMods[1] = -2;
    }

    public override void Spawned()
    {
        toughness = GameManager.SingleInstance.App.Model.investigatorModel.investigators.Count + 3;
    }

    public override void Defeated()
    {

    }

    public override void FinishTest1(int numSuccesses)
    {
        if (numSuccesses < horror)
        {
            int damage = horror - numSuccesses;
            // Can assume active Ancient One is Cthulu
            Cthulu boss = GameManager.SingleInstance.App.Model.ancientOneModel.ancientOne as Cthulu;
            boss.AddSanityToken(damage);
        }
        base.FinishTest1(numSuccesses);
    }
}
