using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythosController : MVC
{
    public void StartMythosPhase()
    {
        App.Controller.investigatorController.NewActiveInvestigator(0); // Set Lead Investigator to active
        StartMythosCard(App.Model.mythosModel.DrawMythosCard());
    }

    public void StartMythosCard(Mythos m)
    {
        App.Model.mythosModel.StartMythosCard(m);
    }

    public void Continue()
    {
        App.View.mythosView.Next();
        NextMythosEffect();
    }

    public void NextMythosEffect() // Point Continue button to this
    {
        App.Model.mythosModel.NextEffect();
        int newIndex = App.Model.mythosModel.activeIndex;
        MythosEffects[] effects = App.Model.mythosModel.activeMythos.effects;
        
        if (newIndex == effects.Length)
        {
            GameManager.SingleInstance.MythosPhaseComplete();
            return;
        }

        MythosEffects next = effects[newIndex];
        switch (next)
        {
            case MythosEffects.AdvanceOmen:
                AdvanceOmen();
                break;
            case MythosEffects.ResolveReckoning:
                Reckoning();
                break;
            case MythosEffects.SpawnGates:
                SpawnGates();
                break;
            case MythosEffects.SpawnClues:
                SpawnClues();
                break;
            case MythosEffects.MonsterSurge:
                MonsterSurge();
                break;
            case MythosEffects.Event:
                MythosEvent();
                break;
            case MythosEffects.Rumor:
                StartRumor();
                break;
            default:
                return;
        }
    }

    public void AdvanceOmen()
    {
        App.Controller.advanceOmenMythosController.StartAdvanceOmen();
    }

    public void Reckoning()
    {
        App.Controller.reckoningMythosController.StartReckoning();
    }

    public void SpawnGates()
    {
        App.Controller.spawnGatesMythosController.StartSpawnGates();
    }

    public void SpawnClues()
    {
        App.Controller.spawnCluesMythosController.StartSpawnCluesEvent();
    }

    public void MonsterSurge()
    {
        App.Controller.monsterSurgeMythosController.StartMonsterSurge();
    }

    public void MythosEvent()
    {
        App.Controller.mythosEventController.StartMythosEvent();
    }

    public void StartRumor()
    {
        App.Controller.rumorMythosController.StartRumorEvent();
    }
}
