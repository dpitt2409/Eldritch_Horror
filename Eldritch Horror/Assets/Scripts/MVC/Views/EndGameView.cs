using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MVC
{
    [SerializeField]
    private Sprite winGame;
    [SerializeField]
    private Sprite loseGame;

    private GameObject endGameScreen;
    private Text endGameText;

    void Start()
    {
        endGameScreen = transform.GetChild(0).GetChild(0).gameObject;
        endGameText = endGameScreen.transform.GetChild(0).GetComponent<Text>();

        endGameScreen.SetActive(false);
    }

    public void EndGame(bool win)
    {
        endGameScreen.SetActive(true);
        if (win)
        {
            endGameScreen.GetComponent<Image>().sprite = winGame;
            endGameText.text = "Investigators Win the Game!";
        }
        else
        {
            endGameScreen.GetComponent<Image>().sprite = loseGame;
            endGameText.text = "Investigators Lose the Game!";
        }
    }
}
