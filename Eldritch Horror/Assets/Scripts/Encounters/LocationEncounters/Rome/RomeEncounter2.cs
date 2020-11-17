using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomeEncounter2 : Encounter
{
    public RomeEncounter2()
    {
        title = "Rome Location Encounter";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "You have an inspirational dream in which you are a proud Roman quaestor. Improve Will. \n\r Your reverie is interrupted by a band of small, primitive men running wild outside. You try to negotiate with this lost tribe of Miri Nigri. \n\r Test Influence \n\r If you fail, lose 1 Health and 1 Sanity as they continue their pursuit of some ancient grudge.";
        passText = "Pass";
        failText = "Fail \n\r Lose 1 Health and 1 Sanity";
    }
}
