using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RumorMythosView : MVC
{
    private GameObject rumorMenu;
    private Text mythosTitle;
    private Text mythosText;
    private GameObject locationInfo;
    private GameObject eldritchInfo;
    private GameObject reckoningInfo;
    private GameObject minimizeButton;
    private GameObject continueButton;

    private Text locationText;
    private Text eldritchText;
    private Text reckoningText;

    void Start()
    {
        rumorMenu = transform.GetChild(0).GetChild(0).gameObject;
        mythosTitle = rumorMenu.transform.GetChild(0).GetComponent<Text>();
        mythosText = rumorMenu.transform.GetChild(1).GetComponent<Text>();
        locationInfo = rumorMenu.transform.GetChild(2).gameObject;
        eldritchInfo = rumorMenu.transform.GetChild(3).gameObject;
        reckoningInfo = rumorMenu.transform.GetChild(4).gameObject;
        minimizeButton = rumorMenu.transform.GetChild(5).gameObject;
        continueButton = rumorMenu.transform.GetChild(6).gameObject;

        locationText = locationInfo.transform.GetChild(0).GetComponent<Text>();
        eldritchText = eldritchInfo.transform.GetChild(0).GetComponent<Text>();
        reckoningText = reckoningInfo.transform.GetChild(0).GetComponent<Text>();

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.rumorMythosController.Continue(); });

        rumorMenu.SetActive(false);
    }

    public void RumorStarted()
    {
        rumorMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(rumorMenu);

        OngoingEffect rumor = App.Model.rumorMythosModel.currentRumor;
        mythosTitle.text = rumor.effectTitle;
        mythosText.text = rumor.effectText;

        if (rumor.location == "")
        {
            locationInfo.SetActive(false);
        }
        else
        {
            locationInfo.SetActive(true);
            locationText.text = rumor.location;
        }

        if (rumor.eldritchTokens == 0)
        {
            eldritchInfo.SetActive(false);
        }
        else
        {
            eldritchInfo.SetActive(true);
            eldritchText.text = "" + rumor.eldritchTokens;
        }

        if (rumor.reckoningText == "")
        {
            reckoningInfo.SetActive(false);
        }
        else
        {
            reckoningInfo.SetActive(true);
            reckoningText.text = rumor.reckoningText;
        }
    }

    public void EventFinished()
    {
        rumorMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
