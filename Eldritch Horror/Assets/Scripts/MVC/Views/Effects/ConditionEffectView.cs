using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionEffectView : MVC
{
    private GameObject conditionEffectMenu;
    private Text menuTitle;
    private Text conditionType;
    private Text conditionText;
    private GameObject minimizeButton;
    private GameObject continueButton;

    void Start()
    {
        conditionEffectMenu = transform.GetChild(0).GetChild(0).gameObject;
        menuTitle = conditionEffectMenu.transform.GetChild(0).GetComponent<Text>();
        conditionType = conditionEffectMenu.transform.GetChild(1).GetComponent<Text>();
        conditionText = conditionEffectMenu.transform.GetChild(2).GetComponent<Text>();
        minimizeButton = conditionEffectMenu.transform.GetChild(3).gameObject;
        continueButton = conditionEffectMenu.transform.GetChild(4).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.conditionEffectController.Continue(); });

        conditionEffectMenu.SetActive(false);
    }

    public void StartConditionEffect()
    {
        conditionEffectMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(conditionEffectMenu);
        continueButton.SetActive(false);

        Condition c = App.Model.conditionEffectModel.currentCondition;
        menuTitle.text = c.conditionName + " Condition";
        conditionType.text = "" + c.type;
        conditionText.text = c.conditionText;
    }

    public void SetResultText(string text)
    {
        conditionText.text = text;
    }

    public void EffectFinished()
    {
        continueButton.SetActive(true);
    }

    public void StartFlipEffect(string title, string text)
    {
        conditionEffectMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(conditionEffectMenu);
        continueButton.SetActive(false);

        menuTitle.text = title;
        conditionType.text = "";
        conditionText.text = text;
    }

    public void Continue()
    {
        conditionEffectMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
