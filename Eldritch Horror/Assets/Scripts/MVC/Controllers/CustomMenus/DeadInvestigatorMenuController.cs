using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadInvestigatorMenuController : MVC
{
    public void InvestigatorDied(Investigator i, bool relocated)
    {
        // Hide the menu for whatever killed you
        App.Controller.openMenuController.HideOpenMenu();
        App.Model.deadInvestigatorMenuModel.InvestigatorDied(i, relocated);
    }

    public void InvestigatorDevoured(Investigator i, DevouredCallBack callback)
    {
        i.Devoured();
        // Hide the menu for whatever killed you
        App.Controller.openMenuController.HideOpenMenu();
        App.Model.deadInvestigatorMenuModel.InvestigatorDevoured(i, callback);
    }

    public void Continue()
    {
        App.View.deadInvestigatorMenuView.Continue();

        // Advance Doom by 1
        App.Model.doomModel.AdvanceDoom(1);
        App.Controller.queueController.CreateCallBackQueue(PassLeadInvestigator); // Create Queue
        App.Model.eventModel.DoomAdvancedEvent(1); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void PassLeadInvestigator()
    {
        // pass lead investigator
        int playerIndex = -1;
        List<Investigator> allInvestigators = App.Model.investigatorModel.investigators;
        for (int i = 0; i < allInvestigators.Count; i++)
        {
            if (allInvestigators[i].investigatorName == App.Model.deadInvestigatorMenuModel.currentDeadInvestigator.investigatorName)
            {
                playerIndex = i;
            }
        }
        if (playerIndex == 0) // This Investigator was the Lead Investigator
        {
            List<MultipleOptionMenuObject> options = new List<MultipleOptionMenuObject>();
            foreach (Investigator i in App.Model.investigatorModel.investigators)
            {
                if (i.deathEncounter == null)
                    options.Add(new MultipleOptionMenuObject(MultipleOptionType.Investigator, i));
            }
            if (options.Count == 0) // All Investigators are currently dead
            {
                FinishDeath();
            }
            else // Need to select a new Lead Investigator
            {
                App.Controller.multipleOptionController.StartMultipleOption(options, "Select a new Lead Investigator", LeadInvestigatorPassed);
            }
        }
        else // The dead Investigator was not the Lead Investigator
        {
            FinishDeath();
        }
    }

    public void LeadInvestigatorPassed(int index)
    {
        List<Investigator> options = new List<Investigator>();
        foreach (Investigator i in App.Model.investigatorModel.investigators)
        {
            if (i.deathEncounter == null)
                options.Add(i);
        }
        int newLeadIndex = App.Model.investigatorModel.GetInvestigatorIndex(options[index].investigatorName);
        App.Model.investigatorModel.LeadInvestigatorSelected(newLeadIndex);

        FinishDeath();
    }

    public void FinishDeath()
    {
        // Enable the menu for whatever killed you
        App.Controller.openMenuController.EnableOpenMenu();
        if (App.Model.deadInvestigatorMenuModel.devoured)
        {
            App.Model.deadInvestigatorMenuModel.devouredCallBack();
        }
        else
        {
            App.Controller.queueController.NextCallBack();
        }
    }
}
