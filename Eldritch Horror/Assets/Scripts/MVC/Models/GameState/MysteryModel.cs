using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryModel : MVC
{
    public Mystery activeMystery;
    public int mysteriesSolved = 0;
    public int mysteryProgress = 0;

    private Dictionary<string, List<Mystery>> bossMysteryDecks;
    public List<Mystery> activeMysteryDeck;

    void Start()
    {
        bossMysteryDecks = new Dictionary<string, List<Mystery>>();
        // could/should make this more dynamic
        List<Mystery> cthuluDeck = new List<Mystery>();
        cthuluDeck.Add(new CthuluMystery1());
        bossMysteryDecks.Add("Cthulu", cthuluDeck);

        List<Mystery> yigDeck = new List<Mystery>();
        yigDeck.Add(new YigMystery1());
        bossMysteryDecks.Add("Yig", yigDeck);
    }

    // Called at the end of setup
    public void SetupMystery()
    {
        activeMysteryDeck = bossMysteryDecks[App.Model.ancientOneModel.ancientOne.ancientOneName]; // Set Deck based on chosen Ancient One
        App.View.mysteryView.EnableMysteryUI(); // Enable Mystery Icon
    }

    public void DrawNewMystery()
    {
        int index = Random.Range(0, activeMysteryDeck.Count);
        activeMystery = activeMysteryDeck[index];
        activeMystery.StartMystery();
        // activeMysteryDeck.RemoveAt(index);
        mysteryProgress = 0;
        App.View.mysteryView.UpdateMysteryInfo();
        App.View.mysteryView.NewMystery();
    }

    public void MysterySolved()
    {
        activeMystery.FinishMystery();
        mysteriesSolved++;
        App.View.mysteryView.UpdateMysteryInfo();
        App.View.mysteryView.MysterySolved();
    }

    public void AdvanceMystery()
    {
        mysteryProgress++;
        if (mysteryProgress > activeMystery.requirement)
            mysteryProgress = activeMystery.requirement;

        App.View.mysteryView.UpdateMysteryInfo();
    }
}
