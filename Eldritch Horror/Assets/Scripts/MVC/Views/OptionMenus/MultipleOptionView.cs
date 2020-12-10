using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleOptionView : MVC
{
    [SerializeField]
    private GameObject textOption;
    [SerializeField]
    private GameObject monsterOption;
    [SerializeField]
    private GameObject investigatorOption;
    [SerializeField]
    private GameObject statOption;
    [SerializeField]
    private GameObject ancientOneReckoningOption;
    [SerializeField]
    private GameObject ongoingEffectReckoningOption;
    [SerializeField]
    private GameObject investigatorReckoningOption;
    [SerializeField]
    private GameObject eventOption;

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
            if (o.objectType == MultipleOptionType.Text)
            {
                GameObject go = Instantiate(textOption, optionsList.transform);
                go.GetComponentInChildren<Text>().text = o.text;
                int copy = i;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.multipleOptionController.SelectOption(copy); });
            }
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
                    go.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
                    go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "";
                    go.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<Text>().text = "";
                    go.transform.GetChild(0).GetChild(3).GetChild(3).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
                }
                else // Test 1
                {
                    go.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(o.monster.tests[0]);
                    if (o.monster.testMods[0] == 0)
                        go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "";
                    else
                        go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "" + o.monster.testMods[0];
                    go.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<Text>().text = "" + o.monster.horror;
                    go.transform.GetChild(0).GetChild(3).GetChild(3).GetComponent<Image>().sprite = App.Model.gameSpritesModel.sanitySprite;
                }
                if (o.monster.tests[1] == TestStat.None) // No Test 2
                {
                    go.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
                    go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "";
                    go.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = "";
                    go.transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
                }
                else // Test 2
                {
                    go.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(o.monster.tests[1]);
                    if (o.monster.testMods[1] == 0)
                        go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "";
                    else
                        go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "" + o.monster.testMods[1];
                    go.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = "" + o.monster.damage;
                    go.transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<Image>().sprite = App.Model.gameSpritesModel.healthSprite;
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
            else if (o.objectType == MultipleOptionType.Investigator)
            {
                GameObject go = Instantiate(investigatorOption, optionsList.transform);
                Investigator inv = o.investigator;
                go.GetComponent<Image>().sprite = inv.investigatorPortrait;
                go.GetComponentInChildren<Text>().text = inv.investigatorName;

                // Set onclick
                int copy = i;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.multipleOptionController.SelectOption(copy); });
            }
            else if (o.objectType == MultipleOptionType.Stat)
            {
                GameObject go = Instantiate(statOption, optionsList.transform);
                TestStat stat = o.stat;
                go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(stat);
                go.GetComponentInChildren<Text>().text = "" + stat;

                // Set onclick
                int copy = i;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.multipleOptionController.SelectOption(copy); });
            }
            else if (o.objectType == MultipleOptionType.Reckoning)
            {
                ReckoningEvent re = o.reckoning;
                if (re.source == ReckoningSource.AncientOne)
                {
                    GameObject go = Instantiate(ancientOneReckoningOption, optionsList.transform);
                    go.GetComponent<Image>().sprite = re.icon;
                    go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = re.title;
                    go.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = re.text;
                    int copy = i;
                    go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.multipleOptionController.SelectOption(copy); });
                }
                if (re.source == ReckoningSource.Ongoing)
                {
                    GameObject go = Instantiate(ongoingEffectReckoningOption, optionsList.transform);
                    go.GetComponent<Image>().sprite = re.icon;
                    go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = re.title;
                    go.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = re.text;
                    int copy = i;
                    go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.multipleOptionController.SelectOption(copy); });
                }
                if (re.source == ReckoningSource.Investigator)
                {
                    GameObject go = Instantiate(investigatorReckoningOption, optionsList.transform);
                    go.GetComponent<Image>().sprite = re.icon;
                    go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = re.title;
                    go.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = re.text;
                    go.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = re.investigator.investigatorPortrait;
                    int copy = i;
                    go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.multipleOptionController.SelectOption(copy); });
                }
            }
            else if (o.objectType == MultipleOptionType.AssetEvent)
            {
                GameObject go = Instantiate(eventOption, optionsList.transform);
                Asset a = o.asset;
                go.GetComponent<Image>().sprite = a.assetPortrait;
                go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = a.ownedInvestigator.investigatorName + "'s " + a.assetName;
                go.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = a.text;
                go.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = a.ownedInvestigator.investigatorPortrait;
                int copy = i;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.multipleOptionController.SelectOption(copy); });
            }
            else if (o.objectType == MultipleOptionType.SpellEvent)
            {
                GameObject go = Instantiate(eventOption, optionsList.transform);
                Spell s = o.spell;
                go.GetComponent<Image>().sprite = s.spellPortrait;
                go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = s.owner.investigatorName + "'s " + s.spellName;
                go.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = s.text;
                go.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = s.owner.investigatorPortrait;
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
