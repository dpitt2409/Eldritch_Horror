using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryView : MVC
{
    private GameObject mysteryUI;
    private Text mysteriesSolvedText;
    private GameObject mysteryInfo;
    private Text mysteryTitle;
    private Text mysteryDescription;
    private Text mysteryProgress;
    private Text mysteryText;

    private GameObject newMysteryMenu;
    private Text newMysterySolvedText;
    private GameObject newMysteryScreen;
    private GameObject minimizeNewMysteryMenuButton;
    private GameObject newMysteryMenuContinueButton;
    private Text newMysteryTitle;
    private Text newMysteryFlavorText;
    private Text newMysteryText;

    void Awake()
    {
        mysteryUI = transform.GetChild(0).GetChild(0).gameObject;
        mysteriesSolvedText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        mysteryInfo = transform.GetChild(0).GetChild(1).gameObject;
        mysteryTitle = mysteryInfo.transform.GetChild(0).GetComponent<Text>();
        mysteryDescription = mysteryInfo.transform.GetChild(1).GetComponent<Text>();
        mysteryProgress = mysteryInfo.transform.GetChild(2).GetComponent<Text>();
        mysteryText = mysteryInfo.transform.GetChild(3).GetComponent<Text>();

        newMysteryMenu = transform.GetChild(0).GetChild(2).gameObject;
        newMysterySolvedText = newMysteryMenu.transform.GetChild(0).GetComponent<Text>();
        newMysteryScreen = newMysteryMenu.transform.GetChild(1).gameObject;
        minimizeNewMysteryMenuButton = newMysteryMenu.transform.GetChild(2).gameObject;
        newMysteryMenuContinueButton = newMysteryMenu.transform.GetChild(3).gameObject;
        newMysteryTitle = newMysteryScreen.transform.GetChild(0).GetComponent<Text>();
        newMysteryFlavorText = newMysteryScreen.transform.GetChild(1).GetComponent<Text>();
        newMysteryText = newMysteryScreen.transform.GetChild(2).GetComponent<Text>();

        mysteryUI.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.mysteryController.ToggleMysteryInfo(); });
        minimizeNewMysteryMenuButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        newMysteryMenuContinueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.mysteryController.NewMysteryContinue(); });

        mysteryUI.SetActive(false);
        mysteryInfo.SetActive(false);
        newMysteryMenu.SetActive(false);
    }

    public void EnableMysteryUI()
    {
        mysteryUI.SetActive(true);
    }

    public void HideMysteryUI()
    {
        mysteryUI.SetActive(false);
    }

    public void ToggleMysteryInfo()
    {
        if (mysteryInfo.activeSelf)
        {
            App.Controller.openMenuController.ClosePopup();
            mysteryInfo.SetActive(false);
        }
        else
        {
            App.Controller.openMenuController.OpenPopup(mysteryInfo);
            mysteryInfo.SetActive(true);
        }
    }

    public void UpdateMysteryInfo()
    {
        mysteriesSolvedText.text = App.Model.mysteryModel.mysteriesSolved + " / " + App.Model.ancientOneModel.ancientOne.numMysteries;

        Mystery m = App.Model.mysteryModel.activeMystery;
        mysteryTitle.text = m.mysteryName;
        mysteryDescription.text = m.mysteryDescription;
        mysteryProgress.text = App.Model.mysteryModel.mysteryProgress + " / " + m.requirement;
        mysteryText.text = m.mysteryText;
    }

    public void MysterySolved()
    {
        newMysteryMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(newMysteryMenu);
        newMysteryScreen.SetActive(false);
        newMysterySolvedText.text = App.Model.mysteryModel.activeMystery.mysteryName + " Mystery Solved!";
    }

    public void NewMystery()
    {
        newMysteryMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(newMysteryMenu);
        newMysterySolvedText.text = "";
        newMysteryScreen.SetActive(true);

        Mystery m = App.Model.mysteryModel.activeMystery;
        newMysteryTitle.text = m.mysteryName;
        newMysteryFlavorText.text = m.mysteryDescription;
        newMysteryText.text = m.mysteryText;
    }

    public void NewMysteryContinue()
    {
        newMysteryMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
