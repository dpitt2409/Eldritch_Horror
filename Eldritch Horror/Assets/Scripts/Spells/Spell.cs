using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public string spellName;

    public Sprite spellPortrait;

    public string text;

    public SpellType type;

    public string reckoningText;

    public int copyIndex;

    public Investigator owner;

    public abstract void Gained();
    public abstract void Lost();
}
