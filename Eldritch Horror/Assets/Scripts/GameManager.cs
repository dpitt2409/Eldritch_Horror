using System.Collections;
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
    
    // Game Setup -> Select Lead Investigator -> Start Action Phase -> Start Encounter Phase -> Start Mythos Phase -> Check Mystery

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
        App.Controller.investigatorController.StartLeadInvestigatorSelection();
    }

    public void LeadInvestigatorSelected()
    {
        // Start Action Phase
        App.Controller.actionPhaseController.StartActionPhase();
        /*
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
        //App.Controller.investigatorController.StartLeadInvestigatorSelection();
    }

    public void MythosPhaseComplete()
    {
        // UpdateMystery
        App.Controller.mysteryController.CheckMystery();
    }

    public void MysteryChecked()
    {
        // Reset round
        RoundFinished();
        // Check for Dead Investigators
        App.Controller.queueController.CreateCallBackQueue(StartNextRound); // Create Queue
        foreach (Investigator i in App.Model.investigatorModel.investigators)
        {
            if (i.deathEncounter != null) // Investigator is dead
            {
                EventAction e = new EventAction("Select new Investigator", App.Controller.setupController.DraftNewInvestigator);
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

    private void RoundFinished()
    {
        foreach (Investigator i in App.Model.investigatorModel.investigators)
        {
            i.NewRound();
        }
    }

    public void DebugHelper(string s)
    {
        Debug.Log(s);
    }
}
