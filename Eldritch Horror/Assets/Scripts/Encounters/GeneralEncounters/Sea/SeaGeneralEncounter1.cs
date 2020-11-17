using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGeneralEncounter1 : Encounter
{
    public SeaGeneralEncounter1()
    {
        title = "Sea General Encounter";
        testStat = TestStat.Observation;
        testModifier = 0;
        encounterText = "You find the floating detritus of some sunken ship and search for any survivors or salvageable objects. \n\r Test Observation \n\r If you pass, you discover a floating trunk; gain 1 Artifact. \n\r If you fail, you waste hours without result; become Delayed.";
        passText = "Pass \n\r gain 1 Artifact";
        failText = "Fail \n\r Become Delayed";
    }
}
