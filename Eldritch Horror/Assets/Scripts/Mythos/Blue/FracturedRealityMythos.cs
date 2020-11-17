using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturedRealityMythos : Mythos
{
    public FracturedRealityMythos()
    {
        mythosTitle = "Fractured Reality";
        mythosFlavorText = "The strange phenomenon is an echo of the catastrophic destruction of Mu. The repercussions of the serpent people's over-reaching ambitions still take their toll.";
        mythosText = "An an encounter, an investigator on space 2 may use an ancient portal created by a long-dead wizard of Mu; he resolves an Other World Encounter. If the effect allows him to 'close this gate,' solve this rumor instead. \n\r When there are no Eldritch tokens on this card, advance Doom by 1 for each Gate on the game board; then solve this Rumor.";
        effects = new MythosEffects[2];
        effects[0] = MythosEffects.SpawnClues;
        effects[1] = MythosEffects.Rumor;
    }

    public override void SpawnRumor()
    {
        // create new instance of the rumor and set it to ongoingeffect
        ongoingEffect = new FracturedRealityRumor();
    }
}

public class FracturedRealityRumor : OngoingEffect
{
    public FracturedRealityRumor()
    {
        effectTitle = "Fractured Reality";
        effectText = "An an encounter, an investigator on space 2 may use an ancient portal created by a long-dead wizard of Mu; he resolves an Other World Encounter. If the effect allows him to 'close this gate,' solve this rumor instead. \n\r When there are no Eldritch tokens on this card, advance Doom by 1 for each Gate on the game board; then solve this Rumor.";
        reckoningText = "Discard 1 Eldritch token from this card.";

        location = "Space 2";
        eldritchTokens = 4;
    }
}
