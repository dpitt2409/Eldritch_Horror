using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngoingEffectModel : MVC
{
    public List<OngoingEffect> activeOngoingEffects;

    void Start()
    {
        activeOngoingEffects = new List<OngoingEffect>();
    }

    public void NewOngoingEffect(OngoingEffect oe)
    {
        activeOngoingEffects.Add(oe);
        App.View.ongoingEffectView.OngoingEffectsChanged();
    }

    public void OngoingEffectFinished(OngoingEffect oe)
    {
        activeOngoingEffects.Remove(oe);
        App.View.ongoingEffectView.OngoingEffectsChanged();
    }
}
