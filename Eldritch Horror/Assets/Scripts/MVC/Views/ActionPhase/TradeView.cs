using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeView : MVC
{
    [SerializeField]
    private GameObject selectInvestigatorButton;
    [SerializeField]
    private GameObject possessionButton;
    [SerializeField]
    private GameObject assetButton;
    [SerializeField]
    private GameObject spellButton;

    private GameObject tradeMenu;
    private GameObject selectInvestigatorScreen;
    private GameObject tradeScreen;
    private GameObject minimizeButton;
    
    private GameObject investigatorOptionsParent;
    private GameObject cancelButton;

    private Image portrait1;
    private GameObject possessionsParent1;
    private Image portrait2;
    private GameObject possessionsParent2;
    private GameObject donebutton;

    void Start()
    {
        tradeMenu = transform.GetChild(0).GetChild(0).gameObject;
        selectInvestigatorScreen = tradeMenu.transform.GetChild(1).gameObject;
        tradeScreen = tradeMenu.transform.GetChild(2).gameObject;
        minimizeButton = tradeMenu.transform.GetChild(3).gameObject;
        
        investigatorOptionsParent = selectInvestigatorScreen.transform.GetChild(1).gameObject;
        cancelButton = selectInvestigatorScreen.transform.GetChild(2).gameObject;

        portrait1 = tradeScreen.transform.GetChild(0).GetComponent<Image>();
        possessionsParent1 = tradeScreen.transform.GetChild(1).gameObject;
        portrait2 = tradeScreen.transform.GetChild(2).GetComponent<Image>();
        possessionsParent2 = tradeScreen.transform.GetChild(3).gameObject;
        donebutton = tradeScreen.transform.GetChild(4).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        cancelButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.CancelAction(); });
        donebutton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.DoneTrading(); });

        tradeMenu.SetActive(false);
    }

    public void TradeActionStarted()
    {
        tradeMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(tradeMenu);
        selectInvestigatorScreen.SetActive(true);
        tradeScreen.SetActive(false);

        foreach (Transform child in investigatorOptionsParent.transform)
        {
            Destroy(child.gameObject);
        }

        Investigator active = App.Model.investigatorModel.activeInvestigator;
        foreach(Investigator i in active.currentLocation.investigatorsOnLocation)
        {
            if (i.investigatorName != active.investigatorName)
            {
                GameObject go = Instantiate(selectInvestigatorButton, investigatorOptionsParent.transform);
                go.GetComponent<Image>().sprite = i.investigatorPortrait;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.BeginTrade(i); });
            }
        }
    }

    public void TradeBegun()
    {
        selectInvestigatorScreen.SetActive(false);
        tradeScreen.SetActive(true);

        Investigator i1 = App.Model.tradeModel.investigator1;
        Investigator i2 = App.Model.tradeModel.investigator2;
        portrait1.sprite = i1.investigatorPortrait;
        portrait2.sprite = i2.investigatorPortrait;

        GeneratePossessionList(i1, possessionsParent1);
        GeneratePossessionList(i2, possessionsParent2);
    }

    private void GeneratePossessionList(Investigator inv, GameObject possessionParent)
    {
        foreach (Transform child in possessionParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Tickets
        for (int i = 0; i < inv.shipTickets; i++)
        {
            GameObject st = Instantiate(possessionButton, possessionParent.transform);
            st.GetComponent<Image>().sprite = App.Model.gameSpritesModel.shipTicketSprite;
            st.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.TradeShipTicket(inv); });
        }
        for (int i = 0; i < inv.trainTickets; i++)
        {
            GameObject st = Instantiate(possessionButton, possessionParent.transform);
            st.GetComponent<Image>().sprite = App.Model.gameSpritesModel.trainTicketSprite;
            st.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.TradeTrainTicket(inv); });
        }

        // Focus Tokens
        for (int i = 0; i < inv.focusTokens; i++)
        {
            GameObject st = Instantiate(possessionButton, possessionParent.transform);
            st.GetComponent<Image>().sprite = App.Model.gameSpritesModel.focusTokenSprite;
            st.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.TradeFocusToken(inv); });
        }

        // Clues
        foreach(Clue c in inv.clues)
        {
            GameObject clue = Instantiate(possessionButton, possessionParent.transform);
            clue.GetComponent<Image>().sprite = App.Model.gameSpritesModel.clueTokenSprite;
            clue.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.TradeClue(c, inv); });
        }

        // Spells
        foreach (Spell s in inv.spells)
        {
            GameObject spell = Instantiate(spellButton, possessionParent.transform);
            spell.GetComponent<Image>().sprite = s.spellPortrait;
            spell.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = s.spellName;
            spell.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "" + s.type;
            spell.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + s.text;
            if (s.reckoningText == "")
            {
                spell.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                spell.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                spell.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = s.reckoningText;
            }
            spell.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.TradeSpell(s, inv); });
        }

        // Assets
        foreach (Asset a in inv.assets)
        {
            GameObject asset = Instantiate(assetButton, possessionParent.transform);
            asset.GetComponent<Image>().sprite = a.assetPortrait;
            // Set tooltip
            asset.GetComponent<Tooltip>().EnableToolTip();
            asset.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = a.assetName;
            asset.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "" + a.type;
            asset.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + a.text;
            asset.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            if (a.reckoningText == "")
            {
                asset.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                asset.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = a.reckoningText;
            }
            asset.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.tradeController.TradeAsset(a, inv); });
        }
    }

    public void TradeMade()
    {
        GeneratePossessionList(App.Model.tradeModel.investigator1, possessionsParent1);
        GeneratePossessionList(App.Model.tradeModel.investigator2, possessionsParent2);
    }

    public void DoneTrading()
    {
        tradeMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }

    public void TradeActionCanceled()
    {
        tradeMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
