using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPhaseModel : MVC
{
    private int turnIndex = 0;
    public int actionsThisturn = 0;
    List<Investigator> investigators; // If an investigator dies during an encounter, the list order changes

    public bool terminateTurn = false; // Used for when Investigators die/become delayed during the action phase

    public void StartActionPhase()
    {
        investigators = new List<Investigator>(App.Model.investigatorModel.investigators);
        turnIndex = 0;
        actionsThisturn = 0;
        App.View.actionPhaseView.ActionTurnStarted();
    }

    public void ActionPerformed()
    {
        if (investigators[turnIndex].deathEncounter != null || investigators[turnIndex].delayed) // Investigator is dead or delayed
        {
            EndTurn();
            return;
        }

        actionsThisturn++;
        if (actionsThisturn == 2) // Current Investigator's Action turn is over
        {
            EndTurn();
        }
        else // Current Investigator needs to take another Action
        {
            App.View.actionPhaseView.ActionTurnStarted();
        }
    }

    public void EndTurn()
    {
        turnIndex++;
        if (turnIndex == investigators.Count) // All Investigators have done their Action turn
        {
            // action phase is done
            GameManager.SingleInstance.ActionPhaseComplete();
        }
        else // More Investigators need to take their Action turn
        {
            Investigator active = investigators[turnIndex];
            if (active.delayed)
            {
                active.StopBeingDelayed();
                EndTurn();
            }
            else if (active.deathEncounter != null)
            {
                EndTurn();
            }
            else
            {
                int newIndex = App.Model.investigatorModel.GetInvestigatorIndex(active.investigatorName);
                App.Controller.investigatorController.NewActiveInvestigator(newIndex);
                actionsThisturn = 0;
                
                App.View.actionPhaseView.ActionTurnStarted();
            }
        }
    }

    public void TurnToBeTerminated()
    {
        terminateTurn = true;
    }

    public void TerminateTurn()
    {
        terminateTurn = false;
        EndTurn();
    }
}
