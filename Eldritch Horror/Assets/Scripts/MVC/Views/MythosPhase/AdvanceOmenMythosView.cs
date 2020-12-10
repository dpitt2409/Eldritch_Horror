using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceOmenMythosView : MVC
{
    private GameObject advanceOmenMenu;
    private Text omenStateText;
    private Image omenStateIcon;
    private Text doomText;
    private GameObject minimizeButton;
    private GameObject continueButton;

    void Start()
    {
        advanceOmenMenu = transform.GetChild(0).GetChild(0).gameObject;
        omenStateText = advanceOmenMenu.transform.GetChild(0).GetComponent<Text>();
        omenStateIcon = advanceOmenMenu.transform.GetChild(1).GetComponent<Image>();
        doomText = advanceOmenMenu.transform.GetChild(2).GetComponent<Text>();
        minimizeButton = advanceOmenMenu.transform.GetChild(3).gameObject;
        continueButton = advanceOmenMenu.transform.GetChild(4).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.advanceOmenMythosController.FinishAdvanceOmen(); });

        advanceOmenMenu.SetActive(false);
    }

    public void StartAdvanceOmen()
    {
        advanceOmenMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(advanceOmenMenu);
        continueButton.SetActive(false);

        string color = "";
        int newOmen = App.Model.omenModel.currentOmen;
        if (newOmen == 0)
            color += "Green";
        if (newOmen == 1 || newOmen == 3)
            color += "Blue";
        if (newOmen == 2)
            color += "Red";

        omenStateText.text = "New Omen State: " + color;
        omenStateIcon.sprite = App.Model.mythosSpritesModel.GetOmenSprite(newOmen);

        int advanced = App.Model.advanceOmenMythosModel.advanceDoomAmount;
        if (advanced == 0)
        {
            doomText.text = "Do not Advance Doom";
        }
        else
        {
            doomText.text = "Advance Doom by: " + advanced;
        }
    }

    public void Done()
    {
        continueButton.SetActive(true);
    }

    public void FinishEvent()
    {
        advanceOmenMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
