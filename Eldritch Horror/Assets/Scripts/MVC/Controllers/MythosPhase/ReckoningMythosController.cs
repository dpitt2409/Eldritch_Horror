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

    public void SetReckoningText(string text)
    {
        App.View.ReckoningMythosView.SetReckoningText(text);
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
                App.Controller.investigatorController.NewActiveInvestigator(0);// Set Lead Investigator to active
                App.Controller.mythosController.NextMythosEffect();
            }
        }
    }

    public void SelectNextReckoning()
    {
        List<MultipleOptionMenuObject> options = new List<MultipleOptionMenuObject>();
        foreach (ReckoningEvent re in App.Model.reckoningMythosModel.currentEvents)
        {
            if (!(re.source == ReckoningSource.Investigator && re.investigator.deathEncounter != null))
            {
                options.Add(new MultipleOptionMenuObject(MultipleOptionType.Reckoning, re));
            }
        }

        if (options.Count == 0) // An Investigator with a Reckoning died during another reckoning
        {
            // Reckoning is over
            App.Controller.investigatorController.NewActiveInvestigator(0);// Set Lead Investigator to active
            App.Controller.mythosController.NextMythosEffect();
        }
        else
        {
            App.Controller.multipleOptionController.StartMultipleOption(options, "Select a Reckoning to Resolve", ReckoningSelected);
        }
    }

    public void ReckoningSelected(int index)
    {
        ReckoningEvent re = App.Model.reckoningMythosModel.currentEvents[index];
        if (re.source == ReckoningSource.Investigator) // Set active investigator to reckoning source
        {
            Investigator i = re.investigator;
            int invIndex = App.Model.investigatorModel.GetInvestigatorIndex(i.investigatorName);
            App.Controller.investigatorController.NewActiveInvestigator(invIndex);
        }
        
        App.Model.reckoningMythosModel.StartReckoningEvent(index);
        re.callBack();
    }

    // Reckonings must call App.Model.reckoningMythosModel.AddReckoningEvent() in response to the ReckoningEvent
    // Reckonings call FinishReckoning when they're done
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
