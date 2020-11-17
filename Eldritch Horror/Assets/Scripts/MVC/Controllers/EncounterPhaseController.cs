using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterPhaseController : MVC
{
    public void StartEncounterPhase()
    {
        App.Model.encounterPhaseModel.StartEncounterPhase();
    }

    public void ChooseGeneralEncounter(LocationType type)
    {
        App.View.encounterPhaseView.EncounterChosen();
        App.Controller.encounterMenuController.StartEncounter(App.Model.encounterModel.DrawGeneralEncounter(type));
    }

    public void ChooseLocationEncounter(Location l)
    {
        App.View.encounterPhaseView.EncounterChosen();
        App.Controller.encounterMenuController.StartEncounter(App.Model.encounterModel.DrawLocationEncounter(l));
    }

    public void ChooseResearchEncounter(Location l)
    {
        App.View.encounterPhaseView.EncounterChosen();
        App.Controller.encounterMenuController.StartEncounter(App.Model.researchModel.DrawResearchEncounter(l.type));
    }

    public void ChooseOtherWorldlyEncounter()
    {
        App.View.encounterPhaseView.EncounterChosen();
        App.Controller.complexEncounterMenuController.StartComplexEncounter(App.Model.encounterModel.DrawOtherWorldlyEncounter());
    }

    public void ChooseExpeditionEncounter(Location l)
    {
        App.View.encounterPhaseView.EncounterChosen();
        Expedition e = l.expeditionsOnLocation[Random.Range(0, l.expeditionsOnLocation.Count)];
        App.Controller.locationController.ResolveExpedition(l, e);
        App.Controller.complexEncounterMenuController.StartComplexEncounter(e.encounter);
    }

    public void ChooseDeadInvestigatorEncounter(Location l, Investigator i)
    {
        App.View.encounterPhaseView.EncounterChosen();
        App.Model.investigatorModel.activeInvestigator.GainPossessions(i.possessions);
        App.Controller.locationController.DeadInvestigatorTokenClaimed(i, l);
        App.Controller.encounterMenuController.StartEncounter(i.deathEncounter);
    }
}
