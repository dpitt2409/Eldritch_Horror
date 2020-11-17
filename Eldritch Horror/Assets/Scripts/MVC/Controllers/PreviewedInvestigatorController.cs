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

        if (App.Model.openMenuModel.openMenus.Count > 0) // There is an open menu
        {
            if (i.investigatorName == App.Model.investigatorModel.activeInvestigator.investigatorName) // The selected investigator is the active investigator
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
            else // The selected investigator is not the active investigator
            {
                App.Controller.openMenuController.HideOpenMenu();
                App.Controller.minimizeController.HideMaximizeButton();
            }
        }
    }

    public void UpdateInvestigator()
    {
        App.View.previewedInvestigatorView.PreviewedInvestigatorUpdated();
    }
}
