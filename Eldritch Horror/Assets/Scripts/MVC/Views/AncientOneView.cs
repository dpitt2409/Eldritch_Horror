using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AncientOneView : MVC
{
    public Image ancientOnePortrait;

    // Start is called before the first frame update
    void Awake()
    {
        ancientOnePortrait = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        ancientOnePortrait.gameObject.SetActive(false);
    }

    public void AncientOneSet()
    {
        ancientOnePortrait.sprite = App.Model.ancientOneModel.ancientOne.portrait;
        ancientOnePortrait.gameObject.SetActive(true);
    }

    public void EnableAncientOne()
    {
        ancientOnePortrait.gameObject.SetActive(true);
    }

    public void HideAncientOne()
    {
        ancientOnePortrait.gameObject.SetActive(false);
    }
}
