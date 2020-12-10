using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpritesModel : MVC
{
    public Sprite[] statSprites;

    public Sprite[] diceSprites;

    public Dictionary<TestStat, Sprite> testStatSprites;

    public Sprite healthSprite;

    public Sprite sanitySprite;

    public Sprite shipTicketSprite;

    public Sprite trainTicketSprite;

    public Sprite focusTokenSprite;

    public Sprite clueTokenSprite;

    public Sprite gateSprite;

    public Sprite expeditionSprite;

    public Sprite eldritchTokenSprite;

    public void Start()
    {
        testStatSprites = new Dictionary<TestStat, Sprite>();
        testStatSprites.Add(TestStat.Lore, statSprites[0]);
        testStatSprites.Add(TestStat.Influence, statSprites[1]);
        testStatSprites.Add(TestStat.Observation, statSprites[2]);
        testStatSprites.Add(TestStat.Strength, statSprites[3]);
        testStatSprites.Add(TestStat.Will, statSprites[4]);
        testStatSprites.Add(TestStat.None, statSprites[5]);
    }

    public Sprite GetTestStatSprite(TestStat stat)
    {
        return testStatSprites[stat];
    }

    public Sprite GetDiceSprite(int dieVal)
    {
        return diceSprites[dieVal - 1];
    }
}
