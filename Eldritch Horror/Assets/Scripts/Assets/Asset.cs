using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Asset
{
    public string assetName;

    public Sprite assetPortrait;

    public string text;

    public AssetType type;

    public AssetSubType[] subTypes;

    public string reckoningText;

    public int cost;

    public bool artifact;

    public bool magical;

    public Investigator ownedInvestigator;

    public abstract void Gained();

    public abstract void Lost();
}
