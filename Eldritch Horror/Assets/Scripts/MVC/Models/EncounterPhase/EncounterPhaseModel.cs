using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterPhaseModel : MVC
{
    public List<Investigator> investigators; // If an investigator dies during an encounter, the list order changes
    public int turnIndex = 0;

    public void StartEncounterPhase()
    {
        investigators = new List<Investigator>(App.Model.investigatorModel.investigators);
        turnIndex = -1;
        NextEncounterTurn();
    }

    public void NextEncounterTurn()
    {
        turnIndex++;
        if (turnIndex == investigators.Count) // All Investigators have done their encounter turn
        {
            // action phase is done
            GameManager.SingleInstance.EncounterPhaseComplete();
        }
        else // More Investigators need to take their Action turn
        {
            Investigator active = investigators[turnIndex];
            int newIndex = App.Model.investigatorModel.GetInvestigatorIndex(active.investigatorName);
            App.Controller.investigatorController.NewActiveInvestigator(newIndex);
            if (active.deathEncounter != null) // Investigator is dead
            {
                NextEncounterTurn();
                return;
            }

            if (active.currentLocation.monstersOnLocation.Count > 0)
            {
                // Fight monsters
                App.Controller.combatController.StartCombatEncounter(active, active.currentLocation.monstersOnLocation, false, CombatFinished);
            }
            else
            {
                // Choose Encounter
                App.View.encounterPhaseView.StartChooseEncounter();
            }
        }
    }

    public void CombatFinished()
    {
        Investigator active = investigators[turnIndex];
        if (active.currentLocation.monstersOnLocation.Count > 0)
        {
            NextEncounterTurn();
        }
        else
        {
            if (active.deathEncounter != null) // Investigator Died during the combat
            {
                NextEncounterTurn();
            }
            else
            {
                App.View.encounterPhaseView.StartChooseEncounter();
            }
        }
    }
}
