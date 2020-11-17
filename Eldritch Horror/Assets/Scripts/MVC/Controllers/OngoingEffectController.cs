using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngoingEffectController : MVC
{
    public void NewOngoingEffect(OngoingEffect oe)
    {
        App.Model.ongoingEffectModel.NewOngoingEffect(oe);
    }

    public void ToggleOngoingEffectList()
    {
        App.View.ongoingEffectView.ToggleOngoingEffectList();
    }
}
