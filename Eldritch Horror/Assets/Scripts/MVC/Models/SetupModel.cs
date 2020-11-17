using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupModel : MVC
{
    public Investigator[] investigatorList;

    public List<AncientOne> ancieontOneList;

    public bool skipSetup = false;

    [HideInInspector]
    public Investigator previewedInvestigator;

    [HideInInspector]
    public List<Investigator> selectedInvestigators;

    [HideInInspector]
    public AncientOne previewedAncientOne;

    [HideInInspector]
    public bool ancientOnePreviewFront = true;

    [HideInInspector]
    public bool reDrafting = false;

    [HideInInspector]
    public List<Investigator> defeatedInvestigators;

    [HideInInspector]
    public int reDraftIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        selectedInvestigators = new List<Investigator>();
        defeatedInvestigators = new List<Investigator>();
        ancieontOneList = new List<AncientOne>();
        GenerateAncientOneList();
    }

    public void SetSkipSetupData()
    {
        selectedInvestigators.Add(investigatorList[0]);
        selectedInvestigators.Add(investigatorList[1]);
        selectedInvestigators.Add(investigatorList[2]);
        selectedInvestigators.Add(investigatorList[3]);
        previewedAncientOne = new Cthulu();
    }

    private void GenerateAncientOneList()
    {
        ancieontOneList.Add(new Cthulu());
        ancieontOneList.Add(new Yig());
    }

    public void StartSetup()
    {
        App.View.setupView.SetupStarted();
    }

    public void SetPreviewedInvestigator(Investigator inv)
    {
        previewedInvestigator = inv;
        App.View.setupView.InvestigatorPreviewUpdated();
    }

    public void SelectInvestigator()
    {
        selectedInvestigators.Add(previewedInvestigator);
        previewedInvestigator = null;
        App.View.setupView.InvestigatorAdded();
    }

    public void RemoveInvestigator(Investigator inv)
    {
        selectedInvestigators.Remove(inv);
        App.View.setupView.InvestigatorRemoved();
    }

    public void CompleteInvestigatorSelection()
    {
        App.View.setupView.CompleteInvestigatorSelection();
    }

    public void SetPreviewedAncientOne(AncientOne ao)
    {
        previewedAncientOne = ao;
        App.View.setupView.AncientOnePreviewUpdated();
    }

    public void SetAncientOnePreviewSide(bool val)
    {
        ancientOnePreviewFront = val;
    }

    public void FlipAncientOnePreview()
    {
        ancientOnePreviewFront = !ancientOnePreviewFront;
        App.View.setupView.AncientOneFlipped();
    }

    public void SelectAncientOne()
    {
        App.View.setupView.AncientOneSelected();
    }

    public void StartReDraft()
    {
        reDrafting = true;
        // Create the selected investigator list with all investigators except 1 dead investigator
        int replaceIndex = -1;
        List<Investigator> investigators = App.Model.investigatorModel.investigators;
        for (int i = 0; i < investigators.Count; i++)
        {
            if (investigators[i].deathEncounter != null)
            {
                replaceIndex = i;
            }
        }
        reDraftIndex = replaceIndex;
        selectedInvestigators.Clear();
        for (int i = 0; i < investigators.Count; i++)
        {
            if (i != replaceIndex)
            {
                selectedInvestigators.Add(investigators[i]);
            }
        }
        defeatedInvestigators.Add(investigators[replaceIndex]);

        App.View.setupView.SetupStarted();
    }

    public void FinishReDraft()
    {
        reDrafting = false;
        App.View.setupView.ReDraftFinished();
    }
}
