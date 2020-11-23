using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SpellEffectCallBack();

public class SpellEffectModel : MVC
{
    public Spell currentSpell;
    public SpellEffectCallBack currentCallBack;
    public List<string> currentFlipOptions;
    public int activeOption;

    public void StartSpellEffect(Spell s, SpellEffectCallBack callback)
    {
        currentSpell = s;
        currentCallBack = callback;
        App.View.spellEffectView.StartSpellEffect();
    }

    public void StartFlipEffect(Spell s, List<string> options, int active, SpellEffectCallBack callback)
    {
        currentSpell = s;
        currentFlipOptions = options;
        activeOption = active;
        currentCallBack = callback;
        App.View.spellEffectView.StartFlipEffect();
    }

    public void FinishSpellEffect()
    {
        currentSpell = null;
        currentCallBack = null;
        currentFlipOptions.Clear();
        activeOption = -1;
        App.View.spellEffectView.Continue();
    }
}
