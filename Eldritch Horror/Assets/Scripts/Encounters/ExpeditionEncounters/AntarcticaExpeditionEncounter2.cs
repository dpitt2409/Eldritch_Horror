using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntarcticaExpeditionEncounter2 : ComplexEncounter
{
    public AntarcticaExpeditionEncounter2()
    {
        title = "Antarctica";
        encounterTexts = new string[3];
        encounterTexts[0] = "You hear a faint sound echoing up from the caves that lead down into the darkness. You listen carefully to discern the sound's origin. \n\r Test Observation";
        encounterTexts[1] = "You identify the sound of shoggoths. You escape, but can barely keep yourself conscious waiting for a rescue. \n\r Test Will \n\r If you pass, you spot something on the cave floor; gain 1 Artifact. \n\r If you fail, you pass out and hear the sounds of shoggoth everywhere you go; gain a Hallucinations Condition";
        encounterTexts[2] = "'Tekeli-li! Tekeli-li!' You recognize it too late. The shoggoths overhwelm you! \n\r Test Strength \n\r If you pass, you overcome the threat; gain 2 Clues. If you fail, you escape by jumping off a ledge; gain a Leg Injury Condition.";

        testStats = new TestStat[3];
        testStats[0] = TestStat.Observation;
        testStats[1] = TestStat.Will;
        testStats[2] = TestStat.Strength;

        testModifiers = new int[3];
        testModifiers[0] = 0;
        testModifiers[1] = 0;
        testModifiers[2] = -1;

        passTexts = new string[3];
        passTexts[0] = "";
        passTexts[1] = "Pass \n\r Gain 1 Artifact";
        passTexts[2] = "Pass \n\r Gain 2 Clues";

        failTexts = new string[3];
        failTexts[0] = "";
        failTexts[1] = "Fail \n\r Gain a Hallucinations Condition";
        failTexts[2] = "Fail \n\r Gain a Leg Injury Condition";
    }
}
