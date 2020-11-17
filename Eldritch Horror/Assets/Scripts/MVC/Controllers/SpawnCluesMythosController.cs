using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCluesMythosController : MVC
{
    public void StartSpawnCluesEvent()
    {
        int numClues = App.Model.mythosModel.reference[App.Model.investigatorModel.investigators.Count].clues;
        App.Model.spawnCluesMythosModel.StartSpawningClues(numClues);
        SpawnNextClue();
    }

    public void SpawnNextClue()
    {
        if (App.Model.spawnCluesMythosModel.cluesToSpawn == 0)
        {
            App.View.spawnCluesMythosView.CluesSpawned();
        }
        else
        {
            List<Clue> spawned = App.Model.clueModel.SpawnClues(1);
            App.Model.spawnCluesMythosModel.ClueSpawned(spawned[0]);
            App.Controller.queueController.CreateCallBackQueue(SpawnNextClue); // Create Queue
            App.Model.eventModel.ClueSpawnedEvent(spawned[0].location); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void Continue()
    {
        App.View.spawnCluesMythosView.Continue();
        App.Controller.mythosController.NextMythosEffect();
    }
}
