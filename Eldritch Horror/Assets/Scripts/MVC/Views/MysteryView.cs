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

    void Awake()
    {
        mysteryUI = transform.GetChild(0).GetChild(0).gameObject;
        mysteriesSolvedText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        mysteryInfo = transform.GetChild(0).GetChild(1).gameObject;
        mysteryTitle = mysteryInfo.transform.GetChild(0).GetComponent<Text>();
        mysteryDescription = mysteryInfo.transform.GetChild(1).GetComponent<Text>();
        mysteryProgress = mysteryInfo.transform.GetChild(2).GetComponent<Text>();
        mysteryText = mysteryInfo.transform.GetChild(3).GetComponent<Text>();

        mysteryUI.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.mysteryController.ToggleMysteryInfo(); });

        mysteryUI.SetActive(false);
        mysteryInfo.SetActive(false);
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
        mysteryInfo.SetActive(!mysteryInfo.activeSelf);
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
}
