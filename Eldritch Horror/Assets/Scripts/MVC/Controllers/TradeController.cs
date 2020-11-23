using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MVC
{
    public void StartTradeAction()
    {
        App.View.tradeView.TradeActionStarted();
    }

    public void InvestigatorSelected(int index)
    {
        Investigator active = App.Model.investigatorModel.activeInvestigator;
        List<Investigator> investigators = new List<Investigator>();
        foreach (Investigator i in active.currentLocation.investigatorsOnLocation)
        {
            if (i.investigatorName != active.investigatorName)
            {
                investigators.Add(i);
            }
        }
        Investigator partner = null;
        for (int i = 0; i < investigators.Count; i++)
        {
            if (i == index)
            {
                partner = investigators[i];
            }
        }
        if (partner == null)
        {
            Debug.LogError("Error Selecting Trade Partner");
        }
        else
        {
            BeginTrade(partner);
        }
    }

    public void BeginTrade(Investigator i)
    {
        App.Model.tradeModel.BeginTrade(i);
    }

    public void CancelAction()
    {
        App.View.tradeView.TradeActionCanceled();
        App.Controller.actionPhaseController.CancelAction();
    }

    public void DoneTrading()
    {
        App.Model.tradeModel.DoneTrading(); // Trade Event? 

        App.Model.investigatorModel.activeInvestigator.ActionPerformed("" + BasicActions.Trade);
        App.Controller.actionPhaseController.ActionPerformed();
    }

    public void TradeShipTicket(Investigator previousOwner)
    {
        Investigator i1 = App.Model.tradeModel.investigator1;
        Investigator i2 = App.Model.tradeModel.investigator2;

        if (previousOwner.investigatorName == i1.investigatorName)
        {
            // investigator2 gains ticket
            i2.TradeShipTicket(true);
            // If now investigator2 has too many tickets, they give 1 back
            if (i2.shipTickets + i2.trainTickets > 2)
            {
                if (i2.shipTickets > 2) // Now have 3 Ship tickets, give 1 back
                {
                    i2.TradeShipTicket(false);
                    i1.TradeShipTicket(true);
                }
                else // Give back a Train Ticket
                {
                    i2.TradeTrainTicket(false);
                    i1.TradeTrainTicket(true);
                }
            }
        }
        else
        {
            // investigator1 gains ticket
            i1.TradeShipTicket(true);
            // If now investigator1 has too many tickets, they give 1 back
            if (i1.shipTickets + i1.trainTickets > 2)
            {
                if (i1.shipTickets > 2) // Now have 3 Ship tickets, give 1 back
                {
                    i1.TradeShipTicket(false);
                    i2.TradeShipTicket(true);
                }
                else // Give back a Train Ticket
                {
                    i1.TradeTrainTicket(false);
                    i2.TradeTrainTicket(true);
                }
            }
        }
        previousOwner.TradeShipTicket(false);

        App.View.tradeView.TradeMade();
    }

    public void TradeTrainTicket(Investigator previousOwner)
    {
        Investigator i1 = App.Model.tradeModel.investigator1;
        Investigator i2 = App.Model.tradeModel.investigator2;

        if (previousOwner.investigatorName == i1.investigatorName)
        {
            // investigator2 gains ticket
            i2.TradeTrainTicket(true);
            // If now investigator2 has too many tickets, they give 1 back
            if (i2.shipTickets + i2.trainTickets > 2)
            {
                if (i2.trainTickets > 2) // Now have 3 Train tickets, give 1 back
                {
                    i2.TradeTrainTicket(false);
                    i1.TradeTrainTicket(true);
                }
                else // Give back a Ship Ticket
                {
                    i2.TradeShipTicket(false);
                    i1.TradeShipTicket(true);
                }
            }
        }
        else
        {
            // investigator1 gains ticket
            i1.TradeTrainTicket(true);
            // If now investigator1 has too many tickets, they give 1 back
            if (i1.shipTickets + i1.trainTickets > 2)
            {
                if (i1.trainTickets > 2) // Now have 3 Train tickets, give 1 back
                {
                    i1.TradeTrainTicket(false);
                    i2.TradeTrainTicket(true);
                }
                else // Give back a Ship Ticket
                {
                    i1.TradeShipTicket(false);
                    i2.TradeShipTicket(true);
                }
            }
        }
        previousOwner.TradeTrainTicket(false);

        App.View.tradeView.TradeMade();
    }

    public void TradeFocusToken(Investigator previousOwner)
    {
        Investigator i1 = App.Model.tradeModel.investigator1;
        Investigator i2 = App.Model.tradeModel.investigator2;

        if (previousOwner.investigatorName == i1.investigatorName)
        {
            // investigator2 gains ticket
            if (i2.focusTokens == 2)
                return;

            i2.TradeFocusTokens(true);
        }
        else
        {
            if (i1.focusTokens == 2)
                return;

            // investigator1 gains ticket
            i1.TradeFocusTokens(true);
        }
        previousOwner.TradeFocusTokens(false);

        App.View.tradeView.TradeMade();
    }

    public void TradeClue(Clue c, Investigator previousOwner)
    {
        Investigator i1 = App.Model.tradeModel.investigator1;
        Investigator i2 = App.Model.tradeModel.investigator2;

        if (previousOwner.investigatorName == i1.investigatorName)
        {
            i1.TradeClue(c, false);
            i2.TradeClue(c, true);
        }
        else
        {
            i2.TradeClue(c, false);
            i1.TradeClue(c, true);
        }
        App.View.tradeView.TradeMade();
    }

    public void TradeAsset(Asset a, Investigator previousOwner)
    {
        Investigator i1 = App.Model.tradeModel.investigator1;
        Investigator i2 = App.Model.tradeModel.investigator2;

        if (previousOwner.investigatorName == i1.investigatorName)
        {
            // i2 gains asset
            i1.TradeAsset(a, false);
            i2.TradeAsset(a, true);
        }
        else
        {
            // i1 gains asset
            i2.TradeAsset(a, false);
            i1.TradeAsset(a, true);
        }
        App.View.tradeView.TradeMade();
    }

    public void TradeSpell(Spell s, Investigator previousOwner)
    {
        Investigator i1 = App.Model.tradeModel.investigator1;
        Investigator i2 = App.Model.tradeModel.investigator2;

        if (previousOwner.investigatorName == i1.investigatorName)
        {
            // i2 gains spell
            i1.TradeSpell(s, false);
            i2.TradeSpell(s, true);
        }
        else
        {
            // i1 gains spell
            i2.TradeSpell(s, false);
            i1.TradeSpell(s, true);
        }
        App.View.tradeView.TradeMade();
    }
}
