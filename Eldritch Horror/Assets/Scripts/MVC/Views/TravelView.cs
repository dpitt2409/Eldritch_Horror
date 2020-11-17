using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelView : MVC
{
    [SerializeField]
    private GameObject shipPathButton;
    [SerializeField]
    private GameObject trainPathButton;
    [SerializeField]
    private GameObject unchartedPathButton;

    private GameObject travelMenu;
    private GameObject travelOptionsParent;
    private GameObject minimizeTravelButton;
    private GameObject cancelButton;

    private GameObject useTicketMenu;
    private GameObject ticketOptionsParent;
    private GameObject minimizeTicketButton;

    void Start()
    {
        travelMenu = transform.GetChild(0).GetChild(0).gameObject;
        travelOptionsParent = travelMenu.transform.GetChild(1).gameObject;
        minimizeTravelButton = travelMenu.transform.GetChild(2).gameObject;
        cancelButton = travelMenu.transform.GetChild(3).gameObject;

        useTicketMenu = transform.GetChild(0).GetChild(1).gameObject;
        ticketOptionsParent = useTicketMenu.transform.GetChild(1).gameObject;
        minimizeTicketButton = useTicketMenu.transform.GetChild(2).gameObject;

        minimizeTravelButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });
        cancelButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.travelController.CancelAction(); });
        minimizeTicketButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.openMenuController.MinimizeOpenMenu(); });

        travelMenu.SetActive(false);
        useTicketMenu.SetActive(false);
    }

    public void TravelActionStarted()
    {
        travelMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(travelMenu);

        foreach (Transform child in travelOptionsParent.transform)
        {
            Destroy(child.gameObject);
        }

        Connection[] paths = App.Model.investigatorModel.activeInvestigator.currentLocation.connections;
        foreach(Connection c in paths)
        {
            GameObject go;
            if (c.type == ConnectionType.Ship)
            {
                go = Instantiate(shipPathButton, travelOptionsParent.transform);
                go.GetComponentInChildren<Text>().text = "Travel by Ship to " + c.destination.locationName;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.travelController.TravelByPath(c); });
            }
            if (c.type == ConnectionType.Train)
            {
                go = Instantiate(trainPathButton, travelOptionsParent.transform);
                go.GetComponentInChildren<Text>().text = "Travel by Train to " + c.destination.locationName;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.travelController.TravelByPath(c); });
            }
            if (c.type == ConnectionType.Uncharted)
            {
                go = Instantiate(unchartedPathButton, travelOptionsParent.transform);
                go.GetComponentInChildren<Text>().text = "Travel on an Uncharted path to " + c.destination.locationName;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.travelController.TravelByPath(c); });
            }
        }
    }

    public void PathSelected()
    {
        travelMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }

    public void OpenTicketMenu()
    {
        useTicketMenu.SetActive(true);
        App.Controller.openMenuController.OpenMenu(useTicketMenu);

        Investigator active = App.Model.investigatorModel.activeInvestigator;

        foreach (Transform child in ticketOptionsParent.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject noGo = Instantiate(trainPathButton, ticketOptionsParent.transform);
        noGo.GetComponentInChildren<Text>().text = "Do not use Ticket";
        noGo.GetComponent<Image>().sprite = null;
        noGo.GetComponent<Image>().color = new Color(0, 0, 0, 255);
        noGo.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.travelController.TicketNotUsed(); });

        Connection[] paths = active.currentLocation.connections;
        foreach (Connection c in paths)
        {
            GameObject go;
            if (c.type == ConnectionType.Ship && active.shipTickets > 0)
            {
                go = Instantiate(shipPathButton, ticketOptionsParent.transform);
                go.GetComponentInChildren<Text>().text = "Use Ship ticket to " + c.destination.locationName;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.travelController.UseTicket(c); });
            }
            else if (c.type == ConnectionType.Train && active.trainTickets > 0)
            {
                go = Instantiate(trainPathButton, ticketOptionsParent.transform);
                go.GetComponentInChildren<Text>().text = "Use Train ticket to " + c.destination.locationName;
                go.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.travelController.UseTicket(c); });
            }
        }
    }

    public void TicketUsed()
    {
        useTicketMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }

    public void TravelActionCanceled()
    {
        travelMenu.SetActive(false);
        App.Controller.openMenuController.CloseMenu();
    }
}
