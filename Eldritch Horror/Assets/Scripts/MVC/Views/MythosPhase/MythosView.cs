using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MythosView : MVC
{
    [SerializeField]
    private GameObject eventIcon;
    private GameObject mythosMenu;
    private Text mythosTitle;
    private Text mythosText;
    private GameObject mythosEventsParent;
    private GameObject minimizeButton;
    private GameObject continueButton;

    void Start()
    {
        mythosMenu = transform.GetChild(0).GetChild(0).gameObject;
        mythosTitle = mythosMenu.transform.GetChild(0).GetComponent<Text>();
        mythosText = mythosMenu.transform.GetChild(1).GetComponent<Text>();
        mythosEventsParent = mythosMenu.transform.GetChild(2).gameObject;
        minimizeButton = mythosMenu.transform.GetChild(3).gameObject;
        continueButton = mythosMenu.transform.GetChild(4).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.mythosController.Continue(); });

        mythosMenu.SetActive(false);
    }

    public void StartMythos()
    {
        mythosMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(mythosMenu);
        Mythos m = App.Model.mythosModel.activeMythos;
        mythosTitle.text = m.mythosTitle;
        mythosText.text = m.mythosFlavorText;
        
        foreach (Transform child in mythosEventsParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (MythosEffects me in m.effects)
        {
            if (me == MythosEffects.AdvanceOmen)
            {
                GameObject go = Instantiate(eventIcon, mythosEventsParent.transform);
                go.GetComponent<Image>().sprite = App.Model.mythosSpritesModel.advanceOmenSprite;
            }
            if (me == MythosEffects.ResolveReckoning)
            {
                GameObject go = Instantiate(eventIcon, mythosEventsParent.transform);
                go.GetComponent<Image>().sprite = App.Model.mythosSpritesModel.reckoningSprite;
            }
            if (me == MythosEffects.SpawnGates)
            {
                GameObject go = Instantiate(eventIcon, mythosEventsParent.transform);
                go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.gateSprite;
            }
            if (me == MythosEffects.SpawnClues)
            {
                GameObject go = Instantiate(eventIcon, mythosEventsParent.transform);
                go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.clueTokenSprite;
            }
            if (me == MythosEffects.MonsterSurge)
            {
                GameObject go = Instantiate(eventIcon, mythosEventsParent.transform);
                go.GetComponent<Image>().sprite = App.Model.mythosSpritesModel.monsterSurgeSprite;
            }
            if (me == MythosEffects.Rumor)
            {
                GameObject go = Instantiate(eventIcon, mythosEventsParent.transform);
                go.GetComponent<Image>().sprite = App.Model.mythosSpritesModel.rumorSprite;
            }
        }
    }

    public void Next()
    {
        mythosMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
