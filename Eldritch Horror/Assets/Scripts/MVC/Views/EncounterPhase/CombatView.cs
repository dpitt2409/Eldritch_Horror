using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatView : MVC
{
    private GameObject combatMenu;
    private Text monsterName;
    private Image monsterPortrait;
    private Text toughnessText;
    private GameObject damageTaken;
    private Text monsterText;
    private GameObject monsterReckoning;

    private Image monsterTest1Type;
    private Text monsterTest1Mod;
    private Text monsterHorror;
    private Image damage1Icon;
    private GameObject test1Highlight;
    private Image monsterTest2Type;
    private Text monsterTest2Mod;
    private Text monsterDamage;
    private Image damage2Icon;
    private GameObject test2Highlight;

    private Text resultsText1;
    private Text resultsText2;
    private GameObject minimizeCombatMenuButton;
    private GameObject continueCombatMenuButton;

    void Start()
    {
        combatMenu = transform.GetChild(0).GetChild(0).gameObject;
        monsterName = combatMenu.transform.GetChild(1).GetComponent<Text>();
        monsterPortrait = combatMenu.transform.GetChild(2).GetComponent<Image>();
        toughnessText = combatMenu.transform.GetChild(3).GetComponent<Text>();
        damageTaken = combatMenu.transform.GetChild(4).gameObject;
        monsterText = combatMenu.transform.GetChild(5).GetComponent<Text>();
        monsterReckoning = combatMenu.transform.GetChild(6).gameObject;
        monsterTest1Type = combatMenu.transform.GetChild(7).GetChild(0).GetComponent<Image>();
        monsterTest1Mod = combatMenu.transform.GetChild(7).GetChild(1).GetComponent<Text>();
        monsterHorror = combatMenu.transform.GetChild(7).GetChild(2).GetComponent<Text>();
        damage1Icon = combatMenu.transform.GetChild(7).GetChild(3).GetComponent<Image>();
        test1Highlight = combatMenu.transform.GetChild(7).GetChild(4).gameObject;
        monsterTest2Type = combatMenu.transform.GetChild(8).GetChild(0).GetComponent<Image>();
        monsterTest2Mod = combatMenu.transform.GetChild(8).GetChild(1).GetComponent<Text>();
        monsterDamage = combatMenu.transform.GetChild(8).GetChild(2).GetComponent<Text>();
        damage2Icon = combatMenu.transform.GetChild(8).GetChild(3).GetComponent<Image>();
        test2Highlight = combatMenu.transform.GetChild(8).GetChild(4).gameObject;
        resultsText1 = combatMenu.transform.GetChild(9).GetChild(1).GetComponent<Text>();
        resultsText2 = combatMenu.transform.GetChild(9).GetChild(2).GetComponent<Text>();
        minimizeCombatMenuButton = combatMenu.transform.GetChild(10).gameObject;
        continueCombatMenuButton = combatMenu.transform.GetChild(11).gameObject;

        minimizeCombatMenuButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueCombatMenuButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.combatController.MonsterContinue(); });

        combatMenu.SetActive(false);
    }

    public void StartFight(Monster m)
    {
        combatMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(combatMenu);
        continueCombatMenuButton.SetActive(false);
        test1Highlight.SetActive(false);
        test2Highlight.SetActive(false);

        monsterName.text = m.monsterName;
        monsterPortrait.sprite = m.monsterSprite;
        toughnessText.text = "" + m.toughness;
        if (m.damageTaken > 0)
        {
            damageTaken.SetActive(true);
            damageTaken.GetComponentInChildren<Text>().text = "" + m.damageTaken;
        }
        else
        {
            damageTaken.SetActive(false);
        }
        monsterText.text = m.monsterText;
        if (m.reckoningText == "")
        {
            monsterReckoning.SetActive(false);
        }
        else
        {
            monsterReckoning.SetActive(true);
            monsterReckoning.GetComponentInChildren<Text>().text = m.reckoningText;
        }

        if (m.tests[0] == TestStat.None)
        {
            monsterTest1Type.sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
            monsterTest1Mod.text = "";
            monsterHorror.text = "";
            damage1Icon.sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
        }
        else
        {
            monsterTest1Type.sprite = App.Model.gameSpritesModel.GetTestStatSprite(m.tests[0]);
            if (m.testMods[0] == 0)
                monsterTest1Mod.text = "";
            else
                monsterTest1Mod.text = "" + m.testMods[0];
            monsterHorror.text = "" + m.horror;
            damage1Icon.sprite = App.Model.gameSpritesModel.sanitySprite;
        }
        if (m.tests[1] == TestStat.None)
        {
            monsterTest2Type.sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
            monsterTest2Mod.text = "";
            monsterDamage.text = "";
            damage2Icon.sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
        }
        else
        {
            monsterTest2Type.sprite = App.Model.gameSpritesModel.GetTestStatSprite(m.tests[1]);
            if (m.testMods[1] == 0)
                monsterTest2Mod.text = "";
            else
                monsterTest2Mod.text = "" + m.testMods[1];
            monsterDamage.text = "" + m.damage;
            damage2Icon.sprite = App.Model.gameSpritesModel.healthSprite;
        }
        resultsText1.text = "";
        resultsText2.text = "";
    }

    public void DamageTaken()
    {
        Monster m = App.Model.combatModel.currentMonster;
        if (m.damageTaken > 0)
        {
            damageTaken.SetActive(true);
            damageTaken.GetComponentInChildren<Text>().text = "" + m.damageTaken;
        }
        else
        {
            damageTaken.SetActive(false);
        }
    }

    public void Test1Started()
    {
        test1Highlight.SetActive(true);
        test2Highlight.SetActive(false);
    }

    public void Test2Started()
    {
        test1Highlight.SetActive(false);
        test2Highlight.SetActive(true);
    }

    public void Test1Complete(int success)
    {
        Monster m = App.Model.combatModel.currentMonster;
        int damage = m.horror - success;
        if (damage < 0)
            damage = 0;
        resultsText1.text = m.tests[0] + " Test: " + success + " (" + damage + " Sanity taken)";
    }

    public void Test2Complete(int success)
    {
        Monster m = App.Model.combatModel.currentMonster;
        int damage = m.damage - success;
        if (damage < 0)
            damage = 0;
        resultsText2.text = m.tests[1] + " Test: " + success + " (" + damage + " Health taken)";
    }

    public void FightFinished()
    {
        continueCombatMenuButton.SetActive(true);
    }

    public void NextFight()
    {
        combatMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
