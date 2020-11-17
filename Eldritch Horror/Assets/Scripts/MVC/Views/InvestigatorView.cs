using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigatorView : MVC
{
    [SerializeField]
    private GameObject selectInvestigatorPrefab;
    private GameObject selectInvestigatorMenu;
    private GameObject investigatorListParent;
    private GameObject minimizeButton;

    void Start()
    {
        selectInvestigatorMenu = transform.GetChild(0).GetChild(0).gameObject;
        investigatorListParent = selectInvestigatorMenu.transform.GetChild(1).gameObject;
        minimizeButton = selectInvestigatorMenu.transform.GetChild(2).gameObject;

        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });

        selectInvestigatorMenu.SetActive(false);
    }

    public void EnableSelectInvestigatorMenu()
    {
        selectInvestigatorMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(selectInvestigatorMenu);

        foreach (Transform child in investigatorListParent.transform)
        {
            Destroy(child.gameObject);
        }

        List<Investigator> list = App.Model.investigatorModel.investigators;
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(selectInvestigatorPrefab.gameObject, investigatorListParent.transform);
            go.GetComponent<Image>().sprite = list[i].investigatorPortrait;
            int copy = i;
            go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.investigatorController.SelectLeadInvestigator(copy); });
        }
    }

    public void DisableSelectInvestigatorMenu()
    {
        selectInvestigatorMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
