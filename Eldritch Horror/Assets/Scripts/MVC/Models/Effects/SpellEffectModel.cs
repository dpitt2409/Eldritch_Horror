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
    public List<int> currentResults;

    public void StartSpellEffect(Spell s, SpellEffectCallBack callback)
    {
        currentSpell = s;
        currentCallBack = callback;
        App.View.spellEffectView.StartSpellEffect();
    }

    public void StartFlipEffect(Spell s, List<string> options, int active, List<int> results, SpellEffectCallBack callback)
    {
        currentSpell = s;
        currentFlipOptions = options;
        activeOption = active;
        currentResults = results;
        currentCallBack = callback;
        App.View.spellEffectView.StartFlipEffect();
    }

    public void FinishSpellEffect()
    {
        currentSpell = null;
        currentCallBack = null;
        currentFlipOptions.Clear();
        currentResults.Clear();
        activeOption = -1;
        App.View.spellEffectView.Continue();
    }
}
