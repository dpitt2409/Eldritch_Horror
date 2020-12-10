using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellModel : MVC
{
    public List<Spell> spellDeck;
    public List<Spell> spellPool;

    void Start()
    {
        spellDeck = new List<Spell>();
        spellDeck.Add(new FeedTheMindSpell(2));

        spellPool = new List<Spell>(spellDeck);
    }

    public Spell DrawSpellByType(SpellType t)
    {
        List<Spell> matching = new List<Spell>();
        foreach (Spell s in spellPool)
        {
            if (s.type == t)
            {
                matching.Add(s);
            }
        }

        if (matching.Count == 0)
            return null;

        int index = Random.Range(0, matching.Count);
        Spell spell = matching[index];
        spellPool.Remove(spell);
        return spell;
    }

    public Spell DrawSpellByName(string name)
    {
        List<Spell> matching = new List<Spell>();
        foreach (Spell s in spellPool)
        {
            if (s.spellName == name)
            {
                matching.Add(s);
            }
        }

        if (matching.Count == 0)
            return null;

        int index = Random.Range(0, matching.Count);
        Spell spell = matching[index];
        spellPool.Remove(spell);
        return spell;
    }

    // Does not remove Spell from Pool
    public Spell ReferenceSpellByName(string name)
    {
        int index = -1;
        for (int i = 0; i < spellDeck.Count; i++)
        {
            if (spellDeck[i].spellName == name)
            {
                index = i;
            }
        }
        if (index == -1)
            return null;

        Spell s = spellDeck[index];
        return s;
    }

    public void ReturnSpellToPool(Spell s)
    {
        spellPool.Add(s);
    }
}
