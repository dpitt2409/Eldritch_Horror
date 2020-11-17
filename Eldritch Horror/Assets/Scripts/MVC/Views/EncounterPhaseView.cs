using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterPhaseView : MVC
{
    [SerializeField]
    private GameObject encounterOptionButton;

    private GameObject chooseEncounterMenu;
    private GameObject encounterOptionsParent;
    private GameObject minimizeChooseEncounterMenuButton;

    void Start()
    {
        chooseEncounterMenu = transform.GetChild(0).GetChild(0).gameObject;
        encounterOptionsParent = chooseEncounterMenu.transform.GetChild(1).gameObject;
        minimizeChooseEncounterMenuButton = chooseEncounterMenu.transform.GetChild(2).gameObject;

        minimizeChooseEncounterMenuButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });

        chooseEncounterMenu.SetActive(false);
    }

    public void StartChooseEncounter()
    {
        chooseEncounterMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(chooseEncounterMenu);

        foreach (Transform child in encounterOptionsParent.transform)
        {
            Destroy(child.gameObject);
        }

        Location l = App.Model.investigatorModel.activeInvestigator.currentLocation;

        // Check if location has a defeated Investigator on it
        foreach (Investigator i in l.deadInvestigatorsOnLocation)
        {
            GameObject deadInvestigator = Instantiate(encounterOptionButton, encounterOptionsParent.transform);
            deadInvestigator.GetComponentInChildren<Text>().text = i.investigatorName + " Death Encounter";
            deadInvestigator.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.encounterPhaseController.ChooseDeadInvestigatorEncounter(l, i); });
        }

        // Check if location has a mystery related encounter on it
        // Check if location has a Rumor encounter on it
        // Check if location has an expedition token on it
        if (l.expeditionsOnLocation.Count > 0)
        {
            GameObject expedition = Instantiate(encounterOptionButton, encounterOptionsParent.transform);
            expedition.GetComponentInChildren<Text>().text = "Expedition Encounter";
            expedition.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.encounterPhaseController.ChooseExpeditionEncounter(l); });
        }
        // Check if location has a gate on it
        if (l.activeGate)
        {
            GameObject gate = Instantiate(encounterOptionButton, encounterOptionsParent.transform);
            gate.GetComponentInChildren<Text>().text = "OtherWorldly Encounter";
            gate.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.encounterPhaseController.ChooseOtherWorldlyEncounter(); });
        }
        // Check if location has a clue on it
        if (l.cluesOnLocation.Count > 0)
        {
            GameObject research = Instantiate(encounterOptionButton, encounterOptionsParent.transform);
            research.GetComponentInChildren<Text>().text = l.type + " Research Encounter";
            research.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.encounterPhaseController.ChooseResearchEncounter(l); });
        }

        // Check if location has a location specific encounter
        if (l.majorLocation)
        {
            GameObject location = Instantiate(encounterOptionButton, encounterOptionsParent.transform);
            location.GetComponentInChildren<Text>().text = l.locationName + " Location Encounter";
            location.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.encounterPhaseController.ChooseLocationEncounter(l); });
        }

        // General encounter
        GameObject general = Instantiate(encounterOptionButton, encounterOptionsParent.transform);
        general.GetComponentInChildren<Text>().text = l.type + " General Encounter";
        general.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.encounterPhaseController.ChooseGeneralEncounter(l.type); });
    }

    public void EncounterChosen()
    {
        chooseEncounterMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
