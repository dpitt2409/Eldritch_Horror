using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewedInvestigatorView : MVC
{
    private Image portrait;

    private Text healthText;
    private Text sanityText;
    private Text strengthText;
    private Text willText;
    private Text influenceText;
    private Text observationText;
    private Text loreText;

    private Text clueTokensText;
    private Text shipTokensText;
    private Text trainTokensText;
    private Text focusTokensText;

    private GameObject itemsParent;
    private GameObject conditionsParent;
    [SerializeField]
    private GameObject assetPossession;
    [SerializeField]
    private GameObject spellPossession;
    [SerializeField]
    private GameObject conditionPossession;

    private GameObject expandInvestigatorListButton;

    [SerializeField]
    private GameObject selectInvestigatorPrefab;
    private GameObject investigatorUI;
    private GameObject investigatorList;
    private GameObject listParent;

    void Awake()
    {
        investigatorUI = transform.GetChild(0).GetChild(0).gameObject;
        Transform statsParent = investigatorUI.transform.GetChild(1);
        Transform possessionsParent = investigatorUI.transform.GetChild(2);

        portrait = investigatorUI.transform.GetChild(0).GetComponent<Image>();

        healthText = statsParent.GetChild(0).GetChild(0).GetComponent<Text>();
        sanityText = statsParent.GetChild(1).GetChild(0).GetComponent<Text>();
        strengthText = statsParent.GetChild(2).GetChild(0).GetComponent<Text>();
        willText = statsParent.GetChild(3).GetChild(0).GetComponent<Text>();
        influenceText = statsParent.GetChild(4).GetChild(0).GetComponent<Text>();
        observationText = statsParent.GetChild(5).GetChild(0).GetComponent<Text>();
        loreText = statsParent.GetChild(6).GetChild(0).GetComponent<Text>();

        clueTokensText = possessionsParent.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        shipTokensText = possessionsParent.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        trainTokensText = possessionsParent.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        focusTokensText = possessionsParent.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();

        itemsParent = possessionsParent.GetChild(1).GetChild(1).gameObject;
        conditionsParent = possessionsParent.GetChild(2).GetChild(1).gameObject;

        expandInvestigatorListButton = investigatorUI.transform.GetChild(3).gameObject;

        investigatorList = transform.GetChild(0).GetChild(1).gameObject;
        listParent = investigatorList.transform.GetChild(1).gameObject;

        expandInvestigatorListButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.previewedInvestigatorController.TogglePreviewedInvestigatorList(); });

        investigatorUI.SetActive(false);
        investigatorList.SetActive(false);
    }

    public void InvestigatorUIEnabled()
    {
        investigatorUI.SetActive(true);
    }

    public void HideInvestigatorUI()
    {
        investigatorUI.SetActive(false);
    }

    public void PreviewedInvestigatorUpdated()
    {
        Investigator preview = App.Model.previewedInvestigatorModel.previewedInvestigator;

        portrait.sprite = preview.investigatorPortrait;
        healthText.text = preview.currentHealth + " / " + preview.health;
        sanityText.text = preview.currentSanity + " / " + preview.sanity;
        strengthText.text = "" + (preview.strength + preview.strengthMod);
        willText.text = "" + (preview.will + preview.willMod);
        influenceText.text = "" + (preview.influence + preview.influenceMod);
        observationText.text = "" + (preview.observation + preview.observationMod);
        loreText.text = "" + (preview.lore + preview.loreMod);

        clueTokensText.text = "" + preview.clues.Count;
        shipTokensText.text = "" + preview.shipTickets;
        trainTokensText.text = "" + preview.trainTickets;
        focusTokensText.text = "" + preview.focusTokens;

        foreach (Transform child in itemsParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Asset a in preview.assets)
        {
            GameObject assetGo = Instantiate(assetPossession, itemsParent.transform);
            assetGo.GetComponent<Image>().sprite = a.assetPortrait;
            assetGo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = a.assetName;
            string assetType = "" + a.type;
            if (a.magical || a.subTypes.Length > 0)
            {
                assetType += " - ";
                if (a.magical)
                    assetType += "Magical ";
                foreach (AssetSubType subType in a.subTypes)
                    assetType += "" + subType + " ";
            }
            assetGo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = assetType;
            assetGo.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + a.text;
            if (a.artifact)
            {
                assetGo.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                assetGo.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "" + a.cost;
            }
            if (a.reckoningText == "")
            {
                assetGo.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                assetGo.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = a.reckoningText;
            }
        }
        foreach (Spell s in preview.spells)
        {
            GameObject spellGo = Instantiate(spellPossession, itemsParent.transform);
            spellGo.GetComponent<Image>().sprite = s.spellPortrait;
            spellGo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = s.spellName;
            spellGo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "" + s.type;
            spellGo.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + s.text;
            if (s.reckoningText == "")
            {
                spellGo.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                spellGo.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                spellGo.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = s.reckoningText;
            }
        }

        foreach (Transform child in conditionsParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Condition c in preview.conditions)
        {
            GameObject conditionGo = Instantiate(conditionPossession, conditionsParent.transform);
            conditionGo.GetComponent<Image>().sprite = c.conditionPortrait;
            conditionGo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = c.conditionName;
            conditionGo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "" + c.type;
            conditionGo.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = c.conditionText;
            if (c.reckoningText == "")
            {
                conditionGo.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                conditionGo.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = c.reckoningText;
            }
        }

        investigatorList.SetActive(false);
    }

    public void TogglePreviewedInvestigatorList()
    {
        if (investigatorList.activeSelf)
        {
            investigatorList.SetActive(false);
        }
        else
        {
            investigatorList.SetActive(true);
            foreach (Transform child in listParent.transform)
            {
                Destroy(child.gameObject);
            }

            List<Investigator> list = App.Model.investigatorModel.investigators;
            foreach (Investigator inv in list)
            {
                GameObject go = Instantiate(selectInvestigatorPrefab.gameObject, listParent.transform);
                go.GetComponent<Image>().sprite = inv.investigatorPortrait;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.previewedInvestigatorController.SelectPreviewedInvestigatorFromList(inv); });

                if (inv.investigatorName == App.Model.investigatorModel.activeInvestigator.investigatorName)
                {
                    go.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    go.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}
