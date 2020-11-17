using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelController : MVC
{
    public void StartTravelAction()
    {
        App.View.travelView.TravelActionStarted();
    }

    public void TravelByPath(Connection c)
    {
        Investigator i = App.Model.investigatorModel.activeInvestigator;
        // Hide the travel menu
        App.View.travelView.PathSelected();
        // Move the Investigator
        App.Controller.locationController.MoveInvestigator(i, c.destination); // technically should fire travel event here?
        // Either open the ticket menu or end the action
        bool canUseShipTicket = false;
        bool canUseTrainTicket = false;
        if (c.type != ConnectionType.Uncharted)
        {
            foreach(Connection con in c.destination.connections)
            {
                if (con.type == ConnectionType.Ship && i.shipTickets > 0)
                    canUseShipTicket = true;
                if (con.type == ConnectionType.Train && i.trainTickets > 0)
                    canUseTrainTicket = true;
            }
        }
        if (canUseShipTicket || canUseTrainTicket)
        {
            OpenUseTicketMenu();
        }
        else
        {
            App.Controller.queueController.CreateCallBackQueue(FinishAction); // Create Queue
            App.Model.eventModel.TravelEvent(); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }        
    }

    public void CancelAction()
    {
        App.View.travelView.TravelActionCanceled();
        App.Controller.actionPhaseController.CancelAction();
    }

    public void OpenUseTicketMenu()
    {
        App.View.travelView.OpenTicketMenu();
    }

    public void UseTicket(Connection c)
    {
        Investigator i = App.Model.investigatorModel.activeInvestigator;
        // Hide the ticket menu
        App.View.travelView.TicketUsed();
        // Move the Investigator
        App.Controller.locationController.MoveInvestigator(i, c.destination);
        // Spend the ticket
        // Spend Ticket Event ?
        if (c.type == ConnectionType.Ship)
            i.SpendShipTicket();
        if (c.type == ConnectionType.Train)
            i.SpendTrainTicket();

        App.Controller.queueController.CreateCallBackQueue(FinishAction); // Create Queue
        App.Model.eventModel.TravelEvent(); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void TicketNotUsed()
    {
        App.View.travelView.TicketUsed();
        App.Controller.queueController.CreateCallBackQueue(FinishAction); // Create Queue
        App.Model.eventModel.TravelEvent(); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }
    
    public void FinishAction()
    {
        App.Model.investigatorModel.activeInvestigator.ActionPerformed("" + BasicActions.Travel);
        App.Controller.actionPhaseController.ActionPerformed();
    }
}
