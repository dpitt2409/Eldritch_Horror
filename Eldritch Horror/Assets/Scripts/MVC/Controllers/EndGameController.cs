using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MVC
{
    public void EndGame(bool win)
    {
        App.Controller.setupController.HideAllUI();
        App.View.endGameView.EndGame(win);
    }
}
