using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildernessGeneralEncounter14 : Encounter
{
    public WildernessGeneralEncounter14()
    {
        title = "Wilderness General Encounter";
        testStat = TestStat.Will;
        testModifier = 0;
        encounterText = "You notice a highly venomous spider crawling up your arm. You try to gently brush it away, but your fear threatens to break you calm. \n\r Test Will \n\r If you fail, you move too clumsily, and the spider bites you; Lose 1 Sanity and gain a Poisoned Condition.";
        passText = "Pass";
        failText = "Fail \n\r Lose 1 Sanity and gain a Poisoned Condition";
    }
}
