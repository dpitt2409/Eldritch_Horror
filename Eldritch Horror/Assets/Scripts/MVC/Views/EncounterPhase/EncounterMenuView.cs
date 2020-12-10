using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterMenuView : MVC
{
    private GameObject encounterMenu;
    private Text encounterMenuTitle;
    private Text encounterMenuText;
    private GameObject minimizeEncounterMenuButton;
    private GameObject continueEncounterButton;

    void Start()
    {
        encounterMenu = transform.GetChild(0).GetChild(0).gameObject;
        encounterMenuTitle = encounterMenu.transform.GetChild(0).GetComponent<Text>();
        encounterMenuText = encounterMenu.transform.GetChild(1).GetComponent<Text>();
        minimizeEncounterMenuButton = encounterMenu.transform.GetChild(2).gameObject;
        continueEncounterButton = encounterMenu.transform.GetChild(3).gameObject;

        minimizeEncounterMenuButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueEncounterButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.encounterMenuController.ContinueEncounter(); });

        encounterMenu.SetActive(false);
    }

    public void StartEncounter(Encounter encounter)
    {
        encounterMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(encounterMenu);
        continueEncounterButton.SetActive(false);
        encounterMenuTitle.text = encounter.title;
        encounterMenuText.text = encounter.encounterText;
    }

    public void EncounterTested(Encounter encounter, bool passed)
    {
        if (passed)
        {
            encounterMenuText.text = encounter.passText;
        }
        else
        {
            encounterMenuText.text = encounter.failText;
        }
    }

    public void EncounterComplete()
    {
        continueEncounterButton.SetActive(true);
    }

    public void EncounterContinue()
    {
        encounterMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
