using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythosEventController : MVC
{
    public void StartMythosEvent()
    {
        App.View.mythosEventView.MythosEventStarted();
        App.Model.mythosModel.activeMythos.EventEffect();
    }

    public void FinishMythosEvent()
    {
        App.View.mythosEventView.MythosEventFinished();
    }

    public void Continue()
    {
        App.View.mythosEventView.Continue();
        App.Controller.mythosController.NextMythosEffect();
    }
}
