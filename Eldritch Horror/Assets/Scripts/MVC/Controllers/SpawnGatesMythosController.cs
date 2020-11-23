using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGatesMythosController : MVC
{
    public void StartSpawnGates()
    {
        int numGates = App.Model.mythosModel.reference[App.Model.investigatorModel.investigators.Count].gates;
        List<Location> gates = App.Model.gateModel.DrawGates(numGates);
        App.Model.spawnGatesMythosModel.SpawnedGates(gates);
        if (gates.Count < numGates)
        {
            // Advance Doom by 1
            int advaceAmount = (numGates-gates.Count);
            App.Model.doomModel.AdvanceDoom(advaceAmount);
            App.Controller.queueController.CreateCallBackQueue(DoomAdvanced); // Create Queue
            App.Model.eventModel.DoomAdvancedEvent(advaceAmount); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            SpawnNextGate();
        }
    }

    public void DoomAdvanced()
    {
        SpawnNextGate();
    }

    public void SpawnNextGate()
    {
        if (App.Model.spawnGatesMythosModel.gatesToSpawn.Count == 0)
        {
            App.View.spawnGatesMythosView.GatesSpawned();
        }
        else
        {
            // Spawn gate 0
            Location l = App.Model.spawnGatesMythosModel.gatesToSpawn[0];
            App.Controller.locationController.SpawnGate(l);
            App.Controller.queueController.CreateCallBackQueue(GateSpawned); // Create Queue
            App.Model.eventModel.SpawnGateEvent(l); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void GateSpawned()
    {
        Location l = App.Model.spawnGatesMythosModel.gatesToSpawn[0];
        
        Monster m = App.Model.monsterModel.SpawnMonster();
        App.Controller.locationController.SpawnMonsterOnLocation(m, l);

        App.Model.spawnGatesMythosModel.GateSpawned(m);

        App.Controller.queueController.CreateCallBackQueue(SpawnNextGate); // Create Queue
        App.Model.eventModel.MonsterSpawnedEvent(m); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void Continue()
    {
        App.View.spawnGatesMythosView.EventFinished();
        App.Controller.mythosController.NextMythosEffect();
    }
}
