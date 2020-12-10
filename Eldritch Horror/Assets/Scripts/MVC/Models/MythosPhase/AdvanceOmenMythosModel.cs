using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceOmenMythosModel : MVC
{
    public Dictionary<int, GateColor> omenColors;

    public int advanceDoomAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        omenColors = new Dictionary<int, GateColor>();
        omenColors.Add(0, GateColor.Green);
        omenColors.Add(1, GateColor.Blue);
        omenColors.Add(2, GateColor.Red);
        omenColors.Add(3, GateColor.Blue);
    }

    public GateColor GetOmenColor(int omen)
    {
        return omenColors[omen];
    }

    public void StartAdvanceOmen(int advanceAmount)
    {
        advanceDoomAmount = advanceAmount;
        App.View.advanceOmenMythosView.StartAdvanceOmen();
    }

}
