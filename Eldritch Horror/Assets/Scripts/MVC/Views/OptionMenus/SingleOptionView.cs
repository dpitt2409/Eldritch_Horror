using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleOptionView : MVC
{
    private GameObject singleOptionMenu;
    private Text menuTitle;
    private Text menuText;
    private GameObject yesButton;
    private GameObject noButton;
    private GameObject minimizeButton;

    private Text yesButtonText;
    private Text noButtonText;

    void Start()
    {
        singleOptionMenu = transform.GetChild(0).GetChild(0).gameObject;
        menuTitle = singleOptionMenu.transform.GetChild(0).GetComponent<Text>();
        menuText = singleOptionMenu.transform.GetChild(1).GetComponent<Text>();
        yesButton = singleOptionMenu.transform.GetChild(2).gameObject;
        noButton = singleOptionMenu.transform.GetChild(3).gameObject;
        minimizeButton = singleOptionMenu.transform.GetChild(4).gameObject;
        yesButtonText = yesButton.transform.GetComponentInChildren<Text>();
        noButtonText = noButton.transform.GetComponentInChildren<Text>();

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        yesButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.singleOptionController.OptionSelected(true); });
        noButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.singleOptionController.OptionSelected(false); });

        singleOptionMenu.SetActive(false);
    }

    public void SingleOptionStarted()
    {
        singleOptionMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(singleOptionMenu);

        menuTitle.text = App.Model.singleOptionModel.menuTitle;
        menuText.text = App.Model.singleOptionModel.menuText;
        yesButtonText.text = App.Model.singleOptionModel.yesButtonText;
        noButtonText.text = App.Model.singleOptionModel.noButtonText;
    }

    public void EventFinished()
    {
        singleOptionMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
