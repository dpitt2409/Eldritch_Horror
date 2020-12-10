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

    public override void Gained()
    {

    }

    public override void Lost()
    {

    }
}
