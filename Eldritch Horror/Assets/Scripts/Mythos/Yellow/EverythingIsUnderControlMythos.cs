using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EverythingIsUnderControlMythos : Mythos
{
    public EverythingIsUnderControlMythos()
    {
        mythosTitle = "Everything Is under Control";
        mythosFlavorText = "It was so much worse than you thought. This ancient creature had not just established a presence here; it had infected every aspect of the city down to its roots.";
        mythosText = "Spawn 1 Monster on each space containing a Gate that corresponds to the current Omen.";
        effects = new MythosEffects[4];
        effects[0] = MythosEffects.AdvanceOmen;
        effects[1] = MythosEffects.ResolveReckoning;
        effects[2] = MythosEffects.SpawnGates;
        effects[3] = MythosEffects.Event;
    }
    
    public override void EventEffect()
    {
        // Spawn 1 Monster on each space containing a Gate that corresponds to the current Omen.
        base.EventEffect();
    }

}
