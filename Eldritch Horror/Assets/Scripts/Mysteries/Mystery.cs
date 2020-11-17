using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mystery
{
    public string mysteryName;

    public string ancientOne;

    public string mysteryDescription;

    public string mysteryText;

    public int requirement;


    public virtual void StartMystery() { return; }

    public virtual void FinishMystery() { return; }
}
