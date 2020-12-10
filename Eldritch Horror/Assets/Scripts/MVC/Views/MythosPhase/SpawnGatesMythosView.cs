using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGatesMythosView : MVC
{
    private GameObject spawnGatesMenu;
    private Text menuTitle;
    private GameObject gateSpawn1;
    private GameObject gateSpawn2;
    private Text advanceDoomText;
    private GameObject minimizeButton;
    private GameObject continueButton;

    private Text gateLocation1;
    private Image monsterPortrait1;
    private Text monsterName1;
    private Image gateIcon1;
    private Text gateLocation2;
    private Image monsterPortrait2;
    private Text monsterName2;
    private Image gateIcon2;

    void Start()
    {
        spawnGatesMenu = transform.GetChild(0).GetChild(0).gameObject;
        menuTitle = spawnGatesMenu.transform.GetChild(0).GetComponent<Text>();
        gateSpawn1 = spawnGatesMenu.transform.GetChild(1).gameObject;
        gateSpawn2 = spawnGatesMenu.transform.GetChild(2).gameObject;
        advanceDoomText = spawnGatesMenu.transform.GetChild(3).GetComponent<Text>();
        minimizeButton = spawnGatesMenu.transform.GetChild(4).gameObject;
        continueButton = spawnGatesMenu.transform.GetChild(5).gameObject;

        gateLocation1 = gateSpawn1.transform.GetChild(0).GetComponent<Text>();
        monsterPortrait1 = gateSpawn1.transform.GetChild(1).GetComponent<Image>();
        monsterName1 = gateSpawn1.transform.GetChild(2).GetComponent<Text>();
        gateIcon1 = gateSpawn1.transform.GetChild(3).GetComponent<Image>();
        gateLocation2 = gateSpawn2.transform.GetChild(0).GetComponent<Text>();
        monsterPortrait2 = gateSpawn2.transform.GetChild(1).GetComponent<Image>();
        monsterName2 = gateSpawn2.transform.GetChild(2).GetComponent<Text>();
        gateIcon2 = gateSpawn2.transform.GetChild(3).GetComponent<Image>();

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.spawnGatesMythosController.Continue(); });

        spawnGatesMenu.SetActive(false);
    }

    public void GatesSpawned() // All Gates and Monsters have been spawned.
    {
        spawnGatesMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(spawnGatesMenu);
        List<Location> spawned = App.Model.spawnGatesMythosModel.gatesSpawned;
        List<Monster> monsters = App.Model.spawnGatesMythosModel.monstersSpawned;
        if (spawned.Count == 0)
            menuTitle.text = "No Gates Spawned";
        else if (spawned.Count == 1)
            menuTitle.text = "Spawn 1 Gate";
        else
            menuTitle.text = "Spawn " + spawned.Count + " Gates";

        if (spawned.Count > 0)
        {
            gateSpawn1.SetActive(true);
            gateLocation1.text = spawned[0].locationName;
            Monster m = monsters[0];
            monsterPortrait1.sprite = m.monsterSprite;
            monsterName1.text = "A " + m.monsterName + " has emerged from the Gate!";
            gateIcon1.sprite = App.Model.mythosSpritesModel.GetOmenSpriteFromGateColor(spawned[0].gate);
        }
        else
        {
            gateSpawn1.SetActive(false);   
            gateSpawn2.SetActive(false);
        } 
        if (spawned.Count > 1)
        {
            
            gateSpawn2.SetActive(true);
            gateLocation2.text = spawned[1].locationName;
            Monster m = monsters[1];
            monsterPortrait2.sprite = m.monsterSprite;
            monsterName2.text = "A " + m.monsterName + " has emerged from the Gate!";
            gateIcon2.sprite = App.Model.mythosSpritesModel.GetOmenSpriteFromGateColor(spawned[1].gate);
        }
        else
        {
            gateSpawn2.SetActive(false);
        }

        if (App.Model.mythosModel.reference[App.Model.investigatorModel.investigators.Count].gates > spawned.Count)
        {
            advanceDoomText.text = "Advance Doom by " + (App.Model.mythosModel.reference[App.Model.investigatorModel.investigators.Count].gates - spawned.Count);
        }
        else
        {
            advanceDoomText.text = "";
        }
    }

    public void EventFinished()
    {
        spawnGatesMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
