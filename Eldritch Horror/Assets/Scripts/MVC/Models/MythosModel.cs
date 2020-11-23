using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythosModel : MVC
{
    public Dictionary<int, IconReference> reference;
    public List<Mythos> mythosDeck;

    public Mythos activeMythos;
    public int activeIndex = 0;

    void Start()
    {
        reference = new Dictionary<int, IconReference>();
        reference.Add(1, new IconReference(1, 1, 1));
        reference.Add(2, new IconReference(1, 1, 1));
        reference.Add(3, new IconReference(1, 2, 2));
        reference.Add(4, new IconReference(1, 2, 2));
        reference.Add(5, new IconReference(2, 3, 2));
        reference.Add(6, new IconReference(2, 3, 2));
        reference.Add(7, new IconReference(2, 4, 3));
        reference.Add(8, new IconReference(2, 4, 3));
        CreateMythosDeck();
    }

    public void CreateMythosDeck() // Will later take an object describing how to build the deck and be called from the setup instead of in Start
    {
        mythosDeck = new List<Mythos>();

        mythosDeck.Add(new EverythingIsUnderControlMythos());
        //mythosDeck.Add(new FracturedRealityMythos());
    }

    public Mythos DrawMythosCard()
    {
        return mythosDeck[Random.Range(0, mythosDeck.Count)]; // Will later need to draw from the top and remove from deck
    }

    public void StartMythosCard(Mythos m)
    {
        activeMythos = m;
        activeIndex = -1;
        App.View.mythosView.StartMythos();
    }

    public void NextEffect()
    {
        activeIndex++;
    }

}
