using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewedInvestigatorController : MVC
{
    public void TogglePreviewedInvestigatorList()
    {
        App.View.previewedInvestigatorView.TogglePreviewedInvestigatorList();
    }

    public void SelectPreviewedInvestigator(Investigator i)
    {
        App.Model.previewedInvestigatorModel.SetPreviewedInvestigator(i);
    }

    public void SelectPreviewedInvestigatorFromList(Investigator i)
    {
        if (i.investigatorName != App.Model.previewedInvestigatorModel.previewedInvestigator.investigatorName) // We are changing previewed Investigator
        {
            App.Model.previewedInvestigatorModel.SetPreviewedInvestigator(i);
            if (i.investigatorName != App.Model.investigatorModel.activeInvestigator.investigatorName) // The active Investigator is not being previewed anymore
            {
                if (App.Model.minimizeModel.currentCallBack == null) // The current Menu was not minimized
                {
                    App.Controller.openMenuController.HideOpenMenu();
                }
                else
                {
                    App.Controller.minimizeController.HideMaximizeButton();
                }
            }
            else // The active Investigator is becoming previewed again
            {
                if (App.Model.minimizeModel.currentCallBack == null) // The current Menu was not minimized
                {
                    App.Controller.openMenuController.EnableOpenMenu();
                }
                else // The current Menu was minimized
                {
                    App.Controller.minimizeController.EnableMaximizeButton();
                }
            }
        }
    }

    public void UpdateInvestigator()
    {
        App.View.previewedInvestigatorView.PreviewedInvestigatorUpdated();
    }
}
