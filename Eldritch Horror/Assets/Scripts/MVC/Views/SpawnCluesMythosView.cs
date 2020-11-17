using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCluesMythosView : MVC
{
    private GameObject spawnCluesMenu;
    private Text menuTitle;
    private GameObject clueSpawn1;
    private GameObject clueSpawn2;
    private GameObject clueSpawn3;
    private GameObject clueSpawn4;
    private GameObject minimizeButton;
    private GameObject continueButton;

    private Text clueSpawnText1;
    private Text clueSpawnText2;
    private Text clueSpawnText3;
    private Text clueSpawnText4;

    void Start()
    {
        spawnCluesMenu = transform.GetChild(0).GetChild(0).gameObject;
        menuTitle = spawnCluesMenu.transform.GetChild(0).GetComponent<Text>();
        clueSpawn1 = spawnCluesMenu.transform.GetChild(1).gameObject;
        clueSpawn2 = spawnCluesMenu.transform.GetChild(2).gameObject;
        clueSpawn3 = spawnCluesMenu.transform.GetChild(3).gameObject;
        clueSpawn4 = spawnCluesMenu.transform.GetChild(4).gameObject;
        minimizeButton = spawnCluesMenu.transform.GetChild(5).gameObject;
        continueButton = spawnCluesMenu.transform.GetChild(6).gameObject;
        clueSpawnText1 = clueSpawn1.transform.GetChild(0).GetComponent<Text>();
        clueSpawnText2 = clueSpawn2.transform.GetChild(0).GetComponent<Text>();
        clueSpawnText3 = clueSpawn3.transform.GetChild(0).GetComponent<Text>();
        clueSpawnText4 = clueSpawn4.transform.GetChild(0).GetComponent<Text>();

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.spawnCluesMythosController.Continue(); });

        spawnCluesMenu.SetActive(false);
    }

    public void CluesSpawned()
    {
        spawnCluesMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(spawnCluesMenu);
        List<Clue> spawned = App.Model.spawnCluesMythosModel.spawnedClues;
        menuTitle.text = "Spawn " + spawned.Count + " Clues";

        clueSpawn1.SetActive(true);
        clueSpawn2.SetActive(true);
        clueSpawn3.SetActive(true);
        clueSpawn4.SetActive(true);
        if (spawned.Count < 4)
        {
            clueSpawn4.SetActive(false);
        }
        if (spawned.Count < 3)
        {
            clueSpawn3.SetActive(false);
        }
        if (spawned.Count < 2)
        {
            clueSpawn2.SetActive(false);
        }
        if (spawned.Count < 1)
        {
            clueSpawn1.SetActive(false);
        }

        if (spawned.Count > 0) // at least 1 clue spawned
        {
            clueSpawnText1.text = spawned[0].location.locationName;
        }
        if (spawned.Count > 1) // at least 2 clues spawned
        {
            clueSpawnText2.text = spawned[1].location.locationName;
        }
        if (spawned.Count > 2) // at least 3 clues spawned
        {
            clueSpawnText3.text = spawned[2].location.locationName;
        }
        if (spawned.Count > 3) // at least 4 clues spawned
        {
            clueSpawnText4.text = spawned[3].location.locationName;
        }
    }

    public void Continue()
    {
        spawnCluesMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
