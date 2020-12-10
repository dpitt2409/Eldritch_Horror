using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectController : MVC
{
    public void StartSpellEffect(Spell s, SpellEffectCallBack callback)
    {
        App.Model.spellEffectModel.StartSpellEffect(s, callback);
    }

    public void SetFrontResultText(string text)
    {
        App.View.spellEffectView.SetFrontResultText(text);
    }

    public void StartFlipEffect(Spell s, List<string> options, int activeOption, List<int> results, SpellEffectCallBack callback)
    {
        App.Model.spellEffectModel.StartFlipEffect(s, options, activeOption, results, callback);
    }

    // Called externally from Spell. Enables the continue button
    public void SpellEffectFinished()
    {
        App.View.spellEffectView.SpellEffectFinished();
    }

    public void Continue()
    {
        SpellEffectCallBack callback = App.Model.spellEffectModel.currentCallBack;
        App.Model.spellEffectModel.FinishSpellEffect();
        callback();
    }
}
