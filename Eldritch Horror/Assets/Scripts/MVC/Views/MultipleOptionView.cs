using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleOptionView : MVC
{
    [SerializeField]
    private GameObject monsterOption;

    private GameObject multipleOptionMenu;
    private Text menuTitle;
    private GameObject optionsList;
    private GameObject minimizeButton;

    void Start()
    {
        multipleOptionMenu = transform.GetChild(0).GetChild(0).gameObject;
        menuTitle = multipleOptionMenu.transform.GetChild(0).GetComponent<Text>();
        optionsList = multipleOptionMenu.transform.GetChild(1).gameObject;
        minimizeButton = multipleOptionMenu.transform.GetChild(2).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });

        multipleOptionMenu.SetActive(false);
    }

    public void MultipleOptionStarted()
    {
        multipleOptionMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(multipleOptionMenu);

        menuTitle.text = App.Model.multipleOptionModel.menuTitle;

        foreach (Transform child in optionsList.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < App.Model.multipleOptionModel.currentOptions.Count; i++)
        {
            MultipleOptionMenuObject o = App.Model.multipleOptionModel.currentOptions[i];
            if (o.objectType == MultipleOptionType.Monster)
            {
                GameObject go = Instantiate(monsterOption, optionsList.transform);
                go.GetComponent<Image>().sprite = o.monster.monsterSprite; // Portrait
                go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = o.monster.monsterName; // Name
                if (o.monster.damageTaken == 0) // No Taken Damage
                {
                    go.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                }
                else // Some Taken Damage
                {
                    go.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    go.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "" + o.monster.damageTaken;
                }
                go.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + o.monster.toughness; // Toughness
                if (o.monster.tests[0] == TestStat.None) // No Test 1
                {
                    go.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().sprite = App.Model.spriteModel.GetTestStatSprite(TestStat.None);
                    go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "";
                    go.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<Text>().text = "";
                    go.transform.GetChild(0).GetChild(3).GetChild(3).GetComponent<Image>().sprite = App.Model.spriteModel.GetTestStatSprite(TestStat.None);
                }
                else // Test 1
                {
                    go.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().sprite = App.Model.spriteModel.GetTestStatSprite(o.monster.tests[0]);
                    if (o.monster.testMods[0] == 0)
                        go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "";
                    else
                        go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "" + o.monster.testMods[0];
                    go.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<Text>().text = "" + o.monster.horror;
                    go.transform.GetChild(0).GetChild(3).GetChild(3).GetComponent<Image>().sprite = App.Model.spriteModel.sanitySprite;
                }
                if (o.monster.tests[1] == TestStat.None) // No Test 2
                {
                    go.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().sprite = App.Model.spriteModel.GetTestStatSprite(TestStat.None);
                    go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "";
                    go.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = "";
                    go.transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<Image>().sprite = App.Model.spriteModel.GetTestStatSprite(TestStat.None);
                }
                else // Test 2
                {
                    go.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().sprite = App.Model.spriteModel.GetTestStatSprite(o.monster.tests[1]);
                    if (o.monster.testMods[1] == 0)
                        go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "";
                    else
                        go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "" + o.monster.testMods[1];
                    go.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = "" + o.monster.damage;
                    go.transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<Image>().sprite = App.Model.spriteModel.healthSprite;
                }
                go.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = o.monster.monsterText; // Monster Text
                if (o.monster.reckoningText == "") // No Reckoning
                {
                    go.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
                }
                else // Reckoning
                {
                    go.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
                    go.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Text>().text = o.monster.reckoningText;
                }
                // Set onclick
                int copy = i;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.multipleOptionController.SelectOption(copy); });
            }
        }
    }

    public void MultipleOptionFinished()
    {
        multipleOptionMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
