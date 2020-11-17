using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquireView : MVC
{
    private GameObject acquireMenu;
    private Text menuText;
    private GameObject[] assets;
    private GameObject startTestButton;
    private GameObject debtButton;
    private GameObject minimizeButton;
    private GameObject discardButton;
    private GameObject cancelButton;
    private GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        acquireMenu = transform.GetChild(0).GetChild(0).gameObject;
        menuText = acquireMenu.transform.GetChild(1).GetComponent<Text>();
        assets = new GameObject[4];
        assets[0] = acquireMenu.transform.GetChild(2).GetChild(0).gameObject;
        assets[1] = acquireMenu.transform.GetChild(2).GetChild(1).gameObject;
        assets[2] = acquireMenu.transform.GetChild(2).GetChild(2).gameObject;
        assets[3] = acquireMenu.transform.GetChild(2).GetChild(3).gameObject;
        startTestButton = acquireMenu.transform.GetChild(3).gameObject;
        debtButton = acquireMenu.transform.GetChild(4).gameObject;
        minimizeButton = acquireMenu.transform.GetChild(5).gameObject;
        discardButton = acquireMenu.transform.GetChild(6).gameObject;
        cancelButton = acquireMenu.transform.GetChild(7).gameObject;
        continueButton = acquireMenu.transform.GetChild(8).gameObject;

        startTestButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.StartInfluenceTest(); });
        debtButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.TakeDebt(); });
        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        discardButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.SetDiscarding(); });
        cancelButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.CancelAcquireAction(); });
        continueButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.Continue(); });
        assets[0].GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.BuyAsset(0); });
        assets[1].GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.BuyAsset(1); });
        assets[2].GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.BuyAsset(2); });
        assets[3].GetComponent<Button>().onClick.AddListener(delegate { App.Controller.acquireController.BuyAsset(3); });

        acquireMenu.SetActive(false);
    }

    public void AcquireActionStarted()
    {
        acquireMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(acquireMenu);
        cancelButton.SetActive(true);
        startTestButton.SetActive(true);
        continueButton.SetActive(false);
        debtButton.SetActive(false);
        discardButton.SetActive(false);

        UpdateAssets();

        menuText.text = "Test Influence to purchase Assets";
    }

    public void UpdateAssets()
    {
        menuText.text = "Remaining Points: " + App.Model.acquireModel.remainingPoints;
        for (int i = 0; i < 4; i++)
        {
            Asset a = App.Model.reserveModel.reserveAssets[i];
            if (a == null)
            {
                assets[i].GetComponent<Image>().sprite = App.Model.spriteModel.blankIcon;
                assets[i].transform.GetChild(1).gameObject.SetActive(false); //Disable cost
                assets[i].GetComponent<Tooltip>().DisableToolTip();
            }
            else
            {
                GameObject asset = assets[i];
                asset.GetComponent<Image>().sprite = a.assetPortrait;
                asset.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "" + a.cost;
                // Set tooltip
                asset.GetComponent<Tooltip>().EnableToolTip();
                asset.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = a.assetName;
                asset.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "" + a.type;
                asset.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + a.text;
                asset.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "" + a.cost;
            }
        }
    }
    
    public void StartInfluenceTest()
    {
        cancelButton.SetActive(false);
        startTestButton.SetActive(false);
    }

    public void InfluenceTested()
    {
        menuText.text = "Remaining Points: " + App.Model.acquireModel.remainingPoints;
        
        continueButton.SetActive(true);
        discardButton.SetActive(true);

        bool hasDebt = false;
        foreach (Condition c in App.Model.investigatorModel.activeInvestigator.conditions)
        {
            if (c.conditionName == "Debt")
            {
                hasDebt = true;
            }
        }
        debtButton.SetActive(!hasDebt);
    }

    public void DebtTaken()
    {
        debtButton.SetActive(false);
        menuText.text = "Remaining Points: " + App.Model.acquireModel.remainingPoints;
    }

    public void HasBought()
    {
        discardButton.SetActive(false);
    }

    public void Discarding()
    {
        discardButton.SetActive(false);
        debtButton.SetActive(false);
        menuText.text = "Select an Asset to discard";
    }

    public void Done()
    {
        acquireMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
