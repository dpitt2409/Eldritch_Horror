using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ConditionEffectCallBack();

public class ConditionEffectModel : MVC
{
    public Condition currentCondition;
    public ConditionEffectCallBack currentCallBack;

    public void StartConditionEffect(Condition c, ConditionEffectCallBack callback)
    {
        currentCondition = c;
        currentCallBack = callback;
        App.View.conditionEffectView.StartConditionEffect();
    }

    public void FinishConditionEffect()
    {
        currentCondition = null;
        currentCallBack = null;
        App.View.conditionEffectView.Continue();
    }

    public void StartFlipEffect(ConditionEffectCallBack callback)
    {
        currentCallBack = callback;
    }
}
