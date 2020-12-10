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
        App.Model.travelModel.SetPath(c);
        Investigator i = App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex];
        // Hide the travel menu
        App.View.travelView.PathSelected();
        // Move the Investigator
        App.Controller.locationController.MoveInvestigator(i, c.destination);

        App.Controller.queueController.CreateCallBackQueue(TravelEventFinished); // Create Queue
        App.Model.eventModel.TravelEvent(c.destination, i); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void TravelEventFinished()
    {
        Investigator i = App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex];
        if (i.deathEncounter != null || i.delayed) // The Investigator died/became delayed during the travel event
        {
            i.ActionPerformed("" + BasicActions.Travel);
            App.Controller.actionPhaseController.ActionPerformed();
            return;
        }

        Connection c = App.Model.travelModel.currentPath;
        // Either open the ticket menu or end the action
        bool canUseShipTicket = false;
        bool canUseTrainTicket = false;
        if (c.type != ConnectionType.Uncharted)
        {
            foreach (Connection con in c.destination.connections)
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
            FinishAction();
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
        Investigator i = App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex];
        // Hide the ticket menu
        App.View.travelView.TicketUsed();
        // Spend the ticket
        // Spend Ticket Event ?
        if (c.type == ConnectionType.Ship)
            i.SpendShipTicket();
        if (c.type == ConnectionType.Train)
            i.SpendTrainTicket();

        // Move the Investigator
        App.Controller.locationController.MoveInvestigator(i, c.destination);

        App.Controller.queueController.CreateCallBackQueue(FinishAction); // Create Queue
        App.Model.eventModel.TravelEvent(c.destination, i); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void TicketNotUsed()
    {
        App.View.travelView.TicketUsed();
        FinishAction();
    }
    
    public void FinishAction()
    {
        App.Model.actionPhaseModel.investigators[App.Model.actionPhaseModel.turnIndex].ActionPerformed("" + BasicActions.Travel);
        App.Controller.actionPhaseController.ActionPerformed();
    }
}
