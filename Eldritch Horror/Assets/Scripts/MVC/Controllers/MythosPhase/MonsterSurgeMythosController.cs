using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSurgeMythosController : MVC
{
    public void StartMonsterSurge()
    {
        int monstersPerSpace = App.Model.mythosModel.reference[App.Model.investigatorModel.investigators.Count].surge;
        GateColor currentOmen = App.Model.omenModel.GetCurrentGateColor();
        List<Location> matchingLocations = new List<Location>();
        foreach (Location l in App.Model.gateModel.activeGates)
        {
            if (l.gate == currentOmen)
            {
                matchingLocations.Add(l);
            }
        }

        App.Model.monsterSurgeMythosModel.StartSurge(matchingLocations);
        if (matchingLocations.Count == 0)
        {
            // spawn 1 gate
            List<Location> l = App.Model.gateModel.DrawGates(1);
            App.Model.monsterSurgeMythosModel.GateSpawned(l[0]);
            App.Controller.locationController.SpawnGate(l[0]);
            App.Controller.queueController.CreateCallBackQueue(GateSpawned); // Create Queue
            App.Model.eventModel.SpawnGateEvent(l[0]); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            // spawn monsters
            SpawnNextMonster();
        }
    }

    public void SpawnNextMonster()
    {
        int monstersPerSpace = App.Model.mythosModel.reference[App.Model.investigatorModel.investigators.Count].surge;
        if (App.Model.monsterSurgeMythosModel.monstersSpawnedOnLocation == monstersPerSpace) // Done spawning on current location
        {
            App.Model.monsterSurgeMythosModel.NextLocation();
            if (App.Model.monsterSurgeMythosModel.currentLocation == App.Model.monsterSurgeMythosModel.locationsToSurge.Count) // Done Surging
            {
                // update the view to display the surged monsters
                App.View.monsterSurgeMythosView.MonstersSurged();
            }
            else // More locations to surge
            {
                //App.Model.monsterSurgeMythosModel.NextLocation();
                SpawnNextMonster();
            }
        }
        else // Need to spawn more monsters on current Location
        {
            // Spawn monster on the gate
            Location l = App.Model.monsterSurgeMythosModel.locationsToSurge[App.Model.monsterSurgeMythosModel.currentLocation];
            Monster m = App.Model.monsterModel.SpawnMonster();
            App.Controller.locationController.SpawnMonsterOnLocation(m, l);

            // Update the Model with the Monster spawned
            App.Model.monsterSurgeMythosModel.MonsterSpawnedOnSpace(m, l);

            App.Controller.queueController.CreateCallBackQueue(SpawnNextMonster); // Create Queue
            App.Model.eventModel.MonsterSpawnedEvent(m); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void GateSpawned()
    {
        // Spawn monster on the gate
        Location l = App.Model.monsterSurgeMythosModel.gateSpawnLocation;
        Monster m = App.Model.monsterModel.SpawnMonster();
        App.Controller.locationController.SpawnMonsterOnLocation(m, l);

        // Update the Model with the Monster spawned
        App.Model.monsterSurgeMythosModel.SingleGateMonsterSpawned(m);

        App.Controller.queueController.CreateCallBackQueue(GateMonsterSpawned); // Create Queue
        App.Model.eventModel.MonsterSpawnedEvent(m); // Populate Queue
        App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void GateMonsterSpawned()
    {
        // update the view to show the gate/monster spawned
        App.View.monsterSurgeMythosView.MonstersSurged();
    }

    public void Continue()
    {
        App.View.monsterSurgeMythosView.SurgeFinished();
        App.Controller.mythosController.NextMythosEffect();
    }
}
