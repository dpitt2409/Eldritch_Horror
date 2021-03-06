﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MVC
{
    private static GameManager instance;
    public static GameManager SingleInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    void Start()
    {
        StartGame();
    }
    
    // Game Setup -> Draw First Mystery ->
    // Select Lead Investigator -> Start Action Phase -> Start Encounter Phase -> Start Mythos Phase -> Check Mystery
    
    public void StartGame()
    {
        if (App.Model.setupModel.skipSetup)
        {
            App.Controller.setupController.SkipSetup();
        }
        else
        {
            App.Controller.setupController.StartSetup();
        }
    }

    public void SetupFinished()
    {
        App.Controller.mysteryController.DrawFirstMystery();
    }

    public void LeadInvestigatorSelected()
    {
        // Start Action Phase
        App.Controller.actionPhaseController.StartActionPhase();

        /*
        Condition c = App.Model.conditionModel.DrawConditionByName("Dark Pact");
        App.Model.investigatorModel.activeInvestigator.GainCondition(c);
        App.Model.clueModel.SpawnClues(10); // Spawn 10 Clues
        Location l = App.Model.locationModel.FindLocationByName("Antarctica");
        App.Controller.locationController.SpawnGate(l);
        App.Model.gateModel.SpawnGateInLocation(l);
        
        App.Model.clueModel.SpawnClues(10); // Spawn 10 Clues
        foreach(Location l in App.Model.gateModel.DrawGates(3)) // Spawn 3 Gates
        {
            App.Controller.locationController.SpawnGate(l);
        }
        
        Monster m = App.Model.monsterModel.SpawnMonster();
        App.Controller.locationController.SpawnMonsterOnLocation(m, App.Model.investigatorModel.activeInvestigator.currentLocation);
        */
    }

    public void ActionPhaseComplete()
    {
        // Start Encounter Phase
        App.Controller.encounterPhaseController.StartEncounterPhase();
    }

    public void EncounterPhaseComplete()
    {
        // Start Mythos Phase
        App.Controller.mythosController.StartMythosPhase();
    }

    public void MythosPhaseComplete()
    {
        // UpdateMystery
        App.Controller.mysteryController.CheckMystery();
    }

    public void MysteryChecked()
    {
        // Reset round
        App.Model.eventModel.NewRoundEvent();
        // Check for Dead Investigators
        App.Controller.queueController.CreateCallBackQueue(StartNextRound); // Create Queue
        foreach (Investigator i in App.Model.investigatorModel.investigators)
        {
            if (i.deathEncounter != null) // Investigator is dead
            {
                EventAction e = new EventAction(EventType.Mandatory, App.Controller.setupController.DraftNewInvestigator);
                App.Controller.queueController.AddCallBack(e);
            }
        }
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void StartNextRound()
    {
        // Select Lead Investigator
        App.Controller.investigatorController.StartLeadInvestigatorSelection();
    }
}
