using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewedInvestigatorModel : MVC
{
    public Investigator previewedInvestigator;

    // Called after GameSetup
    public void EnableInvestigatorUI()
    {
        App.View.previewedInvestigatorView.InvestigatorUIEnabled();
        previewedInvestigator = App.Model.investigatorModel.investigators[0];
        App.View.previewedInvestigatorView.PreviewedInvestigatorUpdated();
    }

    public void SetPreviewedInvestigator(Investigator i)
    {
        previewedInvestigator = i;
        App.View.previewedInvestigatorView.PreviewedInvestigatorUpdated();
    }
}
