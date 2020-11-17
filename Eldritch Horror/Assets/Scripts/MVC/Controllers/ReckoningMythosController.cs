using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReckoningMythosController : MVC
{
    public void StartReckoning()
    {
        App.Model.reckoningMythosModel.StartReckoning();
        App.Model.eventModel.ReckoningEvent();
        App.Model.reckoningMythosModel.SortReckonings();
    }

    public void Next()
    {
        App.View.ReckoningMythosView.ReckoningFinished();
        NextReckoning();
    }

    public void NextReckoning()
    {
        if (App.Model.reckoningMythosModel.currentEvents.Count != 0)
        {
            SelectNextReckoning();
        }
        else
        {
            List<ReckoningEvent>[] all = App.Model.reckoningMythosModel.events;
            while (App.Model.reckoningMythosModel.activeList < all.Length && App.Model.reckoningMythosModel.currentEvents.Count == 0)
            {
                App.Model.reckoningMythosModel.NextReckoningList();
            }

            if (App.Model.reckoningMythosModel.currentEvents.Count != 0)
            {
                SelectNextReckoning();
            }
            else
            {
                // Reckoning is over
                App.Controller.mythosController.NextMythosEffect();
            }
        }
    }

    public void SelectNextReckoning()
    {
        List<ReckoningEvent> list = App.Model.reckoningMythosModel.currentEvents;
        if (list.Count == 0)
        {
            ReckoningSelected(0);
        }
        else
        {
            // Open up options menu, point to ReckoningSelected with given index
            ReckoningSelected(0);
        }

    }

    public void ReckoningSelected(int index)
    {
        ReckoningEvent re = App.Model.reckoningMythosModel.currentEvents[index];
        App.Model.reckoningMythosModel.StartReckoningEvent(index);
        re.callBack();
        // Reckonings must call App.Model.reckoningMythosModel.AddReckoningEvent() in response to the ReckoningEvent
        // Reckonings call FinishReckoning when they're done
    }

    public void FinishReckoning() // Called externally from a finished reckoning event
    {
        App.Model.reckoningMythosModel.FinishReckoning(); // Removes current event from list and enables next button
    }

    public void EnableCurrentReckoningNextButton(ReckoningCallBack callback)
    {
        App.Model.reckoningMythosModel.SetCurrentReckoningNextButtonCallBack(callback);
    }

    public void CurrentReckoningNextButton()
    {
        ReckoningCallBack callback = App.Model.reckoningMythosModel.currentReckoningNextButtonCallBack;
        App.Model.reckoningMythosModel.CurrentReckoningNextButton();
        callback();
    }
}
