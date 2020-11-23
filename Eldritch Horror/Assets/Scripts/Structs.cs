using System.Collections.Generic;
using UnityEngine;

public delegate void EventActionCallBack();

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
    public string name;

    public EventActionCallBack callback;

    public EventAction(string n, EventActionCallBack c)
    {
        name = n;
        callback = c;
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

    public InvestigatorPossessions(int ship, int train, int focus, List<Asset> a, List<Clue> c)
    {
        shipTickets = ship;
        trainTickets = train;
        focusTokens = focus;
        assets = a;
        clues = c;
    }
}

public struct EventQueue
{
    public List<EventAction> queueCallBacks;
    public int callBackIndex;
    public QueueCallBack finishedCallBack;

    public EventQueue(QueueCallBack callback)
    {
        queueCallBacks = new List<EventAction>();
        callBackIndex = -1;
        finishedCallBack = callback;
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
    public MultipleOptionType objectType;
    public Monster monster;
    public Asset asset;
    public Investigator investigator;
    public TestStat stat;
    public ReckoningEvent reckoning;

    public MultipleOptionMenuObject(MultipleOptionType type, Monster m)
    {
        objectType = type;
        monster = m;
        asset = null;
        investigator = null;
        stat = TestStat.None;
        reckoning = new ReckoningEvent();
    }

    public MultipleOptionMenuObject(MultipleOptionType type, Asset a)
    {
        objectType = type;
        monster = null;
        asset = a;
        investigator = null;
        stat = TestStat.None;
        reckoning = new ReckoningEvent();
    }

    public MultipleOptionMenuObject(MultipleOptionType type, Investigator i)
    {
        objectType = type;
        monster = null;
        asset = null;
        investigator = i;
        stat = TestStat.None;
        reckoning = new ReckoningEvent();
    }

    public MultipleOptionMenuObject(MultipleOptionType type, TestStat s)
    {
        objectType = type;
        monster = null;
        asset = null;
        investigator = null;
        stat = s;
        reckoning = new ReckoningEvent();
    }

    public MultipleOptionMenuObject(MultipleOptionType type, ReckoningEvent re)
    {
        objectType = type;
        monster = null;
        asset = null;
        investigator = null;
        stat = TestStat.None;
        reckoning = re;
    }
}

public struct EldritchToken
{

}