using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupController : MVC
{
    public void StartSetup()
    {
        App.Model.setupModel.StartSetup();
    }

    public void SkipSetup()
    {
        App.Model.setupModel.SetSkipSetupData();
        FinishSetup();
    }

    public void CompleteInvestigatorSelection()
    {
        App.Model.setupModel.CompleteInvestigatorSelection();
    }

    public void SelectInvestigatorFromPool(Investigator inv)
    {
        App.Model.setupModel.SetPreviewedInvestigator(inv);
    }

    public void AddInvestigator()
    {
        if (App.Model.setupModel.reDrafting)
        {
            // replace with previewed investigator
            App.Model.investigatorModel.ReplaceInvestigator(App.Model.setupModel.previewedInvestigator, App.Model.setupModel.reDraftIndex);
            App.Model.setupModel.previewedInvestigator.JoinGame();
            App.Model.setupModel.FinishReDraft();

            // set the previewed investigator to investigators[0]
            App.Controller.investigatorController.NewActiveInvestigator(0);
            App.Controller.previewedInvestigatorController.SelectPreviewedInvestigator(App.Model.investigatorModel.activeInvestigator);

            EnableAllUI();
            // Call Queue Callback
            App.Controller.queueController.NextCallBack();
        }
        else
        {
            App.Model.setupModel.SelectInvestigator();
        }
    }

    public void RemoveInvestigator(Investigator inv)
    {
        if (!App.Model.setupModel.reDrafting)
            App.Model.setupModel.RemoveInvestigator(inv);
    }

    public void SelectAncientOneFromPool(AncientOne ao)
    {
        App.Model.setupModel.SetPreviewedAncientOne(ao);
    }

    public void FlipAncientOne()
    {
        App.Model.setupModel.FlipAncientOnePreview();
    }

    public void SelectAncientOne()
    {
        App.Model.setupModel.SelectAncientOne();
        FinishSetup();
    }

    public void FinishSetup()
    {
        App.Model.investigatorModel.SetInvestigatorList(App.Model.setupModel.selectedInvestigators);
        App.Model.ancientOneModel.SetAncientOne(App.Model.setupModel.previewedAncientOne);

        App.Model.previewedInvestigatorModel.EnableInvestigatorUI();
        App.Model.omenModel.EnableOmenUI();
        App.Model.doomModel.EnableDoomUI();
        App.Controller.mysteryController.EnableMysteryUI();
        App.Model.researchModel.SetResearchDecks(App.Model.setupModel.previewedAncientOne.CreateResearchDeck());
        App.Controller.reserveController.EnableReserve();
        App.Controller.locationController.EnableMap();
        App.Controller.locationController.SpawnNewExpedition();
        // Spawn initial clues/gates
        int initialClues = App.Model.mythosModel.reference[App.Model.investigatorModel.investigators.Count].clues;
        int initialGates = App.Model.mythosModel.reference[App.Model.investigatorModel.investigators.Count].gates;
        App.Model.clueModel.SpawnClues(initialClues);
        foreach (Location l in App.Model.gateModel.DrawGates(initialGates))
        {
            App.Controller.locationController.SpawnGate(l);
            Monster m = App.Model.monsterModel.SpawnMonster();
            App.Controller.locationController.SpawnMonsterOnLocation(m, l);
        }

        GameManager.SingleInstance.SetupFinished();
    }

    public void DraftNewInvestigator()
    {
        HideAllUI();
        App.Model.setupModel.StartReDraft();
    }

    public void HideAllUI()
    {
        App.View.previewedInvestigatorView.HideInvestigatorUI();
        App.View.ancientOneView.HideAncientOne();
        App.View.mysteryView.HideMysteryUI();
        App.View.reserveView.HideReserve();
        App.View.doomView.HideDoomUI();
        App.View.omenView.HideOmenUI();
        App.View.locationView.DisableMap();
        App.View.ongoingEffectView.HideOngoingEffectUI();
    }

    public void EnableAllUI()
    {
        App.View.previewedInvestigatorView.InvestigatorUIEnabled();
        App.View.ancientOneView.EnableAncientOne();
        App.View.mysteryView.EnableMysteryUI();
        App.View.reserveView.EnableReserve();
        App.View.doomView.DoomUIEnabled();
        App.View.omenView.OmenUIEnabled();
        App.View.locationView.EnableMap();
        App.View.ongoingEffectView.EnableOngoingEffectUI();
    }
}
