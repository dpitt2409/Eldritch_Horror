using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneralEncounter25 : Encounter
{
    public CityGeneralEncounter25()
    {
        title = "City General Encounter";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "A local politician blames you for several murders and kidnappings. People have been convinced to find and punish you. You try to reason with them. \n\r Test Influence \n\r If you fail, they beat you mercilessly; gain 1 Injury Condition and impair Strength.";
        passText = "Pass";
        failText = "Fail \n\r Gain an Injury Condition and Impair Strength";
    }

}
