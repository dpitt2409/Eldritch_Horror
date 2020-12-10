using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherWorldlyEncounter8 : ComplexEncounter
{
    public OtherWorldlyEncounter8()
    {
        title = "The Future";
        encounterTexts = new string[3];
        encounterTexts[0] = "You find yourself in a familiar city, but there's no electricity. The only light comes from the greenish moon, and the only sound is distant screaming. You can feel your reason being overrun by fear. \n\r Test Will";
        encounterTexts[1] = "Resisting the urge to panic, you find a large metal and glass machine that has electricty sparkling across its surface. You try to learn how to operate the device. \n\r Test Lore \n\r If you pass, you return to your own time; close this Gate. \n\r If you fail, you receive an electrical shock; lose 2 Health.";
        encounterTexts[2] = "Without thinking, you start walking with a long line of people. You hear screams from the people ahead of you. You try to escape, but the area is being guarded. \n\r Test Strength \n\r If you fail, you return home with no memory of what happened; gain an Amnesia Condition.";

        testStats = new TestStat[3];
        testStats[0] = TestStat.Will;
        testStats[1] = TestStat.Lore;
        testStats[2] = TestStat.Strength;

        testModifiers = new int[3];
        testModifiers[0] = 0;
        testModifiers[1] = 0;
        testModifiers[2] = 0;

        passTexts = new string[3];
        passTexts[0] = "";
        passTexts[1] = "Pass \n\r Close this Gate.";
        passTexts[2] = "Pass";

        failTexts = new string[3];
        failTexts[0] = "";
        failTexts[1] = "Fail \n\r Lose 2 Health";
        failTexts[2] = "Fail \n\r Gain an Amnesia Condition";
    }

    public override void FinishPhase2(bool passed)
    {
        if (GameManager.SingleInstance.App.Model.complexEncounterMenuModel.testIndex == 1)
        {
            if (passed)
            {
                // Fire 'Close this Gate' Event
                Location l = GameManager.SingleInstance.App.Model.complexEncounterMenuModel.currentInvestigator.currentLocation;
                GameManager.SingleInstance.App.Model.gateModel.SetClosingGate(l);

                GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(CloseThisGateEvent); // Create Queue
                GameManager.SingleInstance.App.Model.eventModel.OtherworldlyCloseGateEvent(l); // Populate Queue
                GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
            }
            else
            {
                // Lose 2 Health
                GameManager.SingleInstance.App.Controller.complexEncounterMenuController.CompleteEncounter();
            }
        }
        else
        {
            if (passed)
            {
                // Continue
                GameManager.SingleInstance.App.Controller.complexEncounterMenuController.CompleteEncounter();
            }
            else
            {
                // Gain an Amnesia Condition
                GameManager.SingleInstance.App.Controller.complexEncounterMenuController.CompleteEncounter();
            }
        }
    }

    public void CloseThisGateEvent()
    {
        // Close Gate
        Location l = GameManager.SingleInstance.App.Model.gateModel.currentClosingGate;
        if (l != null)
        {
            GameManager.SingleInstance.App.Controller.locationController.CloseGate(l);

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(GateClosed); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.CloseGateEvent(l); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.complexEncounterMenuController.CompleteEncounter();
        }
    }

    public void GateClosed()
    {
        GameManager.SingleInstance.App.Controller.complexEncounterMenuController.CompleteEncounter();
    }

}
