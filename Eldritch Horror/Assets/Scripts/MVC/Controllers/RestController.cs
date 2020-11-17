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
        Investigator i = App.Model.investigatorModel.activeInvestigator;
        i.GainHealth(App.Model.restModel.healthHeal);
        i.GainSanity(App.Model.restModel.sanityHeal);

        App.Model.investigatorModel.activeInvestigator.ActionPerformed("" + BasicActions.Rest);
        App.Controller.actionPhaseController.ActionPerformed();
    }
}
