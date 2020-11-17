using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public string conditionName;

    public string conditionText;

    public string reckoningText;

    public Sprite conditionPortrait;

    public ConditionType type;

    public int copyIndex;

    public Investigator owner;

    public abstract void Gained();
    public abstract void Lost();
}
