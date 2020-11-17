using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LondonEncounter31 : Encounter
{
    public LondonEncounter31()
    {
        title = "London Location Encounter";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "You try to gain access to socialites who have fallen under the influence of Hastur. \n\r Test Influence \n\r If you pass, you gain their trust and learn aobut Hastur's avatars; gain 1 Clue. \n\r If you fail, you end up spending more money than you have; gain a Debt Condition.";
        passText = "Pass \n\r Gain 1 Clue";
        failText = "Fail \n\r Gain a Debt Condition.";
    }
}
