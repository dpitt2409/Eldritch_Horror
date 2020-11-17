using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceOmenMythosController : MVC
{
    public void StartAdvanceOmen()
    {
        App.Model.omenModel.AdvanceOmen();

        GateColor newColor = App.Model.advanceOmenMythosModel.GetOmenColor(App.Model.omenModel.currentOmen);
        int matchingGates = 0;
        foreach (Location l in App.Model.gateModel.activeGates)
        {
            if (l.gate == newColor)
            {
                matchingGates++;
            }
        }
        App.Model.advanceOmenMythosModel.StartAdvanceOmen(matchingGates);
        
        if (matchingGates == 0)
        {
            App.View.advanceOmenMythosView.Done();
        }
        else
        {
            // Advance Doom by 1
            App.Model.doomModel.AdvanceDoom(matchingGates);
            App.Controller.queueController.CreateCallBackQueue(DoomAdvanced); // Create Queue
            App.Model.eventModel.DoomAdvanced(); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void DoomAdvanced()
    {
        App.View.advanceOmenMythosView.Done();
    }

    public void FinishAdvanceOmen()
    {
        App.View.advanceOmenMythosView.FinishEvent();
        App.Controller.mythosController.NextMythosEffect();
    }
}
