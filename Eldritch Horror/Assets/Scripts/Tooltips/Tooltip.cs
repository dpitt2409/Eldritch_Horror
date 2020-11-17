using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject toolTip;
    private bool toolTipEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        toolTip = transform.GetChild(0).gameObject;
        toolTip.SetActive(false);
        toolTipEnabled = true;
    }

    public void EnableToolTip()
    {
        toolTipEnabled = true;
    }

    public void DisableToolTip()
    {
        toolTipEnabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toolTipEnabled)
        {
            toolTip.SetActive(true);  
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
    }

}
