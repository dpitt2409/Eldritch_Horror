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

    private Text monsterReckonings;
    private Text ancientOneReckonings;
    private Text ongoingEffectReckonings;
    private Text investigatorReckonings;

    private Text currentReckoningTitle;
    private Text currentReckoningText;
    private Text currentReckoningCounter;
    private GameObject currentReckoningNextButton;

    void Start()
    {
        reckoningMenu = transform.GetChild(0).GetChild(0).gameObject;
        startReckoningScreen = reckoningMenu.transform.GetChild(0).gameObject;
        currentReckoningScren = reckoningMenu.transform.GetChild(1).gameObject;
        nextButton = reckoningMenu.transform.GetChild(2).gameObject;
        minimizeButton = reckoningMenu.transform.GetChild(3).gameObject;

        monsterReckonings = startReckoningScreen.transform.GetChild(0).GetComponent<Text>();
        ancientOneReckonings = startReckoningScreen.transform.GetChild(1).GetComponent<Text>();
        ongoingEffectReckonings = startReckoningScreen.transform.GetChild(2).GetComponent<Text>();
        investigatorReckonings = startReckoningScreen.transform.GetChild(3).GetComponent<Text>();

        currentReckoningTitle = currentReckoningScren.transform.GetChild(0).GetComponent<Text>();
        currentReckoningText = currentReckoningScren.transform.GetChild(1).GetComponent<Text>();
        currentReckoningCounter = currentReckoningScren.transform.GetChild(2).GetComponent<Text>();
        currentReckoningNextButton = currentReckoningScren.transform.GetChild(3).gameObject;

        nextButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.reckoningMythosController.Next(); });
        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        currentReckoningNextButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.reckoningMythosController.CurrentReckoningNextButton(); });

        reckoningMenu.SetActive(false);
    }

    public void StartReckoning()
    {
        reckoningMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(reckoningMenu);
        startReckoningScreen.SetActive(true);
        currentReckoningScren.SetActive(false);
        nextButton.SetActive(true);

        List<ReckoningEvent>[] events = App.Model.reckoningMythosModel.events;
        monsterReckonings.text = "" + events[0].Count;
        ancientOneReckonings.text = "" + events[1].Count;
        ongoingEffectReckonings.text = "" + events[2].Count;
        investigatorReckonings.text = "" + events[3].Count;
    }

    public void NextReckoningEvent()
    {
        reckoningMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(reckoningMenu);
        startReckoningScreen.SetActive(false);
        currentReckoningScren.SetActive(true);
        currentReckoningNextButton.SetActive(false);
        nextButton.SetActive(false);


        int currentList = App.Model.reckoningMythosModel.activeList;
        string counterText = "";
        int total = -1;
        if (currentList == 0)
        {
            counterText += "Monster ";
            total = App.Model.reckoningMythosModel.totalMonsterReckonings;
        }
        else if (currentList == 1)
        {
            counterText += "Ancient One ";
            total = App.Model.reckoningMythosModel.totalAncientOneReckonings;
        }
        else if (currentList == 2)
        {
            counterText += "Ongoing Effect ";
            total = App.Model.reckoningMythosModel.totalOngoingEffectReckonings;
        }
        else if (currentList == 3)
        {
            counterText += "Investigator ";
            total = App.Model.reckoningMythosModel.totalInvestigatorReckonings;
        }
        List<ReckoningEvent> currentEvents = App.Model.reckoningMythosModel.currentEvents;
        counterText += "Reckoning: " + (total - currentEvents.Count + 1) + " / " + total;
        currentReckoningCounter.text = counterText;

        ReckoningEvent re = currentEvents[App.Model.reckoningMythosModel.currentEvent];
        currentReckoningTitle.text = re.title;
        currentReckoningText.text = re.text;
    }

    public void SetReckoningText(string newText)
    {
        currentReckoningText.text = newText;
    }

    public void EnableCurrentReckoningNextButton()
    {
        currentReckoningNextButton.SetActive(true);
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
