using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteModel : MVC
{
    public Sprite[] statSprites;

    public Dictionary<TestStat, Sprite> testStatSprites;

    public Sprite healthSprite;

    public Sprite sanitySprite;

    public Sprite shipTicketSprite;

    public Sprite trainTicketSprite;

    public Sprite focusTokenSprite;

    public Sprite clueTokenSprite;

    public Sprite gateSprite;

    public Sprite expeditionSprite;

    private Dictionary<GateColor, Color> gateColors;
    private Dictionary<int, Sprite> omenSprites;

    public Sprite rumorSprite;

    public Sprite reckoningSprite;

    public Sprite monsterSurgeSprite;

    public Sprite advanceOmenSprite;

    public Sprite blueOmenSprite;

    public Sprite redOmenSprite;

    public Sprite greenOmenSprite;

    public Sprite cthuluSprite;

    public Sprite yigSprite;

    public Sprite blankIcon;

    public Sprite axeSprite;

    public Sprite arcaneScholarSprite;

    public Sprite whiskeySprite;

    public Sprite luckyTalismanSprite;

    public Sprite privateInvestigatorSprite;

    public Sprite debtSprite;

    public Sprite personalAssistantSprite;

    public Sprite ghoulSprite;

    public void Start()
    {
        testStatSprites = new Dictionary<TestStat, Sprite>();
        testStatSprites.Add(TestStat.Lore, statSprites[0]);
        testStatSprites.Add(TestStat.Influence, statSprites[1]);
        testStatSprites.Add(TestStat.Observation, statSprites[2]);
        testStatSprites.Add(TestStat.Strength, statSprites[3]);
        testStatSprites.Add(TestStat.Will, statSprites[4]);
        testStatSprites.Add(TestStat.None, statSprites[5]);

        gateColors = new Dictionary<GateColor, Color>();
        gateColors.Add(GateColor.Blue, Color.blue);
        gateColors.Add(GateColor.Green, Color.green) ;
        gateColors.Add(GateColor.Red, Color.red);

        omenSprites = new Dictionary<int, Sprite>();
        omenSprites.Add(0, greenOmenSprite);
        omenSprites.Add(1, blueOmenSprite);
        omenSprites.Add(2, redOmenSprite);
        omenSprites.Add(3, blueOmenSprite);
    }

    public Sprite GetTestStatSprite(TestStat stat)
    {
        return testStatSprites[stat];
    }

    public Color GetGateColor(GateColor gc)
    {
        return gateColors[gc];
    }

    public Sprite GetOmenSprite(int omen)
    {
        return omenSprites[omen];
    }

    public Sprite GetOmenSpriteFromGateColor(GateColor gc)
    {
        if (gc == GateColor.Red)
            return redOmenSprite;
        if (gc == GateColor.Green)
            return greenOmenSprite;
        else
            return blueOmenSprite;
    }
}
