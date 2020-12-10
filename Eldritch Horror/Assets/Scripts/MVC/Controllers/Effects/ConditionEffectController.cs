using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEffectController : MVC
{
    public void StartConditionEffect(Condition c, ConditionEffectCallBack callback)
    {
        App.Model.conditionEffectModel.StartConditionEffect(c, callback);
    }

    public void SetResultText(string text)
    {
        App.View.conditionEffectView.SetResultText(text);
    }

    public void ConditionEffectFinished()
    {
        App.View.conditionEffectView.EffectFinished();
    }

    public void StartFlipEffect(string title, string text, ConditionEffectCallBack callback)
    {
        App.Model.conditionEffectModel.StartFlipEffect(callback);
        App.View.conditionEffectView.StartFlipEffect(title, text);
    }

    public void Continue()
    {
        ConditionEffectCallBack callback = App.Model.conditionEffectModel.currentCallBack;
        App.Model.conditionEffectModel.FinishConditionEffect();
        callback();
    }
}
