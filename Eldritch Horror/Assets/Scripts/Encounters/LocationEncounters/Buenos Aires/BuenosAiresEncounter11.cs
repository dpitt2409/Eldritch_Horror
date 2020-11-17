using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuenosAiresEncounter11 : Encounter
{
    public BuenosAiresEncounter11()
    {
        title = "Buenos Aires Location Encounter";
        testStat = TestStat.Observation;
        testModifier = 0;
        encounterText = "A well-dressed man is browsing for antique books. Something about him seems strange. \n\r Test Observation \n\r If you pass, you see that he's one of the serpent people in disguise; lose 1 Sanity and gain 1 Clue. \n\r If you fail, his identity remains a mystery; lose 1 Sanity and gain a Paranoia Condition.";
        passText = "Pass \n\r Lose 1 Sanity and Gain 1 Clue";
        failText = "Fail \n\r Lose 1 Sanity and gain a Paranoia Condition";
    }
}
