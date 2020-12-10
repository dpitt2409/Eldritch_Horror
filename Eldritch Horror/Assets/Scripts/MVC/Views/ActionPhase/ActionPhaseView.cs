using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ActionCallBack();
public class ActionPhaseView : MVC
{
    [SerializeField]
    private GameObject actionButtonPrefab;

    private GameObject actionMenu;
    private Text actionsTakenText;
    private GameObject actionsListParent;
    private GameObject endTurnButton;
    private GameObject minimizeButton;

    public void Start()
    {
        actionMenu = transform.GetChild(0).GetChild(0).gameObject;
        actionsTakenText = actionMenu.transform.GetChild(1).GetComponent<Text>();
        actionsListParent = actionMenu.transform.GetChild(2).gameObject;
        endTurnButton = actionMenu.transform.GetChild(3).gameObject;
        minimizeButton = actionMenu.transform.GetChild(4).gameObject;

        endTurnButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.actionPhaseController.EndTurn(); });
        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });

        actionMenu.SetActive(false);
    }

    public void ActionTurnStarted()
    {
        actionMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(actionMenu);
        actionsTakenText.text = App.Model.actionPhaseModel.actionsThisturn + " / 2";

        UpdateActionList();
    }

    private void UpdateActionList()
    {
        foreach (Transform child in actionsListParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(BasicActions action in App.Model.investigatorModel.activeInvestigator.AvailableActions())
        {
            GameObject go = Instantiate(actionButtonPrefab.gameObject, actionsListParent.transform);
            go.GetComponentInChildren<Text>().text = "" + action;

            go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.actionPhaseController.PerformBasicAction(action); });
        }

        App.Model.eventModel.ActionListEvent();
    }

    public void ActionAddedToList(string text, ActionCallBack callback)
    {
        GameObject go = Instantiate(actionButtonPrefab.gameObject, actionsListParent.transform);
        go.GetComponentInChildren<Text>().text = text;
        go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.actionPhaseController.PerformCustomAction(callback); });
    }

    public void ActionSelected()
    {
        actionMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }

    public void ActionCanceled()
    {
        actionMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(actionMenu);
    }
}
