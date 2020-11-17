using System.Collections;
using System.Collections.Generic;

public class BlessedCondition : Condition
{
    // Constructor
    public BlessedCondition()
    {
        conditionName = "Blessed";

        //GameManager.SingleInstance.loseHealthEvent += LoseHealthEvent;
        //GameManager.SingleInstance.loseSanityEvent += LoseSanityEvent;
    }

    public void LoseHealthEvent()
    {
        GameManager.SingleInstance.DebugHelper("Blessed Lose Health Event");
    }

    public void LoseSanityEvent()
    {
        GameManager.SingleInstance.DebugHelper("Blessed Lose Sanity Event");
    }

    public override void Gained()
    {

    }

    public override void Lost()
    {

    }
}
