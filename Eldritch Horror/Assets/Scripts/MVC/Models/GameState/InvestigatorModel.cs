using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatorModel : MVC
{
    public List<Investigator> investigators;
    public Investigator activeInvestigator;

    // Called at the end of GameSetup to initialize game Investigator list
    public void SetInvestigatorList(List<Investigator> list)
    {
        investigators = list;
        foreach(Investigator i in list)
        {
            i.JoinGame();
        }

        activeInvestigator = investigators[0];
    }

    // Rearranges investigator list so that new lead investigator is index 0
    public void LeadInvestigatorSelected(int index)
    {
        List<Investigator> newInvestigatorList = new List<Investigator>();

        int newListIndex = 0;
        for (int i = index; i < investigators.Count; i++)
        {
            newInvestigatorList.Add(investigators[i]);
            newListIndex++;
        }
        for (int i = 0; i < index; i++)
        {
            newInvestigatorList.Add(investigators[i]);
            newListIndex++;
        }
        investigators = newInvestigatorList;
    }

    public void NewActiveInvestigator(int index)
    {
        activeInvestigator = investigators[index];
    }

    public int GetInvestigatorIndex(string name)
    {
        int index = -1;
        for (int i = 0; i < investigators.Count; i++)
        {
            if (investigators[i].investigatorName == name)
            {
                index = i;
            }
        }
        return index;
    }

    public void ReplaceInvestigator(Investigator i, int index)
    {
        investigators[index] = i;
    }
}
