using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SydneyEncounter9 : Encounter
{
    public SydneyEncounter9()
    {
        title = "Sydney Location Encounter";
        testStat = TestStat.Will;
        testModifier = 0;
        encounterText = "Charles Hopkins hires you to restore the suburb of Bungarribee. At night, you feel hands grab your throat! \n\r Test Will \n\r If you pass, you return to the work in the morning and grow stronger; improve Strength. \n\r If you fail, you run away in terror; lose 1 Sanity and gain a Hallucinations Condition.";
        passText = "Pass \n\r Improve Strength";
        failText = "Fail \n\r Lose 1 Sanity and gain a Hallucinations Condition.";
    }
}
