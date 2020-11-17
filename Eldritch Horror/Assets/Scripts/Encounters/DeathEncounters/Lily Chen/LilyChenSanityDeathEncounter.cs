using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class LilyChenSanityDeathEncounter : Encounter
{
    public LilyChenSanityDeathEncounter()
    {
        title = "In case of Insanity or other Psychosis.";
        testStat = TestStat.Influence;
        testModifier = 0;
        encounterText = "You barely recognize the small woman huddled in an alleyway, her few belongings scattered next to her. Gain all of her posssessions. 'It wasn't true,' Lily sobs. 'I have no no destiny. I cannot fight the darkness.' You reassure her that she has already done her part and that others will finish the task. \n\r Test Influence \n\r If you pass, she chants over you, passing her destiny on to you; retreat Doom by 1. If you fail, Lily disappears into the darkness.";
        passText = "Pass \n\r Retreat Doom by 1";
        failText = "Fail";
    }

    public override void FinishEncounter(bool passed)
    {
        if (passed)
        {
            // Retreat doom by 1
            GameManager.SingleInstance.App.Model.doomModel.RetreatDoom(1);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DoomRetreated); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.DoomAdvanced(); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
        }
    }

    public void DoomRetreated()
    {
        GameManager.SingleInstance.App.Controller.encounterMenuController.CompleteEncounter(); // End Encounter
    }
}
*/