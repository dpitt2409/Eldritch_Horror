using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanFranciscoEncounter16 : Encounter
{
    public SanFranciscoEncounter16()
    {
        title = "San Francisco Location Encounter";
        testStat = TestStat.Strength;
        testModifier = 0;
        encounterText = "As a stranger bumps into you, a small needle pierces your skin, infecting you with the black fever. Dr. Miller oversees your recovery. \n\r Test Strength \n\r If you pass, you recover, and Miller teaches you to be alert to such tactics in the future; improve Observation. \n\r If you fail, the fever stays with you; gain a Poisoned Condition and a Paranoia Condition.";
        passText = "Pass \n\r Improve Observation";
        failText = "Fail \n\r Gain a Poisoned Condition and a Paranoia Condition";
    }
}
