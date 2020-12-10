using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatorController : MVC
{
    public void StartLeadInvestigatorSelection()
    {
        App.View.investigatorView.EnableSelectInvestigatorMenu();
    }

    public void SelectLeadInvestigator(int index)
    {
        App.Model.investigatorModel.LeadInvestigatorSelected(index);
        App.Model.investigatorModel.NewActiveInvestigator(0);
        App.View.investigatorView.LeadInvestigatorSelected();
        App.Controller.previewedInvestigatorController.SelectPreviewedInvestigator(App.Model.investigatorModel.activeInvestigator);
        GameManager.SingleInstance.LeadInvestigatorSelected();
    }

    public void NewActiveInvestigator(int index)
    {
        App.Model.investigatorModel.NewActiveInvestigator(index);
        App.Controller.previewedInvestigatorController.SelectPreviewedInvestigator(App.Model.investigatorModel.activeInvestigator);
    }
}
