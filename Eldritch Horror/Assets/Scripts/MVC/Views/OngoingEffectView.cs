using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OngoingEffectView : MVC
{
    [SerializeField]
    private GameObject ongoingEffectObject;

    private GameObject ongoingEffectsIcon;
    private GameObject ongoingEffectsList;

    void Start()
    {
        ongoingEffectsIcon = transform.GetChild(0).GetChild(0).gameObject;
        ongoingEffectsList = ongoingEffectsIcon.transform.GetChild(0).gameObject;
        
        ongoingEffectsIcon.GetComponent<Button>().onClick.AddListener(delegate { App.Controller.ongoingEffectController.ToggleOngoingEffectList(); });

        ongoingEffectsIcon.SetActive(false);
        ongoingEffectsList.SetActive(false);
    }

    public void HideOngoingEffectUI()
    {
        ongoingEffectsIcon.SetActive(false);
    }

    public void EnableOngoingEffectUI()
    {
        ongoingEffectsIcon.SetActive(true);
    }

    public void OngoingEffectsChanged()
    {
        if (App.Model.ongoingEffectModel.activeOngoingEffects.Count > 0)
        {
            ongoingEffectsIcon.SetActive(true);
        }
        else
        {
            ongoingEffectsIcon.SetActive(false);
        }

        foreach (Transform child in ongoingEffectsList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (OngoingEffect oe in App.Model.ongoingEffectModel.activeOngoingEffects)
        {
            GameObject go = Instantiate(ongoingEffectObject, ongoingEffectsList.transform);
            go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = oe.effectTitle;

            if (oe.eldritchTokens == 0)
            {
                go.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                go.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                go.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "" + oe.eldritchTokens;
            }

            go.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = oe.location;
            go.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = oe.effectText;

            if (oe.reckoningText == "")
            {
                go.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                go.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                go.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = oe.reckoningText;
            }
        }
    }

    public void ToggleOngoingEffectList()
    {
        if (ongoingEffectsList.activeSelf)
        {
            App.Controller.openMenuController.ClosePopup();
            ongoingEffectsList.SetActive(false);
        }
        else
        {
            App.Controller.openMenuController.OpenPopup(ongoingEffectsList);
            ongoingEffectsList.SetActive(true);
        }
    }
}
