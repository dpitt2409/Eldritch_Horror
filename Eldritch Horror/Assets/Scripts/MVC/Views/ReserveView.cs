using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReserveView : MVC
{
    private GameObject reserveIcon;

    private GameObject reserveList;

    private GameObject[] reserveAssets;

    // Start is called before the first frame update
    void Start()
    {
        reserveIcon = transform.GetChild(0).GetChild(0).gameObject;
        reserveList = reserveIcon.transform.GetChild(0).gameObject;
        reserveAssets = new GameObject[4];
        reserveAssets[0] = reserveList.transform.GetChild(0).gameObject;
        reserveAssets[1] = reserveList.transform.GetChild(1).gameObject;
        reserveAssets[2] = reserveList.transform.GetChild(2).gameObject;
        reserveAssets[3] = reserveList.transform.GetChild(3).gameObject;

        reserveIcon.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.reserveController.ToggleReserveList(); });

        reserveIcon.SetActive(false);
        reserveList.SetActive(false);
    }

    public void EnableReserve()
    {
        reserveIcon.SetActive(true);
    }

    public void HideReserve()
    {
        reserveIcon.SetActive(false);
    }

    public void ToggleReserveList()
    {
        reserveList.SetActive(!reserveList.activeSelf);
    }

    public void ReserveUpdated()
    {
        for (int i = 0; i < 4; i++)
        {
            Asset a = App.Model.reserveModel.reserveAssets[i];
            if (a == null)
            {
                reserveAssets[i].GetComponent<Image>().sprite = App.Model.spriteModel.blankIcon;
                reserveAssets[i].transform.GetChild(1).gameObject.SetActive(false); //Disable cost
                reserveAssets[i].GetComponent<Tooltip>().DisableToolTip();
            }
            else
            {
                GameObject asset = reserveAssets[i];
                asset.GetComponent<Image>().sprite = a.assetPortrait;
                asset.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "" + a.cost;
                // Set tooltip
                reserveAssets[i].GetComponent<Tooltip>().EnableToolTip();
                asset.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = a.assetName;
                asset.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "" + a.type;
                asset.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + a.text;
                asset.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = "" + a.cost;
            }
        }
    }
}
