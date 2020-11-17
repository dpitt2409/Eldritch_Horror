using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mythos
{
    public MythosEffects[] effects;

    public string mythosTitle;

    public string mythosFlavorText;

    public string mythosText;

    public MythosCardType mythosType;

    public OngoingEffect ongoingEffect = null;
    public virtual void EventEffect() { GameManager.SingleInstance.App.Controller.mythosEventController.FinishMythosEvent(); }

    public virtual void SpawnRumor() { return; } // create new instance of the rumor and set it to ongoingeffect ?
}
