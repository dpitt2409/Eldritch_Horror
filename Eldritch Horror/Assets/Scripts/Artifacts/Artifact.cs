using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Artifact
{
    public string artifactName;

    public Sprite artifactPortrait;

    public string text;

    public string reckoningText;

    public AssetType type;

    public AssetSubType[] subTypes;

    public bool magical;

    public Investigator ownedInvestigator;

    public abstract void Gained();

    public abstract void Lost();

}
