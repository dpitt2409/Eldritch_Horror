using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeModel : MVC
{
    public Investigator investigator1;
    public Investigator investigator2;

    public void BeginTrade(Investigator i)
    {
        investigator1 = App.Model.investigatorModel.activeInvestigator;
        investigator2 = i;
        App.View.tradeView.TradeBegun();
    }

    public void DoneTrading()
    {
        investigator1 = null;
        investigator2 = null;
        App.View.tradeView.DoneTrading();
    }
}
