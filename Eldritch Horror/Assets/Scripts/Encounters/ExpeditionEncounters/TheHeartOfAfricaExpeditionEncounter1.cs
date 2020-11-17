using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeartOfAfricaExpeditionEncounter1 : ComplexEncounter
{
    public TheHeartOfAfricaExpeditionEncounter1()
    {
        title = "The Heart of Africa";
        encounterTexts = new string[3];
        encounterTexts[0] = "The whole jungle shakes, and the ground splits beneath your feet. You fall through the crevice into a vast subterranean tunnel. \n\r Test Strength";
        encounterTexts[1] = "You follow the tunnel to the ancient city of G'harne. Retreat Doom by 1. If you have read about G'harne, you know to leave quickly \n\r Test Lore \n\r If you fail, you wake up in the jungle with no memory; gain an Amnesia Condition.";
        encounterTexts[2] = "You land painfully on your spine. Lose 1 Health and gain a Back Injury Condition. While you're stuck here, you examine this tunnel \n\r Test Observation -1 \n\r If you pass, you see that it was dug out by a large creature; retreat Doom by 1.";

        testStats = new TestStat[3];
        testStats[0] = TestStat.Strength;
        testStats[1] = TestStat.Lore;
        testStats[2] = TestStat.Observation;

        testModifiers = new int[3];
        testModifiers[0] = 0;
        testModifiers[1] = 0;
        testModifiers[2] = -1;

        passTexts = new string[3];
        passTexts[0] = "";
        passTexts[1] = "Pass";
        passTexts[2] = "Pass \n\r Retreat Doom by 1";

        failTexts = new string[3];
        failTexts[0] = "";
        failTexts[1] = "Fail \n\r Gain an Amnesia Condition";
        failTexts[2] = "Fail";
    }
}
