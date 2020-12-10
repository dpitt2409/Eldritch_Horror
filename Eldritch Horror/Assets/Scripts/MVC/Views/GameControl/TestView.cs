using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestView : MVC
{
    [SerializeField]
    private Sprite[] diceSprites;

    [SerializeField]
    private GameObject dieResult;

    [SerializeField]
    private GameObject useableToken;

    [SerializeField]
    private GameObject useableAsset;

    private GameObject testMenu;
    private GameObject rollDiceButton;
    private Text rollDiceButtonText;
    private GameObject diceResultsParent;
    private GameObject useableItemsParent;
    private GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        testMenu = transform.GetChild(0).GetChild(0).gameObject;
        rollDiceButton = testMenu.transform.GetChild(0).gameObject;
        rollDiceButtonText = rollDiceButton.GetComponentInChildren<Text>();
        diceResultsParent = testMenu.transform.GetChild(1).gameObject;
        useableItemsParent = testMenu.transform.GetChild(2).gameObject;
        continueButton = testMenu.transform.GetChild(3).gameObject;

        rollDiceButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.testController.RollDice(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.testController.Continue(); });

        testMenu.SetActive(false);
    }

    public void PreTest()
    {
        foreach (Transform child in useableItemsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void StartTest()
    {
        testMenu.SetActive(true);
        App.Controller.openMenuController.TestStarted();
        rollDiceButton.SetActive(true);
        diceResultsParent.SetActive(false);
        continueButton.SetActive(false);
        SetRollButtonText();
    }

    private void SetRollButtonText()
    {
        Investigator active = App.Model.testModel.testingInvestigator;
        int val = active.CheckStat(App.Model.testModel.activeTestStat) + active.CheckMod(App.Model.testModel.activeTestStat);
        int charMod = App.Model.testModel.currentBonus + App.Model.testModel.currentAdditionalDie;
        int testMod = App.Model.testModel.activeTestMod;
        int total = App.Model.testModel.currentNumRolls;
        
        string buttonText;
        if (charMod == 0 && testMod == 0)
            buttonText = "Test " + App.Model.testModel.activeTestStat + ": " + total;
        else if (charMod == 0)
            buttonText = "Test " + App.Model.testModel.activeTestStat + ": " + total + " (" + val + " + " + testMod + ")";
        else if (testMod == 0)
            buttonText = "Test " + App.Model.testModel.activeTestStat + ": " + total + " (" + val + " + " + charMod + ")";
        else
            buttonText = "Test " + App.Model.testModel.activeTestStat + ": " + total + " (" + val + " + " + charMod + " + " + testMod + ")";

        rollDiceButtonText.text = buttonText;
    }

    public void DiceRolled()
    {
        foreach (Transform child in useableItemsParent.transform)
        {
            Destroy(child.gameObject);
        }
        rollDiceButton.SetActive(false);
        diceResultsParent.SetActive(true);
        DiceUpdated();
    }

    public void DiceUpdated()
    {
        foreach (Transform child in diceResultsParent.transform)
        {
            Destroy(child.gameObject);
        }
        List<int> dice = App.Model.testModel.currentResults;
        for (int i = 0; i < dice.Count; i++)
        {
            GameObject go = Instantiate(dieResult, diceResultsParent.transform);
            go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetDiceSprite(dice[i]);
            int index = i;
            go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.testController.RerollDie(index); }); 
        }
    }

    public void AddUseableAsset(Asset a, UseableItemCallBack callback)
    {
        GameObject go = Instantiate(useableAsset, useableItemsParent.transform);
        go.GetComponent<Image>().sprite = a.assetPortrait;
        // Set tooltip
        go.GetComponent<Tooltip>().EnableToolTip();
        go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = a.assetName;
        go.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "" + a.type;
        go.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + a.text;
        go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.testController.UseAsset(go, callback); });
    }

    public void PostTestEventComplete()
    {
        Investigator inv = App.Model.testModel.testingInvestigator;
        for (int i = 0; i < inv.focusTokens; i++)
        {
            GameObject go = Instantiate(useableToken, useableItemsParent.transform);
            go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.focusTokenSprite;
            go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Focus Token";
            go.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Reroll 1 Die";
            go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.testController.UseFocusToken(go); });
        }
        foreach(Clue c in inv.clues)
        {
            GameObject go = Instantiate(useableToken, useableItemsParent.transform);
            go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.clueTokenSprite;
            go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Clue Token";
            go.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Reroll 1 Die";
            go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.testController.UseClueToken(go); });
        }

        continueButton.SetActive(true);
    }

    public void ItemUsed(GameObject go)
    {
        Destroy(go);
    }

    public void SingleRollTestStarted()
    {
        testMenu.SetActive(true);
        App.Controller.openMenuController.TestStarted();
        rollDiceButton.SetActive(true);
        diceResultsParent.SetActive(false);
        continueButton.SetActive(false);
        rollDiceButtonText.text = "Roll 1 Die";

        foreach (Transform child in useableItemsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SingleDieRolled()
    {
        rollDiceButton.SetActive(false);
        diceResultsParent.SetActive(true);
        DiceUpdated();
        continueButton.SetActive(true);
    }

    public void Continue()
    {
        testMenu.SetActive(false);
        App.Controller.openMenuController.TestFinished();
    }

    public void MinimizeTest()
    {
        testMenu.SetActive(false);
    }

    public void MaximizeTest()
    {
        testMenu.SetActive(true);
    }
}
