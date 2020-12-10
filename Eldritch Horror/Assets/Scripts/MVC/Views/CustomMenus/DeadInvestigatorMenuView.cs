using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadInvestigatorMenuView : MVC
{
    private GameObject deadInvestigatorMenu;
    private Image deadInvestigatorPortrait;
    private Image deathTypeImage;
    private Text deathText;
    private Text relocateText;
    private GameObject minimizeButton;
    private GameObject continuebutton;

    void Start()
    {
        deadInvestigatorMenu = transform.GetChild(0).GetChild(0).gameObject;
        deadInvestigatorPortrait = deadInvestigatorMenu.transform.GetChild(0).GetComponent<Image>();
        deathTypeImage = deadInvestigatorMenu.transform.GetChild(1).GetComponent<Image>();
        deathText = deadInvestigatorMenu.transform.GetChild(2).GetComponent<Text>();
        relocateText = deadInvestigatorMenu.transform.GetChild(3).GetComponent<Text>();
        minimizeButton = deadInvestigatorMenu.transform.GetChild(4).gameObject;
        continuebutton = deadInvestigatorMenu.transform.GetChild(5).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continuebutton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.deadInvestigatorMenuController.Continue(); });

        deadInvestigatorMenu.SetActive(false);
    }

    public void InvestigatorDied()
    {
        deadInvestigatorMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(deadInvestigatorMenu);

        Investigator i = App.Model.deadInvestigatorMenuModel.currentDeadInvestigator;
        deadInvestigatorPortrait.sprite = i.investigatorPortrait;

        if (i.currentHealth <= 0)
        {
            deathTypeImage.sprite = App.Model.gameSpritesModel.healthSprite;
            deathText.text = i.investigatorName + " has lost all Health and died!";
        }
        else
        {
            deathTypeImage.sprite = App.Model.gameSpritesModel.sanitySprite;
            deathText.text = i.investigatorName + " has lost all Sanity and died!";
        }

        if (App.Model.deadInvestigatorMenuModel.investigatorRelocated)
        {
            relocateText.text = i.investigatorName + "'s body has been relocated to " + i.currentLocation.locationName;
        }
        else
        {
            relocateText.text = "";
        }
    }

    public void InvestigatorDevoured()
    {
        deadInvestigatorMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(deadInvestigatorMenu);

        Investigator i = App.Model.deadInvestigatorMenuModel.currentDeadInvestigator;
        deadInvestigatorPortrait.sprite = i.investigatorPortrait;
        deathTypeImage.gameObject.SetActive(false);
        deathText.text = i.investigatorName + " has been devoured!";
        relocateText.text = "";
    }

    public void Continue()
    {
        deadInvestigatorMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
