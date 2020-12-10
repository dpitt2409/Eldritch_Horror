using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AncientOneFlippedMenuView : MVC
{
    [SerializeReference]
    private GameObject textBlock;

    private GameObject flippedMenu;
    private Text menuTitle;
    private GameObject awakenEvent;
    private Text awakenTitle;
    private Text awakenEventText;
    private Text mysteryTitle;
    private Text mysteryFlavorText;
    private Text mysteryText;
    private Text infoTitle;
    private GameObject infoTexts;

    private Text cultistToughness;
    private Image cultistTest1Type;
    private Text cultistTest1Mod;
    private Text cultistHorror;
    private Image cultistDamage1Icon;
    private Image cultistTest2Type;
    private Text cultistTest2Mod;
    private Text cultistDamage;
    private Image cultistDamage2Icon;
    private Text cultistText;

    private GameObject minimizeButton;
    private GameObject continueButton;

    void Start()
    {
        flippedMenu = transform.GetChild(0).GetChild(0).gameObject;
        menuTitle = flippedMenu.transform.GetChild(0).GetComponent<Text>();
        awakenEvent = flippedMenu.transform.GetChild(1).gameObject;
        awakenTitle = awakenEvent.transform.GetChild(0).GetComponent<Text>();
        awakenEventText = awakenEvent.transform.GetChild(1).GetComponent<Text>();
        mysteryTitle = flippedMenu.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        mysteryFlavorText = flippedMenu.transform.GetChild(2).GetChild(1).GetComponent<Text>();
        mysteryText = flippedMenu.transform.GetChild(2).GetChild(2).GetComponent<Text>();
        infoTitle = flippedMenu.transform.GetChild(3).GetChild(0).GetComponent<Text>();
        infoTexts = flippedMenu.transform.GetChild(3).GetChild(1).gameObject;

        cultistToughness = flippedMenu.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>();
        cultistTest1Type = flippedMenu.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>();
        cultistTest1Mod = flippedMenu.transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<Text>();
        cultistHorror = flippedMenu.transform.GetChild(4).GetChild(1).GetChild(2).GetComponent<Text>();
        cultistDamage1Icon = flippedMenu.transform.GetChild(4).GetChild(1).GetChild(3).GetComponent<Image>();
        cultistTest2Type = flippedMenu.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>();
        cultistTest2Mod = flippedMenu.transform.GetChild(4).GetChild(2).GetChild(1).GetComponent<Text>();
        cultistDamage = flippedMenu.transform.GetChild(4).GetChild(2).GetChild(2).GetComponent<Text>();
        cultistDamage2Icon = flippedMenu.transform.GetChild(4).GetChild(2).GetChild(3).GetComponent<Image>();
        cultistText = flippedMenu.transform.GetChild(4).GetChild(3).GetComponent<Text>();

        minimizeButton = flippedMenu.transform.GetChild(5).gameObject;
        continueButton = flippedMenu.transform.GetChild(6).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.ancientOneFlippedMenuController.Continue(); });

        flippedMenu.SetActive(false);
    }

    public void Flipped()
    {
        flippedMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(flippedMenu);

        
        AncientOne ao = App.Model.ancientOneModel.ancientOne;
        menuTitle.text = ao.ancientOneName + " has Awoken!";
        if (ao.awakenText == "")
        {
            awakenEvent.SetActive(false);
        }
        else
        {
            awakenTitle.text = ao.ancientOneName + " Awakens!";
            awakenEventText.text = ao.awakenText;
        }
        mysteryTitle.text = ao.finalMysteryTitle;
        mysteryFlavorText.text = ao.finalMysteryFlavorText;
        mysteryText.text = ao.finalMysteryText;
        infoTitle.text = ao.flipInfoTitle;
        foreach (Transform child in infoTexts.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ao.flipTexts.Length; i++)
        {
            GameObject go = Instantiate(textBlock, infoTexts.transform);
            go.GetComponent<Text>().text = ao.flipTexts[i];
            GameObject reckoning = go.transform.GetChild(0).gameObject;
            if (i == ao.backReckoningTextIndex)
            {
                reckoning.SetActive(true);
            }
            else
            {
                reckoning.SetActive(false);
            }
        }

        Monster c = ao.cultist2;
        cultistToughness.text = "" + c.toughness;
        if (c.tests[0] == TestStat.None)
        {
            cultistTest1Type.sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
            cultistTest1Mod.text = "";
            cultistHorror.text = "";
            cultistDamage1Icon.sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
        }
        else
        {
            cultistTest1Type.sprite = App.Model.gameSpritesModel.GetTestStatSprite(c.tests[0]);
            if (c.testMods[0] == 0)
                cultistTest1Mod.text = "";
            else
                cultistTest1Mod.text = "" + c.testMods[0];
            cultistHorror.text = "" + c.horror;
            cultistDamage1Icon.sprite = App.Model.gameSpritesModel.sanitySprite;
        }
        if (c.tests[1] == TestStat.None)
        {
            cultistTest2Type.sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
            cultistTest2Mod.text = "";
            cultistDamage.text = "";
            cultistDamage2Icon.sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
        }
        else
        {
            cultistTest2Type.sprite = App.Model.gameSpritesModel.GetTestStatSprite(c.tests[1]);
            if (c.testMods[1] == 0)
                cultistTest2Mod.text = "";
            else
                cultistTest2Mod.text = "" + c.testMods[1];
            cultistDamage.text = "" + c.damage;
            cultistDamage2Icon.sprite = App.Model.gameSpritesModel.healthSprite;
        }
        cultistText.text = c.monsterText;
    }

    public void Continue()
    {
        flippedMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
