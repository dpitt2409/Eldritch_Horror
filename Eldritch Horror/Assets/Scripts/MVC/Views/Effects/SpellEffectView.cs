using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellEffectView : MVC
{
    [SerializeField]
    private GameObject flipOption;

    [SerializeField]
    private GameObject dieResult;

    private GameObject spellEffectMenu;
    private GameObject frontEffect;
    private GameObject flipEffect;
    private GameObject minimizeButton;
    private GameObject continueButton;

    private Text frontTitle;
    private Text frontType;
    private Text frontText;

    private Text flipTitle;
    private GameObject flipOptionsParent;
    private GameObject flipDieResultsParent;

    void Start()
    {
        spellEffectMenu = transform.GetChild(0).GetChild(0).gameObject;
        frontEffect = spellEffectMenu.transform.GetChild(0).gameObject;
        flipEffect = spellEffectMenu.transform.GetChild(1).gameObject;
        minimizeButton = spellEffectMenu.transform.GetChild(2).gameObject;
        continueButton = spellEffectMenu.transform.GetChild(3).gameObject;

        frontTitle = frontEffect.transform.GetChild(0).GetComponent<Text>();
        frontType = frontEffect.transform.GetChild(1).GetComponent<Text>();
        frontText = frontEffect.transform.GetChild(2).GetComponent<Text>();

        flipTitle = flipEffect.transform.GetChild(0).GetComponent<Text>();
        flipOptionsParent = flipEffect.transform.GetChild(1).gameObject;
        flipDieResultsParent = flipEffect.transform.GetChild(2).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.spellEffectController.Continue(); });

        spellEffectMenu.SetActive(false);
    }

    public void StartSpellEffect()
    {
        spellEffectMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(spellEffectMenu);
        frontEffect.SetActive(true);
        flipEffect.SetActive(false);
        continueButton.SetActive(false);

        Spell s = App.Model.spellEffectModel.currentSpell;
        frontTitle.text = s.spellName;
        frontType.text = "" + s.type;
        frontText.text = s.text;
    }

    public void SetFrontResultText(string text)
    {
        frontText.text = text;
    }

    public void StartFlipEffect()
    {
        spellEffectMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(spellEffectMenu);
        frontEffect.SetActive(false);
        flipEffect.SetActive(true);
        continueButton.SetActive(true);

        flipTitle.text = App.Model.spellEffectModel.currentSpell.spellName;
        foreach (Transform child in flipOptionsParent.transform)
        {
            Destroy(child.gameObject);
        }
        List<string> options = App.Model.spellEffectModel.currentFlipOptions;
        for (int i = 0; i < options.Count; i++)
        {
            GameObject go = Instantiate(flipOption, flipOptionsParent.transform);
            go.GetComponent<Text>().text = options[i];
            if (i == App.Model.spellEffectModel.activeOption)
            {
                go.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                go.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        foreach (Transform child in flipDieResultsParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (int val in App.Model.spellEffectModel.currentResults)
        {
            GameObject go = Instantiate(dieResult, flipDieResultsParent.transform);
            go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetDiceSprite(val);
        }
    }

    public void SpellEffectFinished()
    {
        continueButton.SetActive(true);
    }

    public void Continue()
    {
        spellEffectMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
