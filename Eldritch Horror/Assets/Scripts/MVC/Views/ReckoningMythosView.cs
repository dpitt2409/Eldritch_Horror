using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReckoningMythosView : MVC
{
    private GameObject reckoningMenu;
    private GameObject startReckoningScreen;
    private GameObject currentReckoningScren;
    private GameObject nextButton;
    private GameObject minimizeButton;

    private Text currentReckoningTitle;
    private Text currentReckoningText;
    private GameObject currentReckoningNextButton;

    void Start()
    {
        reckoningMenu = transform.GetChild(0).GetChild(0).gameObject;
        startReckoningScreen = reckoningMenu.transform.GetChild(0).gameObject;
        currentReckoningScren = reckoningMenu.transform.GetChild(1).gameObject;
        nextButton = reckoningMenu.transform.GetChild(2).gameObject;
        minimizeButton = reckoningMenu.transform.GetChild(3).gameObject;

        currentReckoningTitle = currentReckoningScren.transform.GetChild(0).GetComponent<Text>();
        currentReckoningText = currentReckoningScren.transform.GetChild(1).GetComponent<Text>();
        currentReckoningNextButton = currentReckoningScren.transform.GetChild(2).gameObject;

        nextButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.reckoningMythosController.Next(); });
        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });

        reckoningMenu.SetActive(false);
    }

    public void StartReckoning()
    {
        reckoningMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(reckoningMenu);
        startReckoningScreen.SetActive(true);
        currentReckoningScren.SetActive(false);
        nextButton.SetActive(true);
    }

    public void NextReckoningEvent()
    {
        reckoningMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(reckoningMenu);
        startReckoningScreen.SetActive(false);
        currentReckoningScren.SetActive(true);
        currentReckoningNextButton.SetActive(false);
        nextButton.SetActive(false);

        ReckoningEvent re = App.Model.reckoningMythosModel.currentEvents[App.Model.reckoningMythosModel.currentEvent];
        currentReckoningTitle.text = re.title;
        currentReckoningText.text = re.text;
    }

    public void EnableCurrentReckoningNextButton()
    {
        currentReckoningNextButton.SetActive(true);
        currentReckoningNextButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.reckoningMythosController.CurrentReckoningNextButton(); });
    }

    public void CurrentReckoningNextButton()
    {
        currentReckoningNextButton.SetActive(false);
    }

    public void EnableNextReckoning()
    {
        nextButton.SetActive(true);
    }

    public void ReckoningFinished()
    {
        reckoningMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
