using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OngoingEffect
{
    public string effectTitle;
    public string effectText;
    public string reckoningText;

    public string location;
    public int eldritchTokens;

    public abstract void Spawned();

    public abstract void Resolved();

    public virtual bool CheckEncounter() { return false; }

    public virtual void StartEncounter() { return; }
}
