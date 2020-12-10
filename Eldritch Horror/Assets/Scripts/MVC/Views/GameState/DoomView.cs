using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoomView : MVC
{
    private GameObject doomUI;

    private Text doomText;


    // Start is called before the first frame update
    void Awake()
    {
        doomUI = transform.GetChild(0).GetChild(0).gameObject;
        doomText = doomUI.transform.GetChild(0).GetComponent<Text>();

        doomUI.SetActive(false);
    }

    public void DoomUIEnabled()
    {
        doomUI.SetActive(true);
    }

    public void HideDoomUI()
    {
        doomUI.SetActive(false);
    }

    public void DoomUpdated()
    {
        doomText.text = "" + App.Model.doomModel.currentDoom;
    }

}
