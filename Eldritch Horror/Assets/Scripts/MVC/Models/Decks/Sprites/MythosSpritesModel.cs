using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythosSpritesModel : MVC
{

    private Dictionary<GateColor, Color> gateColors;
    private Dictionary<int, Sprite> omenSprites;

    public Sprite rumorSprite;

    public Sprite reckoningSprite;

    public Sprite monsterSurgeSprite;

    public Sprite advanceOmenSprite;

    public Sprite blueOmenSprite;

    public Sprite redOmenSprite;

    public Sprite greenOmenSprite;

    public void Start()
    {
        gateColors = new Dictionary<GateColor, Color>();
        gateColors.Add(GateColor.Blue, Color.blue);
        gateColors.Add(GateColor.Green, Color.green);
        gateColors.Add(GateColor.Red, Color.red);

        omenSprites = new Dictionary<int, Sprite>();
        omenSprites.Add(0, greenOmenSprite);
        omenSprites.Add(1, blueOmenSprite);
        omenSprites.Add(2, redOmenSprite);
        omenSprites.Add(3, blueOmenSprite);
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
