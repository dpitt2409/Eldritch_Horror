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
        foreach (OngoingEffect o in activeOngoingEffects)
        {
            if (o.effectTitle == oe.effectTitle)
                return;
        }
        activeOngoingEffects.Add(oe);
        oe.Spawned();
        App.View.ongoingEffectView.OngoingEffectsChanged();
    }

    public void OngoingEffectFinished(OngoingEffect oe)
    {
        activeOngoingEffects.Remove(oe);
        oe.Resolved();
        App.View.ongoingEffectView.OngoingEffectsChanged();
    }
}
