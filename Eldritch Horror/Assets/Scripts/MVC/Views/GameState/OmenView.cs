using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenView : MVC
{
    private GameObject omenUI;
    private GameObject[] omenHighlights;

    // Start is called before the first frame update
    void Awake()
    {
        omenUI = transform.GetChild(0).GetChild(0).gameObject;
        omenHighlights = new GameObject[4];
        omenHighlights[0] = omenUI.transform.GetChild(0).GetChild(0).gameObject;
        omenHighlights[1] = omenUI.transform.GetChild(1).GetChild(0).gameObject;
        omenHighlights[2] = omenUI.transform.GetChild(2).GetChild(0).gameObject;
        omenHighlights[3] = omenUI.transform.GetChild(3).GetChild(0).gameObject;

        omenUI.SetActive(false);
    }

    public void OmenUIEnabled()
    {
        omenUI.SetActive(true);
    }

    public void HideOmenUI()
    {
        omenUI.SetActive(false);
    }

    public void OmenUpdated()
    {
        for (int i = 0; i < omenHighlights.Length; i++)
        {
            if (i == App.Model.omenModel.currentOmen)
            {
                omenHighlights[i].SetActive(true);
            }
            else
            {
                omenHighlights[i].SetActive(false);
            }
        }
    }
}
