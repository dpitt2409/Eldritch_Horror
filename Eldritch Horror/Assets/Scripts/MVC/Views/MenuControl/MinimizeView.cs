using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimizeView : MVC
{
    private Button maximizeButton;


    // Start is called before the first frame update
    void Start()
    {
        maximizeButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        maximizeButton.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.minimizeController.Maximize(); });

        maximizeButton.gameObject.SetActive(false);
    }

    public void WindowMinimized()
    {
        maximizeButton.gameObject.SetActive(true);
    }

    public void WindowMaximized()
    {
        maximizeButton.gameObject.SetActive(false);
    }

    public void HideMaximizeButton()
    {
        maximizeButton.gameObject.SetActive(false);
    }

    public void EnableMaximizeButton()
    {
        maximizeButton.gameObject.SetActive(true);
    }
}
