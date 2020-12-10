using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenModel : MVC
{
    public int currentOmen;

    public void EnableOmenUI()
    {
        App.View.omenView.OmenUIEnabled();
        currentOmen = 0;
        App.View.omenView.OmenUpdated();
    }

    public void AdvanceOmen()
    {
        currentOmen++;
        if (currentOmen == 4)
            currentOmen = 0;
        App.View.omenView.OmenUpdated();
    }

    public GateColor GetCurrentGateColor()
    {
        if (currentOmen == 0)
            return GateColor.Green;
        else if (currentOmen == 2)
            return GateColor.Red;
        else
            return GateColor.Blue;
    }
}
