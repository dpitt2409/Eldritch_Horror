using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokyoEncounter10 : Encounter
{
    public TokyoEncounter10()
    {
        title = "Tokyo Location Encounter";
        testStat = TestStat.Will;
        testModifier = 0;
        encounterText = "Your hotel room is haunted by a yurei. You are terrified as the white-clad woman howls and passes through you. \n\r Test Will \n\r If you fail, her lingering presence is a blight on your soul; Lose 1 Sanity and gain a Cursed Condition.";
        passText = "Pass";
        failText = "Fail \n\r Lose 1 Sanity and gain a Cursed Condition.";
    }
}
