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
    private bool activeOtherWorldlyEncounter = false;

    public FracturedRealityRumor()
    {
        effectTitle = "Fractured Reality";
        effectText = "An an encounter, an investigator on space 2 may use an ancient portal created by a long-dead wizard of Mu; he resolves an Other World Encounter. If the effect allows him to 'close this gate,' solve this rumor instead. \n\r When there are no Eldritch tokens on this card, advance Doom by 1 for each Gate on the game board; then solve this Rumor.";
        reckoningText = "Discard 1 Eldritch token from this card.";

        location = "Space 2";
        eldritchTokens = 1;
    }

    public override void Spawned()
    {
        GameManager.SingleInstance.App.Model.eventModel.newRoundEvent += RoundReset;
        GameManager.SingleInstance.App.Model.eventModel.reckoningEvent += ReckoningEvent;
        GameManager.SingleInstance.App.Model.eventModel.otherworldlyCloseGateEvent += CloseGateEvent;
    }

    public override void Resolved()
    {
        GameManager.SingleInstance.App.Model.eventModel.newRoundEvent -= RoundReset;
        GameManager.SingleInstance.App.Model.eventModel.reckoningEvent -= ReckoningEvent;
        GameManager.SingleInstance.App.Model.eventModel.otherworldlyCloseGateEvent -= CloseGateEvent;
    }

    public void RoundReset()
    {
        activeOtherWorldlyEncounter = false;
    }

    public void ReckoningEvent()
    {
        string title = effectTitle + " Rumor Reckoning";
        ReckoningEvent re = new ReckoningEvent(title, reckoningText, StartReckoning, ReckoningSource.Ongoing, GameManager.SingleInstance.App.Model.mythosSpritesModel.rumorSprite);
        GameManager.SingleInstance.App.Model.reckoningMythosModel.AddReckoningEvent(re);
    }

    public void StartReckoning()
    {
        eldritchTokens--;
        if (eldritchTokens == 0)
        {
            int numGates = GameManager.SingleInstance.App.Model.gateModel.activeGates.Count;

            string finishedText = "Advance Doom by 1 for each Gate on the game board. (" + numGates + ") \n\r Then solve this Rumor.";
            GameManager.SingleInstance.App.Controller.reckoningMythosController.SetReckoningText(finishedText);

            GameManager.SingleInstance.App.Model.doomModel.AdvanceDoom(numGates);
            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DoomAdvanced); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.DoomAdvancedEvent(numGates); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            GameManager.SingleInstance.App.Controller.ongoingEffectController.OngoingEffectUpdated(this);
            GameManager.SingleInstance.App.Controller.reckoningMythosController.FinishReckoning();
        }
    }

    public void DoomAdvanced()
    {
        GameManager.SingleInstance.App.Controller.ongoingEffectController.ResolveOngoingEffect(this);
        GameManager.SingleInstance.App.Controller.reckoningMythosController.FinishReckoning();
    }

    public override bool CheckEncounter()
    {
        return true;
    }

    public override void StartEncounter()
    {
        activeOtherWorldlyEncounter = true;
        GameManager.SingleInstance.App.Controller.complexEncounterMenuController.StartComplexEncounter(GameManager.SingleInstance.App.Model.encounterModel.DrawOtherWorldlyEncounter());
    }

    public void CloseGateEvent(Location l)
    {
        if (activeOtherWorldlyEncounter)
        {
            GameManager.SingleInstance.App.Model.gateModel.SetClosingGate(null);
            GameManager.SingleInstance.App.Controller.ongoingEffectController.ResolveOngoingEffect(this);
        }
    }
}
