using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ReckoningCallBack();

public class ReckoningMythosModel : MVC
{
    public List<ReckoningEvent> reckoningEvents; // Total List
    
    public List<ReckoningEvent>[] events; // Separated Lists
    public int activeList;

    public List<ReckoningEvent> currentEvents; // Current List
    public int currentEvent; // Current Event

    public ReckoningCallBack currentReckoningNextButtonCallBack;

    public int totalMonsterReckonings = 0;
    public int totalAncientOneReckonings = 0;
    public int totalOngoingEffectReckonings = 0;
    public int totalInvestigatorReckonings = 0;

    public void StartReckoning()
    {
        reckoningEvents = new List<ReckoningEvent>();
    }

    public void SortReckonings()
    {
        List<ReckoningEvent> monsterReckonings = new List<ReckoningEvent>();
        List<ReckoningEvent> ancientOneReckonings = new List<ReckoningEvent>();
        List<ReckoningEvent> ongoingReckonings = new List<ReckoningEvent>();
        List<ReckoningEvent> investigatorReckonings = new List<ReckoningEvent>();

        foreach (ReckoningEvent re in reckoningEvents)
        {
            if (re.source == ReckoningSource.Monster)
            {
                monsterReckonings.Add(re);
            }
            if (re.source == ReckoningSource.AncientOne)
            {
                ancientOneReckonings.Add(re);
            }
            if (re.source == ReckoningSource.Ongoing)
            {
                ongoingReckonings.Add(re);
            }
            if (re.source == ReckoningSource.Investigator)
            {
                investigatorReckonings.Add(re);
            }
        }

        List<ReckoningEvent>[] allEvents = new List<ReckoningEvent>[4];
        allEvents[0] = monsterReckonings;
        allEvents[1] = ancientOneReckonings;
        allEvents[2] = ongoingReckonings;
        allEvents[3] = investigatorReckonings;

        events = allEvents;

        totalMonsterReckonings = monsterReckonings.Count;
        totalAncientOneReckonings = ancientOneReckonings.Count;
        totalOngoingEffectReckonings = ongoingReckonings.Count;
        totalInvestigatorReckonings = investigatorReckonings.Count;

        activeList = 0;
        currentEvents = events[activeList];

        App.View.ReckoningMythosView.StartReckoning();
    }

    public void AddReckoningEvent(ReckoningEvent e)
    {
        reckoningEvents.Add(e);
    }

    public void FinishReckoning()
    {
        currentEvents.RemoveAt(currentEvent);
        App.View.ReckoningMythosView.EnableNextReckoning();
    }

    public void NextReckoningList()
    {
        activeList++;
        if (activeList < events.Length)
            currentEvents = events[activeList];
    }

    public void StartReckoningEvent(int index)
    {
        currentEvent = index;
        App.View.ReckoningMythosView.NextReckoningEvent();
    }

    public void SetCurrentReckoningNextButtonCallBack(ReckoningCallBack callback)
    {
        currentReckoningNextButtonCallBack = callback;
        App.View.ReckoningMythosView.EnableCurrentReckoningNextButton();
    }

    public void CurrentReckoningNextButton()
    {
        currentReckoningNextButtonCallBack = null;
        App.View.ReckoningMythosView.CurrentReckoningNextButton();
    }
}
