using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientOneModel : MVC
{
    public AncientOne ancientOne;

    public void SetAncientOne(AncientOne ao)
    {
        ancientOne = ao;
        App.View.ancientOneView.AncientOneSet();
    }
}
