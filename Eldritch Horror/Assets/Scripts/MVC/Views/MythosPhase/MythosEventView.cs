using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MythosEventView : MVC
{
    private GameObject mythosEventMenu;
    private Text eventTitle;
    private Text eventDescription;
    private GameObject minimizeButton;
    private GameObject continueButton;

    void Start()
    {
        mythosEventMenu = transform.GetChild(0).GetChild(0).gameObject;
        eventTitle = mythosEventMenu.transform.GetChild(0).GetComponent<Text>();
        eventDescription = mythosEventMenu.transform.GetChild(1).GetComponent<Text>();
        minimizeButton = mythosEventMenu.transform.GetChild(2).gameObject;
        continueButton = mythosEventMenu.transform.GetChild(3).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.mythosEventController.Continue(); });

        mythosEventMenu.SetActive(false);
    }

    public void MythosEventStarted()
    {
        mythosEventMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(mythosEventMenu);
        continueButton.SetActive(false);

        Mythos m = App.Model.mythosModel.activeMythos;
        eventTitle.text = m.mythosTitle;
        eventDescription.text = m.mythosText;
    }

    public void MythosEventFinished()
    {
        continueButton.SetActive(true);
    }

    public void Continue()
    {
        mythosEventMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
