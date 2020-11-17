using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupView : MVC
{
    [SerializeField]
    private GameObject investigatorIcon;

    [SerializeField]
    private GameObject ancientOneTextBlock;

    [SerializeField]
    private GameObject startingAsset;

    private GameObject setupMenu;
    private GameObject investigatorScreen;
    private GameObject ancientOneScreen;

    private GameObject investigatorPoolParent;
    private GameObject selectedInvestigatorsParent;
    private GameObject investigatorPreview;
    private GameObject doneButton;

    private Image investigatorPreviewPortrait;
    private Text investigatorPreviewName;
    private Text investigatorPreviewFlavorText;
    private Text investigatorPreviewActionAbility;
    private Text investigatorPreviewPassiveAbility;
    private Text investigatorPreviewBio;
    private Text investigatorPreviewStartingLocation;
    private GameObject investigatorPreviewStartingEquipmentParent;
    private Text investigatorPreviewLore;
    private Text investigatorPreviewInfluence;
    private Text investigatorPreviewObservation;
    private Text investigatorPreviewStrength;
    private Text investigatorPreviewWill;
    private GameObject addInvestigatorButton;

    private GameObject ancientOnePreview;
    private GameObject ancientOneListParent;

    private Text ancientOneName;
    private Text ancientOneTitle;
    private Image ancientOnePortrait;
    private GameObject flipButton;
    private GameObject selectAncientOneButton;
    private GameObject previewFront;
    private GameObject previewBack;

    private GameObject ancientOneTextsParent;
    private Text ancientOnePreviewBio;
    private Text ancientOnePreviewDoomText;

    private Text cultistToughness;
    private Image cultistTest1Type;
    private Text cultistTest1Mod;
    private Text cultistHorror;
    private Image damage1Icon;
    private Image cultistTest2Type;
    private Text cultistTest2Mod;
    private Text cultistDamage;
    private Image damage2Icon;
    private Text cultistText;

    private GameObject awakenEvent;
    private Text awakenEventTitle;
    private Text awakenEventText;

    private Text finalMysteryTitle;
    private Text finalMysteryFlavorText;
    private Text finalMysteryText;

    private Text flipInfoTitle;
    private GameObject flipInfoTextParent;

    // Start is called before the first frame update
    void Start()
    {
        setupMenu = transform.GetChild(0).GetChild(0).gameObject;
        investigatorScreen = setupMenu.transform.GetChild(0).gameObject;
        ancientOneScreen = setupMenu.transform.GetChild(1).gameObject;

        investigatorPoolParent = investigatorScreen.transform.GetChild(0).gameObject;
        selectedInvestigatorsParent = investigatorScreen.transform.GetChild(1).gameObject;
        investigatorPreview = investigatorScreen.transform.GetChild(2).gameObject;
        doneButton = investigatorScreen.transform.GetChild(8).gameObject;

        investigatorPreviewPortrait = investigatorPreview.transform.GetChild(0).GetComponent<Image>();
        investigatorPreviewName = investigatorPreview.transform.GetChild(1).GetComponent<Text>();
        investigatorPreviewFlavorText = investigatorPreview.transform.GetChild(2).GetComponent<Text>();
        investigatorPreviewActionAbility = investigatorPreview.transform.GetChild(4).GetComponent<Text>();
        investigatorPreviewPassiveAbility = investigatorPreview.transform.GetChild(5).GetComponent<Text>();
        investigatorPreviewBio = investigatorPreview.transform.GetChild(6).GetComponent<Text>();
        investigatorPreviewStartingLocation = investigatorPreview.transform.GetChild(8).GetComponent<Text>();
        investigatorPreviewStartingEquipmentParent = investigatorPreview.transform.GetChild(10).gameObject;
        investigatorPreviewLore = investigatorPreview.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<Text>();
        investigatorPreviewInfluence = investigatorPreview.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<Text>();
        investigatorPreviewObservation = investigatorPreview.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<Text>();
        investigatorPreviewStrength = investigatorPreview.transform.GetChild(11).GetChild(3).GetChild(0).GetComponent<Text>();
        investigatorPreviewWill = investigatorPreview.transform.GetChild(11).GetChild(4).GetChild(0).GetComponent<Text>();
        
        addInvestigatorButton = investigatorPreview.transform.GetChild(12).gameObject;

        ancientOnePreview = ancientOneScreen.transform.GetChild(0).gameObject;
        ancientOneListParent = ancientOneScreen.transform.GetChild(1).gameObject;

        ancientOneName = ancientOnePreview.transform.GetChild(0).GetComponent<Text>();
        ancientOneTitle = ancientOnePreview.transform.GetChild(1).GetComponent<Text>();
        ancientOnePortrait = ancientOnePreview.transform.GetChild(2).GetComponent<Image>();
        flipButton = ancientOnePreview.transform.GetChild(3).gameObject;
        selectAncientOneButton = ancientOnePreview.transform.GetChild(4).gameObject;
        previewFront = ancientOnePreview.transform.GetChild(5).gameObject;
        previewBack = ancientOnePreview.transform.GetChild(6).gameObject;

        ancientOneTextsParent = previewFront.transform.GetChild(0).gameObject;
        ancientOnePreviewBio = previewFront.transform.GetChild(1).GetComponent<Text>();
        ancientOnePreviewDoomText = previewFront.transform.GetChild(2).GetComponent<Text>();
        
        cultistToughness = ancientOnePreview.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<Text>();
        cultistTest1Type = ancientOnePreview.transform.GetChild(7).GetChild(1).GetChild(0).GetComponent<Image>();
        cultistTest1Mod = ancientOnePreview.transform.GetChild(7).GetChild(1).GetChild(1).GetComponent<Text>();
        cultistHorror = ancientOnePreview.transform.GetChild(7).GetChild(1).GetChild(2).GetComponent<Text>();
        damage1Icon = ancientOnePreview.transform.GetChild(7).GetChild(1).GetChild(3).GetComponent<Image>();
        cultistTest2Type = ancientOnePreview.transform.GetChild(7).GetChild(2).GetChild(0).GetComponent<Image>();
        cultistTest2Mod = ancientOnePreview.transform.GetChild(7).GetChild(2).GetChild(1).GetComponent<Text>();
        cultistDamage = ancientOnePreview.transform.GetChild(7).GetChild(2).GetChild(2).GetComponent<Text>();
        damage2Icon = ancientOnePreview.transform.GetChild(7).GetChild(2).GetChild(3).GetComponent<Image>();
        cultistText = ancientOnePreview.transform.GetChild(7).GetChild(3).GetComponent<Text>();

        awakenEvent = previewBack.transform.GetChild(0).gameObject;
        awakenEventTitle = awakenEvent.transform.GetChild(0).GetComponent<Text>();
        awakenEventText = awakenEvent.transform.GetChild(1).GetComponent<Text>();

        finalMysteryTitle = previewBack.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        finalMysteryFlavorText = previewBack.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        finalMysteryText = previewBack.transform.GetChild(1).GetChild(2).GetComponent<Text>();

        flipInfoTitle = previewBack.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        flipInfoTextParent = previewBack.transform.GetChild(2).GetChild(1).gameObject;

        doneButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.setupController.CompleteInvestigatorSelection(); });
        addInvestigatorButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.setupController.AddInvestigator(); });

        flipButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.setupController.FlipAncientOne(); });
        selectAncientOneButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.setupController.SelectAncientOne(); });

        setupMenu.SetActive(false);
    }
    
    public void SkipSetup()
    {
        setupMenu.SetActive(false);
    }

    public void SetupStarted()
    {
        setupMenu.SetActive(true);
        investigatorScreen.SetActive(true);
        ancientOneScreen.SetActive(false);
        investigatorPreview.SetActive(false);
        doneButton.SetActive(false);

        CreateSelectedInvestigatorList();
        CreateInvestigatorPoolList();
    }

    private void CreateSelectedInvestigatorList()
    {
        foreach (Transform child in selectedInvestigatorsParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Investigator inv in App.Model.setupModel.selectedInvestigators)
        {
            GameObject go = Instantiate(investigatorIcon, selectedInvestigatorsParent.transform);
            go.GetComponent<Image>().sprite = inv.investigatorPortrait;
            go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.setupController.RemoveInvestigator(inv); });
        }
    }

    private void CreateInvestigatorPoolList()
    {
        foreach (Transform child in investigatorPoolParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Investigator inv in App.Model.setupModel.investigatorList)
        {
            if (App.Model.setupModel.selectedInvestigators.Contains(inv) || App.Model.setupModel.defeatedInvestigators.Contains(inv))
                continue;

            GameObject go = Instantiate(investigatorIcon, investigatorPoolParent.transform);
            go.GetComponent<Image>().sprite = inv.investigatorPortrait;
            go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.setupController.SelectInvestigatorFromPool(inv); });
        }
    }

    public void InvestigatorPreviewUpdated()
    {
        investigatorPreview.SetActive(true);
        addInvestigatorButton.SetActive(true);

        Investigator i = App.Model.setupModel.previewedInvestigator;

        investigatorPreviewPortrait.sprite = i.investigatorPortrait;
        investigatorPreviewName.text = i.investigatorName + " - " + i.occupation;
        investigatorPreviewFlavorText.text = i.flavorText;
        investigatorPreviewActionAbility.text = i.actionAbilityText;
        investigatorPreviewPassiveAbility.text = i.passiveAbilityText;
        investigatorPreviewBio.text = i.bioText;
        investigatorPreviewStartingLocation.text = i.startingLocation.locationName;

        investigatorPreviewLore.text = "" + i.lore;
        investigatorPreviewInfluence.text = "" + i.influence;
        investigatorPreviewObservation.text = "" + i.observation;
        investigatorPreviewStrength.text = "" + i.strength;
        investigatorPreviewWill.text = "" + i.will;

        foreach (Transform child in investigatorPreviewStartingEquipmentParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (StartingItem item in i.startingItems)
        {
            if (item.type == StartingItemType.Asset)
            {
                Asset a = App.Model.assetModel.ReferenceAssetByName(item.val);
                if (a != null)
                {
                    GameObject assetGo = Instantiate(startingAsset, investigatorPreviewStartingEquipmentParent.transform);
                    assetGo.GetComponent<Image>().sprite = a.assetPortrait;
                    assetGo.GetComponent<Tooltip>().EnableToolTip();
                    assetGo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = a.assetName;
                    assetGo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "" + a.type;
                    assetGo.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + a.text;
                    assetGo.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                }
            }
        }
    }

    public void InvestigatorAdded()
    {
        CreateInvestigatorPoolList();
        CreateSelectedInvestigatorList();

        investigatorPreview.SetActive(false);
        doneButton.SetActive(true);
    }

    public void InvestigatorRemoved()
    {
        CreateInvestigatorPoolList();
        CreateSelectedInvestigatorList();

        if (App.Model.setupModel.selectedInvestigators.Count == 0)
        {
            doneButton.SetActive(false);
        }
    }

    public void CompleteInvestigatorSelection()
    {
        BeginAncientOneSelection();
    }

    public void BeginAncientOneSelection()
    {
        setupMenu.SetActive(true);
        investigatorScreen.SetActive(false);
        ancientOneScreen.SetActive(true);
        ancientOnePreview.SetActive(false);

        CreateAncientOnePoolList();
    }

    private void CreateAncientOnePoolList()
    {
        foreach (Transform child in ancientOneListParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (AncientOne ao in App.Model.setupModel.ancieontOneList)
        {
            GameObject go = Instantiate(investigatorIcon, ancientOneListParent.transform);
            go.GetComponent<Image>().sprite = ao.portrait;
            go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.setupController.SelectAncientOneFromPool(ao); });
        }
    }

    public void AncientOnePreviewUpdated()
    {
        ancientOnePreview.SetActive(true);

        AncientOne ao = App.Model.setupModel.previewedAncientOne;

        ancientOneName.text = ao.ancientOneName;
        ancientOneTitle.text = ao.title;
        ancientOnePortrait.sprite = ao.portrait;

        App.Model.setupModel.SetAncientOnePreviewSide(true);
        previewFront.SetActive(true);
        previewBack.SetActive(false);

        ancientOnePreviewBio.text = ao.flavorText;
        ancientOnePreviewDoomText.text = "" + ao.doom;
        foreach (Transform child in ancientOneTextsParent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ao.ancientOneTexts.Length; i++)
        {
            GameObject go = Instantiate(ancientOneTextBlock, ancientOneTextsParent.transform);
            go.transform.GetChild(0).GetComponent<Text>().text = ao.ancientOneTexts[i];
            if (i == ao.frontReckoningTextIndex)
            {
                go.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                go.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        //also update the backside
        if (ao.awakenText == "")
        {
            awakenEvent.SetActive(false);
        }
        else
        {
            awakenEvent.SetActive(true);
            awakenEventTitle.text = ao.ancientOneName + " Awakens!";
            awakenEventText.text = ao.awakenText;
        }

        finalMysteryTitle.text = ao.finalMysteryTitle;
        finalMysteryFlavorText.text = ao.finalMysteryFlavorText;
        finalMysteryText.text = ao.finalMysteryText;

        flipInfoTitle.text = ao.flipInfoTitle;
        foreach (Transform child in flipInfoTextParent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ao.flipTexts.Length; i++)
        {
            GameObject go = Instantiate(ancientOneTextBlock, flipInfoTextParent.transform);
            go.transform.GetChild(0).GetComponent<Text>().text = ao.flipTexts[i];
            if (i == ao.backReckoningTextIndex)
            {
                go.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                go.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        SetCultistInfo(ao.cultist1);
    }

    public void AncientOneFlipped()
    {
        if (App.Model.setupModel.ancientOnePreviewFront)
        {
            previewFront.SetActive(true);
            previewBack.SetActive(false);
            SetCultistInfo(App.Model.setupModel.previewedAncientOne.cultist1);
        }
        else
        {
            previewFront.SetActive(false);
            previewBack.SetActive(true);
            SetCultistInfo(App.Model.setupModel.previewedAncientOne.cultist2);
        }
    }

    private void SetCultistInfo(Monster c)
    {
        cultistToughness.text = "" + c.toughness;
        if (c.tests[0] == TestStat.None)
        {
            cultistTest1Type.sprite = App.Model.spriteModel.GetTestStatSprite(TestStat.None);
            cultistTest1Mod.text = "";
            cultistHorror.text = "";
            damage1Icon.sprite = App.Model.spriteModel.GetTestStatSprite(TestStat.None);
        }
        else
        {
            cultistTest1Type.sprite = App.Model.spriteModel.GetTestStatSprite(c.tests[0]);
            if (c.testMods[0] == 0)
                cultistTest1Mod.text = "";
            else
                cultistTest1Mod.text = "" + c.testMods[0];
            cultistHorror.text = "" + c.horror;
            damage1Icon.sprite = App.Model.spriteModel.sanitySprite;
        }
        if (c.tests[1] == TestStat.None)
        {
            cultistTest2Type.sprite = App.Model.spriteModel.GetTestStatSprite(TestStat.None);
            cultistTest2Mod.text = "";
            cultistDamage.text = "";
            damage2Icon.sprite = App.Model.spriteModel.GetTestStatSprite(TestStat.None);
        }
        else
        {
            cultistTest2Type.sprite = App.Model.spriteModel.GetTestStatSprite(c.tests[1]);
            if (c.testMods[1] == 0)
                cultistTest2Mod.text = "";
            else
                cultistTest2Mod.text = "" + c.testMods[1];
            cultistDamage.text = "" + c.damage;
            damage2Icon.sprite = App.Model.spriteModel.healthSprite;
        }
        cultistText.text = c.monsterText;
    }

    public void AncientOneSelected()
    {
        setupMenu.SetActive(false);
    }

    public void ReDraftFinished()
    {
        setupMenu.SetActive(false);
    }
}
