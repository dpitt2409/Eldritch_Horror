using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPhaseController : MVC
{
    public void StartActionPhase()
    {
        App.Model.actionPhaseModel.StartActionPhase();
    }

    public void CancelAction()
    {
        App.View.actionPhaseView.ActionCanceled();
    }

    public void AddActionToList(string text, ActionCallBack callback)
    {
        if (!App.Model.investigatorModel.activeInvestigator.actionsTakenThisTurn.Contains(text))
        {
            App.View.actionPhaseView.ActionAddedToList(text, callback);
        }
    }

    public void PerformBasicAction(BasicActions action)
    {
        App.View.actionPhaseView.ActionSelected();
        App.Model.investigatorModel.activeInvestigator.PerformBasicAction(action);
    }

    public void PerformCustomAction(ActionCallBack callback)
    {
        App.View.actionPhaseView.ActionSelected();
        callback();
    }

    public void ActionPerformed()
    {
        App.Model.actionPhaseModel.ActionPerformed();
    }

    public void EndTurn()
    {
        App.View.actionPhaseView.ActionSelected();
        App.Model.actionPhaseModel.EndTurn();
    }

    public void TerminateTurn() // Used when Investigators die/become delayed during the action phase
    {
        App.Model.actionPhaseModel.TerminateTurn();
    }
}
