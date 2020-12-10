using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AncientOneView : MVC
{
    [SerializeField]
    private GameObject ancientOneTextBlock;

    private GameObject ancientOnePortrait;
    private GameObject ancientOneInfo;
    private Text ancientOneName;
    private Text ancientOneTitle;
    private GameObject frontScreen;
    private GameObject backScreen;
    private GameObject cultistInfo;
    private GameObject flipButton;

    private Text frontDoomText;
    private GameObject frontTextParent;
    private Text frontFlavorText;

    private GameObject backAwaken;
    private Text backAwakenTitle;
    private Text backAwakenText;
    private Text backMysteryTitle;
    private Text backMysteryFlavorText;
    private Text backMysteryText;
    private Text backTitle;
    private GameObject backTextParent;

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
    private GameObject cultistReckoning;
    private Text cultistReckoningText;

    private GameObject postFlipTokens;
    private Image postFlipTokenIcon;
    private Text postFlipTokenValue;

    void Awake()
    {
        ancientOnePortrait = transform.GetChild(0).GetChild(0).gameObject;
        ancientOneInfo = ancientOnePortrait.transform.GetChild(0).gameObject;
        ancientOneName = ancientOneInfo.transform.GetChild(0).GetComponent<Text>();
        ancientOneTitle = ancientOneInfo.transform.GetChild(1).GetComponent<Text>();
        frontScreen = ancientOneInfo.transform.GetChild(2).gameObject;
        backScreen = ancientOneInfo.transform.GetChild(3).gameObject;
        cultistInfo = ancientOneInfo.transform.GetChild(4).GetChild(0).gameObject;
        flipButton = ancientOneInfo.transform.GetChild(5).gameObject;

        frontDoomText = frontScreen.transform.GetChild(0).GetComponent<Text>();
        frontTextParent = frontScreen.transform.GetChild(1).gameObject;
        frontFlavorText = frontScreen.transform.GetChild(2).GetComponent<Text>();

        backAwaken = backScreen.transform.GetChild(0).gameObject;
        backAwakenTitle = backAwaken.transform.GetChild(0).GetComponent<Text>();
        backAwakenText = backAwaken.transform.GetChild(1).GetComponent<Text>();
        backMysteryTitle = backScreen.transform.GetChild(1).GetComponent<Text>();
        backMysteryFlavorText = backScreen.transform.GetChild(2).GetComponent<Text>();
        backMysteryText = backScreen.transform.GetChild(3).GetComponent<Text>();
        backTitle = backScreen.transform.GetChild(4).GetComponent<Text>();
        backTextParent = backScreen.transform.GetChild(5).gameObject;

        cultistToughness = cultistInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        cultistTest1Type = cultistInfo.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        cultistTest1Mod = cultistInfo.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        cultistHorror = cultistInfo.transform.GetChild(1).GetChild(2).GetComponent<Text>();
        cultistDamage1Icon = cultistInfo.transform.GetChild(1).GetChild(3).GetComponent<Image>();
        cultistTest2Type = cultistInfo.transform.GetChild(2).GetChild(0).GetComponent<Image>();
        cultistTest2Mod = cultistInfo.transform.GetChild(2).GetChild(1).GetComponent<Text>();
        cultistDamage = cultistInfo.transform.GetChild(2).GetChild(2).GetComponent<Text>();
        cultistDamage2Icon = cultistInfo.transform.GetChild(2).GetChild(3).GetComponent<Image>();
        cultistText = cultistInfo.transform.GetChild(3).GetComponent<Text>();
        cultistReckoning = cultistInfo.transform.GetChild(4).gameObject;
        cultistReckoningText = cultistReckoning.transform.GetChild(0).GetComponent<Text>();

        postFlipTokens = ancientOnePortrait.transform.GetChild(1).gameObject;
        postFlipTokenIcon = postFlipTokens.transform.GetChild(0).GetComponent<Image>();
        postFlipTokenValue = postFlipTokens.transform.GetChild(1).GetComponent<Text>();

        ancientOnePortrait.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.ancientOneController.ToggleAncientOneInfo(); });
        flipButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.ancientOneController.FlipAncientOneInfo(); });

        ancientOnePortrait.gameObject.SetActive(false);
        ancientOneInfo.SetActive(false);
        postFlipTokens.SetActive(false);
    }

    public void AncientOneSet()
    {
        ancientOnePortrait.GetComponent<Image>().sprite = App.Model.ancientOneModel.ancientOne.portrait;
        SetAncientOneInfo();
        ancientOnePortrait.gameObject.SetActive(true);
    }

    public void EnableAncientOne()
    {
        ancientOnePortrait.gameObject.SetActive(true);
    }

    public void HideAncientOne()
    {
        ancientOnePortrait.gameObject.SetActive(false);
    }

    public void ToggleAncientOneInfo()
    {
        if (ancientOneInfo.activeSelf)
        {
            App.Controller.openMenuController.ClosePopup();
            ancientOneInfo.SetActive(false);
        }
        else
        {
            ancientOneInfo.SetActive(true);
            App.Controller.openMenuController.OpenPopup(ancientOneInfo);

            AncientOne ao = App.Model.ancientOneModel.ancientOne;
            if (ao.flipped)
            {
                frontScreen.SetActive(false);
                backScreen.SetActive(true);
                SetCultistInfo(ao.cultist2);
            }
            else
            {
                frontScreen.SetActive(true);
                backScreen.SetActive(false);
                SetCultistInfo(ao.cultist1);
            }
        }
    }

    public void Flip()
    {
        AncientOne ao = App.Model.ancientOneModel.ancientOne;
        if (frontScreen.activeSelf)
        {
            frontScreen.SetActive(false);
            backScreen.SetActive(true);
            SetCultistInfo(ao.cultist2);
        }
        else
        {
            frontScreen.SetActive(true);
            backScreen.SetActive(false);
            SetCultistInfo(ao.cultist1);
        }
    }

    public void EnablePostFlipTokens(Sprite icon, int num)
    {
        postFlipTokens.SetActive(true);
        postFlipTokenIcon.sprite = icon;
        postFlipTokenValue.text = "" + num;
    }

    public void UpdatePostFlipTokens(int num)
    {
        postFlipTokenValue.text = "" + num;
    }

    private void SetAncientOneInfo()
    {
        AncientOne ao = App.Model.ancientOneModel.ancientOne;

        ancientOneName.text = ao.ancientOneName;
        ancientOneTitle.text = ao.title;
        frontDoomText.text = "" + ao.doom;
        frontFlavorText.text = ao.flavorText;
        foreach (Transform child in frontTextParent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ao.ancientOneTexts.Length; i++)
        {
            GameObject go = Instantiate(ancientOneTextBlock, frontTextParent.transform);
            go.GetComponent<Text>().text = ao.ancientOneTexts[i];
            GameObject reckoning = go.transform.GetChild(0).gameObject;
            if (i == ao.frontReckoningTextIndex)
            {
                reckoning.SetActive(true);
            }
            else
            {
                reckoning.SetActive(false);
            }
        }

        if (ao.awakenText == "")
        {
            backAwaken.SetActive(false);
        }
        else
        {
            backAwaken.SetActive(true);
            backAwakenTitle.text = ao.ancientOneName + " Awakens!";
            backAwakenText.text = ao.awakenText;
        }
        backMysteryTitle.text = ao.finalMysteryTitle;
        backMysteryFlavorText.text = ao.finalMysteryFlavorText;
        backMysteryText.text = ao.finalMysteryText;
        backTitle.text = ao.flipInfoTitle;
        foreach (Transform child in backTextParent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ao.flipTexts.Length; i++)
        {
            GameObject go = Instantiate(ancientOneTextBlock, backTextParent.transform);
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
    }

    private void SetCultistInfo(Monster c)
    {
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
        if (c.reckoningText == "")
        {
            cultistReckoning.SetActive(false);
        }
        else
        {
            cultistReckoning.SetActive(true);
            cultistReckoningText.text = c.reckoningText;
        }
    }
}
