using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkhamEncounter11 : Encounter
{
    public ArkhamEncounter11()
    {
        title = "Arkham Location Encounter";
        testStat = TestStat.Lore;
        testModifier = 0;
        encounterText = "One of the stained glass windows in the South Church features an angel reading strange runes from a scroll. The runes look familiar to you. \n\r Test Lore \n\r If you pass, gain 1 Incantation Spell. \n\r If you fail, you cannot interpret the runes, but the angel's face has a more inhuman aspect now; lose 2 Sanity.";
        passText = "Pass";
        failText = "Fail \n\r Lose 2 Sanity";
    }
}
