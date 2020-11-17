using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareView : MVC
{
    private GameObject prepareMenu;
    private GameObject shipButton;
    private GameObject trainButton;
    private GameObject minimizeButton;
    private GameObject cancelButton;

    void Start()
    {
        prepareMenu = transform.GetChild(0).GetChild(0).gameObject;
        shipButton = prepareMenu.transform.GetChild(1).gameObject;
        trainButton = prepareMenu.transform.GetChild(2).gameObject;
        minimizeButton = prepareMenu.transform.GetChild(3).gameObject;
        cancelButton = prepareMenu.transform.GetChild(4).gameObject;

        shipButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.prepareController.PrepareTicket(true); });
        trainButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.prepareController.PrepareTicket(false); });
        minimizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        cancelButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.prepareController.CancelAction(); });

        prepareMenu.SetActive(false);
    }

    public void PrepareActionStarted()
    {
        prepareMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(prepareMenu);
        bool canPrepareShipTicket = false;
        bool canPrepareTrainTicket = false;

        foreach (Connection c in App.Model.investigatorModel.activeInvestigator.currentLocation.connections)
        {
            if (c.type == ConnectionType.Ship)
            {
                canPrepareShipTicket = true;
            }
            if (c.type == ConnectionType.Train)
            {
                canPrepareTrainTicket = true;
            }
        }

        shipButton.SetActive(canPrepareShipTicket);
        trainButton.SetActive(canPrepareTrainTicket);
    }

    public void TicketPrepared()
    {
        prepareMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }

    public void PrepareActionCanceled()
    {
        prepareMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
