using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestController : MVC
{
    public void StartRestAction()
    {
        App.Controller.queueController.CreateCallBackQueue(FinishAction); // Create Queue
        App.Model.eventModel.RestEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }
    
    public void FinishAction()
    {
        Investigator i = App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex];
        i.GainHealth(App.Model.restModel.healthHeal);
        i.GainSanity(App.Model.restModel.sanityHeal);

        i.ActionPerformed("" + BasicActions.Rest);
        App.Controller.actionPhaseController.ActionPerformed();
    }
}
