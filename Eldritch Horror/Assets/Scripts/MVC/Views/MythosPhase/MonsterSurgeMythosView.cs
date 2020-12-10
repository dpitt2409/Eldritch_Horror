using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSurgeMythosView : MVC
{
    [SerializeField]
    private GameObject monsterSurgeObject;
    [SerializeField]
    private GameObject monsterObject;

    private GameObject monsterSurgeMenu;
    private Image omenIcon;
    private GameObject surgeLocations;
    private GameObject gateSpawn;
    private GameObject minimizeButton;
    private GameObject continueButton;

    void Start()
    {
        monsterSurgeMenu = transform.GetChild(0).GetChild(0).gameObject;
        omenIcon = monsterSurgeMenu.transform.GetChild(0).GetComponent<Image>();
        surgeLocations = monsterSurgeMenu.transform.GetChild(1).gameObject;
        gateSpawn = monsterSurgeMenu.transform.GetChild(2).gameObject;
        minimizeButton = monsterSurgeMenu.transform.GetChild(3).gameObject;
        continueButton = monsterSurgeMenu.transform.GetChild(4).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.monsterSurgeMythosController.Continue(); });

        monsterSurgeMenu.SetActive(false);
    }

    public void MonstersSurged()
    {
        monsterSurgeMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(monsterSurgeMenu);

        omenIcon.sprite = App.Model.mythosSpritesModel.GetOmenSprite(App.Model.omenModel.currentOmen);
        if (App.Model.monsterSurgeMythosModel.locationsToSurge.Count > 0) // Surge happened
        {
            surgeLocations.SetActive(true);
            gateSpawn.SetActive(false);

            foreach (Transform child in surgeLocations.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Location l in App.Model.monsterSurgeMythosModel.locationsToSurge)
            {
                List<Monster> monsters = App.Model.monsterSurgeMythosModel.surged[l];
                GameObject surgeObject = Instantiate(monsterSurgeObject, surgeLocations.transform);
                surgeObject.transform.GetChild(0).GetComponent<Text>().text = l.locationName;
                foreach (Monster m in monsters)
                {
                    GameObject mo = Instantiate(monsterObject, surgeObject.transform.GetChild(1));
                    mo.transform.GetChild(0).GetComponent<Image>().sprite = m.monsterSprite;
                    mo.transform.GetChild(1).GetComponent<Text>().text = m.monsterName;
                }
            }
        }
        else // A Gate was spawned
        {
            surgeLocations.SetActive(false);
            gateSpawn.SetActive(true);

            Location l = App.Model.monsterSurgeMythosModel.gateSpawnLocation;
            Monster m = App.Model.monsterSurgeMythosModel.singleMonsterSpawn;
            gateSpawn.transform.GetChild(0).GetComponent<Text>().text = l.locationName;
            gateSpawn.transform.GetChild(1).GetComponent<Image>().sprite = m.monsterSprite;
            gateSpawn.transform.GetChild(2).GetComponent<Text>().text = m.monsterName;
            gateSpawn.transform.GetChild(3).GetComponent<Image>().sprite = App.Model.mythosSpritesModel.GetOmenSpriteFromGateColor(l.gate);
        }
    }

    public void SurgeFinished()
    {
        monsterSurgeMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
