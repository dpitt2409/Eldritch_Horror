using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DevouredCallBack();

public class DeadInvestigatorMenuModel : MVC
{
    public Investigator currentDeadInvestigator;
    public bool investigatorRelocated;

    public bool devoured = false;
    public DevouredCallBack devouredCallBack;

    public void InvestigatorDied(Investigator i, bool relocated)
    {
        currentDeadInvestigator = i;
        investigatorRelocated = relocated;
        devoured = false;
        App.View.deadInvestigatorMenuView.InvestigatorDied();
    }

    public void InvestigatorDevoured(Investigator i, DevouredCallBack callback)
    {
        currentDeadInvestigator = i;
        App.View.deadInvestigatorMenuView.InvestigatorDevoured();
        devoured = true;
        devouredCallBack = callback;
    }
}
