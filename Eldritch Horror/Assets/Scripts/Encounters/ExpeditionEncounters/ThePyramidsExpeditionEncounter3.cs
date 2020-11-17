using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePyramidsExpeditionEncounter3 : ComplexEncounter
{
    public ThePyramidsExpeditionEncounter3()
    {
        title = "The Pyramids";
        encounterTexts = new string[3];
        encounterTexts[0] = "You reach for the ancient relic when a small white cat jumps in front of you. To your horror, the cat transforms into a demonic, feline creature and attacks \n\r Test Strength";
        encounterTexts[1] = "The demon shrivels down to a husk, leaving the relic unguarded. Gain 1 Artifact. Examining the item, you may be too distracted to notice the warning hieroglyph \n\r Test Observation -1 \n\r If you fail, you aren't prepared to evade the poisoned barb; lose 1 Health";
        encounterTexts[2] = "The creature leaves you badly wounded. Gain a Leg Injury Condition \n\r Test Influence \n\r If you pass, the men who come to find you distract the beast, allowing you to procure the relic; Gain 1 Artifact. \n\r If you fail, lose 1 Health as you have to walk without receiving medical attention.";

        testStats = new TestStat[3];
        testStats[0] = TestStat.Strength;
        testStats[1] = TestStat.Observation;
        testStats[2] = TestStat.Influence;

        testModifiers = new int[3];
        testModifiers[0] = 0;
        testModifiers[1] = -1;
        testModifiers[2] = 0;

        passTexts = new string[3];
        passTexts[0] = "";
        passTexts[1] = "Pass";
        passTexts[2] = "Pass \n\r Gain 1 Artifact";

        failTexts = new string[3];
        failTexts[0] = "";
        failTexts[1] = "Fail \n\r Lose 1 Health";
        failTexts[2] = "Fail \n\r Lose 2 Health";
    }
}
