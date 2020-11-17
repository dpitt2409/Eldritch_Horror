using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareController : MVC
{
    public void StartPrepareAction()
    {
        App.View.prepareView.PrepareActionStarted();
    }

    public void PrepareTicket(bool ship)
    {
        App.View.prepareView.TicketPrepared();
        Investigator i = App.Model.investigatorModel.activeInvestigator;
        if (ship) // get ship
        {
            i.GetShipTicket();
        }
        else // get train
        {
            i.GetTrainTicket();
        }

        // Can Add Prepare Event Here
        i.ActionPerformed("" + BasicActions.Prepare);
        App.Controller.actionPhaseController.ActionPerformed(); 
    }

    public void CancelAction()
    {
        App.View.prepareView.PrepareActionCanceled();
        App.Controller.actionPhaseController.CancelAction();
    }
}
