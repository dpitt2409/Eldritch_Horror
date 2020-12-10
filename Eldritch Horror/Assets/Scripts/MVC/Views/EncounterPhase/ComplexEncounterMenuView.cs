using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexEncounterMenuView : MVC
{
    private GameObject complexEncounterMenu;
    private Text complexEncounterMenuTitle;
    private GameObject encounterBlocks;
    private Text complexBlock1Text;
    private GameObject complexBlock1Highlight;
    private Text complexBlock2Text;
    private GameObject complexBlock2Highlight;
    private Text complexBlock3Text;
    private GameObject complexBlock3Highlight;
    private Text resultsText;
    private GameObject minimizeComplexEncounterMenuButton;
    private GameObject continueComplexEncounterButton;

    void Start()
    {
        complexEncounterMenu = transform.GetChild(0).GetChild(0).gameObject;
        complexEncounterMenuTitle = complexEncounterMenu.transform.GetChild(0).GetComponent<Text>();
        encounterBlocks = complexEncounterMenu.transform.GetChild(1).gameObject;
        complexBlock1Text = encounterBlocks.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        complexBlock1Highlight = encounterBlocks.transform.GetChild(0).GetChild(1).gameObject;
        complexBlock2Text = encounterBlocks.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        complexBlock2Highlight = encounterBlocks.transform.GetChild(1).GetChild(1).gameObject;
        complexBlock3Text = encounterBlocks.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        complexBlock3Highlight = encounterBlocks.transform.GetChild(2).GetChild(1).gameObject;
        resultsText = complexEncounterMenu.transform.GetChild(2).GetComponent<Text>();
        minimizeComplexEncounterMenuButton = complexEncounterMenu.transform.GetChild(3).gameObject;
        continueComplexEncounterButton = complexEncounterMenu.transform.GetChild(4).gameObject;

        minimizeComplexEncounterMenuButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueComplexEncounterButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.complexEncounterMenuController.ContinueEncounter(); });

        complexEncounterMenu.SetActive(false);
    }

    public void StartComplexEncounter(ComplexEncounter encounter)
    {
        complexEncounterMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(complexEncounterMenu);
        continueComplexEncounterButton.SetActive(false);
        encounterBlocks.SetActive(true);
        resultsText.text = "";
        complexEncounterMenuTitle.text = encounter.title;
        complexBlock1Text.text = encounter.encounterTexts[0];
        complexBlock2Text.text = encounter.encounterTexts[1];
        complexBlock3Text.text = encounter.encounterTexts[2];
        complexBlock1Highlight.SetActive(true);
        complexBlock2Highlight.SetActive(false);
        complexBlock3Highlight.SetActive(false);
    }

    public void StartPhase2()
    {
        complexBlock1Highlight.SetActive(false);
        if (App.Model.complexEncounterMenuModel.testIndex == 1)
        {
            complexBlock2Highlight.SetActive(true);
            complexBlock3Highlight.SetActive(false);
        }
        else
        {
            complexBlock2Highlight.SetActive(false);
            complexBlock3Highlight.SetActive(true);
        }
    }

    public void ComplexEncounterComplete()
    {
        encounterBlocks.SetActive(false);

        ComplexEncounter e = App.Model.complexEncounterMenuModel.currentComplexEncounter;
        if (App.Model.complexEncounterMenuModel.passed)
        {
            resultsText.text = e.passTexts[App.Model.complexEncounterMenuModel.testIndex];
        }
        else
        {
            resultsText.text = e.failTexts[App.Model.complexEncounterMenuModel.testIndex];
        }
        continueComplexEncounterButton.SetActive(true);
    }

    public void ComplexEncounterContinue()
    {
        complexEncounterMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
