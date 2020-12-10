using System.Collections.Generic;
using UnityEngine;

public delegate void EventActionCallBack();

public delegate bool EventValidity();

public struct Connection
{
    public Location destination;

    public ConnectionType type;

    public Connection(Location d, ConnectionType t)
    {
        destination = d;
        type = t;
    }
}

public struct EventAction
{
    public EventType type;
    public EventActionCallBack callback;
    public EventValidity validityCallBack;
    public Spell spell;
    public Asset asset;

    public EventAction(EventType t, EventActionCallBack c)
    {
        type = t;
        callback = c;
        validityCallBack = null;
        spell = null;
        asset = null;
    }

    public EventAction(EventType t, EventActionCallBack c, EventValidity ev, Asset a)
    {
        type = t;
        callback = c;
        validityCallBack = ev;
        spell = null;
        asset = a;
    }

    public EventAction(EventType t, EventActionCallBack c, EventValidity ev, Spell s)
    {
        type = t;
        callback = c;
        validityCallBack = ev;
        spell = s;
        asset = null;
    }
}

public struct StartingItem
{
    public StartingItemType type;

    public string val;

    public StartingItem(StartingItemType t, string v)
    {
        type = t;
        val = v;
    }
}

public struct Clue
{
    public Location location;

    public Clue(Location l)
    {
        location = l;
    }
}

public struct Expedition
{
    public ComplexEncounter encounter;

    public Expedition(ComplexEncounter e)
    {
        encounter = e;
    }
}

public struct InvestigatorPossessions
{
    public int shipTickets;
    public int trainTickets;  
    public int focusTokens;
    public List<Asset> assets;
    public List<Clue> clues;
    public List<Spell> spells;

    public InvestigatorPossessions(int ship, int train, int focus, List<Asset> a, List<Clue> c, List<Spell> s)
    {
        shipTickets = ship;
        trainTickets = train;
        focusTokens = focus;
        assets = new List<Asset>(a);
        clues = new List<Clue>(c);
        spells = new List<Spell>(s);
    }
}

public struct EventQueue
{
    //public List<EventAction> queueCallBacks;
    public List<EventAction> mandatoryCallBacks;
    public List<EventAction> optionalCallBacks;
    public QueueCallBack finishedCallBack;

    public EventQueue(QueueCallBack callback)
    {
        //queueCallBacks = new List<EventAction>();
        mandatoryCallBacks = new List<EventAction>();
        optionalCallBacks = new List<EventAction>();
        finishedCallBack = callback;
    }

    public void AddEvent(EventAction e)
    {
        if (e.type == EventType.Mandatory)
        {
            mandatoryCallBacks.Add(e);
        }
        else
        {
            optionalCallBacks.Add(e);
        }
    }

    public void ExecuteEvent(EventAction e)
    {
        if (e.type == EventType.Mandatory)
        {
            mandatoryCallBacks.Remove(e);
        }
        else
        {
            optionalCallBacks.Remove(e);
        }
    }

    public void RemoveInvalidOptionalEvents()
    {
        int index = 0;
        while (index < optionalCallBacks.Count)
        {
            if (!optionalCallBacks[index].validityCallBack())
            {
                optionalCallBacks.RemoveAt(index);
            }
            else
            {
                index++;
            }
        }
    }
}

public struct IconReference
{
    public int gates;
    public int clues;
    public int surge;

    public IconReference(int g, int c, int s)
    {
        gates = g;
        clues = c;
        surge = s;
    }
}

public struct ReckoningEvent
{
    public string title;
    public string text;
    public ReckoningCallBack callBack;
    public ReckoningSource source;
    public Investigator investigator;
    public Sprite icon;

    public ReckoningEvent(string ti, string te, ReckoningCallBack cb, ReckoningSource s, Investigator i, Sprite ic)
    {
        title = ti;
        text = te;
        callBack = cb;
        source = s;
        investigator = i;
        icon = ic;
    }

    public ReckoningEvent(string ti, string te, ReckoningCallBack cb, ReckoningSource s, Sprite ic)
    {
        title = ti;
        text = te;
        callBack = cb;
        source = s;
        investigator = null;
        icon = ic;
    }
}

public struct MultipleOptionMenuObject
{
    public string text;
    public MultipleOptionType objectType;
    public Monster monster;
    public Asset asset;
    public Investigator investigator;
    public TestStat stat;
    public ReckoningEvent reckoning;
    public Spell spell;

    public MultipleOptionMenuObject(MultipleOptionType type, string s)
    {
        text = s;
        objectType = type;
        monster = null;
        asset = null;
        investigator = null;
        stat = TestStat.None;
        reckoning = new ReckoningEvent();
        spell = null;
    }

    public MultipleOptionMenuObject(MultipleOptionType type, Monster m)
    {
        text = "";
        objectType = type;
        monster = m;
        asset = null;
        investigator = null;
        stat = TestStat.None;
        reckoning = new ReckoningEvent();
        spell = null;
    }

    public MultipleOptionMenuObject(MultipleOptionType type, Asset a)
    {
        text = "";
        objectType = type;
        monster = null;
        asset = a;
        investigator = null;
        stat = TestStat.None;
        reckoning = new ReckoningEvent();
        spell = null;
    }

    public MultipleOptionMenuObject(MultipleOptionType type, Investigator i)
    {
        text = "";
        objectType = type;
        monster = null;
        asset = null;
        investigator = i;
        stat = TestStat.None;
        reckoning = new ReckoningEvent();
        spell = null;
    }

    public MultipleOptionMenuObject(MultipleOptionType type, TestStat s)
    {
        text = "";
        objectType = type;
        monster = null;
        asset = null;
        investigator = null;
        stat = s;
        reckoning = new ReckoningEvent();
        spell = null;
    }

    public MultipleOptionMenuObject(MultipleOptionType type, ReckoningEvent re)
    {
        text = "";
        objectType = type;
        monster = null;
        asset = null;
        investigator = null;
        stat = TestStat.None;
        reckoning = re;
        spell = null;
    }

    public MultipleOptionMenuObject(MultipleOptionType type, Spell s)
    {
        text = "";
        objectType = type;
        monster = null;
        asset = null;
        investigator = null;
        stat = TestStat.None;
        reckoning = new ReckoningEvent();
        spell = s;
    }
}

public struct EldritchToken
{

}