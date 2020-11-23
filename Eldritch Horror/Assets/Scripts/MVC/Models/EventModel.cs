using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventModel : MVC
{
    public delegate void VoidEvent();
    public delegate void IntEvent(int num);
    public delegate void DamageEvent(Investigator i, int damage);
    public delegate void LocationEvent(Location l);
    public delegate void MonsterEvent(Monster m);
    public delegate void ConditionEvent(Condition c);
    public delegate void SpellEvent(Spell s);
    public delegate void StatEvent(Investigator i, TestStat stat);
    public event VoidEvent newRoundEvent;
    public event VoidEvent actionListEvent;
    public event LocationEvent travelEvent;
    public event VoidEvent restEvent;
    public event VoidEvent preTestEvent;
    public event VoidEvent postTestEvent;
    public event LocationEvent clueSpawnedEvent;
    public event VoidEvent researchClueClaimedEvent;
    public event VoidEvent clueGainedEvent;
    public event VoidEvent clueSpentEvent;
    public event DamageEvent loseSanityEvent;
    public event DamageEvent loseHealthEvent;
    public event VoidEvent damageTakenEvent;
    public event LocationEvent otherworldlyCloseGateEvent;
    public event LocationEvent spawnGateEvent;
    public event LocationEvent closeGateEvent;
    public event IntEvent doomRetreatedEvent;
    public event IntEvent doomAdvancedEvent;
    public event MonsterEvent monsterSpawnedEvent;
    public event MonsterEvent monsterKilledEvent;
    public event VoidEvent reckoningEvent;
    public event ConditionEvent conditionLostEvent;
    public event SpellEvent spellLostEvent;
    public event StatEvent statImprovedEvent;

    public void NewRoundEvent()
    {
        if (newRoundEvent != null)
            newRoundEvent.Invoke();
    }

    public void ActionListEvent()
    {
        if (actionListEvent != null)
            actionListEvent.Invoke();
    }

    public void TravelEvent(Location l)
    {
        if (travelEvent != null)
            travelEvent.Invoke(l);
    }

    public void RestEvent()
    {
        if (restEvent != null)
            restEvent.Invoke();
    }

    public void PreTestEvent()
    {
        if (preTestEvent != null)
            preTestEvent.Invoke();
    }

    public void PostTestEvent()
    {
        if (postTestEvent != null)
            postTestEvent.Invoke();
    }

    public void ClueSpawnedEvent(Location l)
    {
        if (clueSpawnedEvent != null)
            clueSpawnedEvent.Invoke(l);
    }

    public void ResearchClueClaimedEvent()
    {
        if (researchClueClaimedEvent != null)
            researchClueClaimedEvent.Invoke();
    }

    public void ClueGainedEvent()
    {
        if (clueGainedEvent != null)
            clueGainedEvent.Invoke();
    }

    public void ClueSpentEvent()
    {
        if (clueSpentEvent != null)
            clueSpentEvent.Invoke();
    }

    public void DamageTakenEvent()
    {
        if (damageTakenEvent != null)
            damageTakenEvent.Invoke();
    }

    public void LoseSanityEvent(Investigator i, int amount)
    {
        if (loseSanityEvent != null)
            loseSanityEvent.Invoke(i, amount);
    }

    public void LoseHealthEvent(Investigator i, int amount)
    {
        if (loseHealthEvent != null)
            loseHealthEvent.Invoke(i, amount);
    }
    public void OtherworldlyCloseGateEvent(Location l)
    {
        if (otherworldlyCloseGateEvent != null)
            otherworldlyCloseGateEvent.Invoke(l);
    }
    
    public void SpawnGateEvent(Location l)
    {
        if (spawnGateEvent != null)
            spawnGateEvent.Invoke(l);
    }

    public void CloseGateEvent(Location l)
    {
        if (closeGateEvent != null)
            closeGateEvent.Invoke(l);
    }

    public void DoomRetreatedEvent(int num)
    {
        if (doomRetreatedEvent != null)
            doomRetreatedEvent.Invoke(num);
    }

    public void DoomAdvancedEvent(int num)
    {
        if (doomAdvancedEvent != null)
            doomAdvancedEvent.Invoke(num);
    }

    public void MonsterSpawnedEvent(Monster m)
    {
        if (monsterSpawnedEvent != null)
            monsterSpawnedEvent.Invoke(m);
    }

    public void MonsterKilledEvent(Monster m)
    {
        if (monsterKilledEvent != null)
            monsterKilledEvent.Invoke(m);
    }

    public void ReckoningEvent()
    {
        if (reckoningEvent != null)
            reckoningEvent.Invoke();
    }

    public void ConditionLostEvent(Condition c)
    {
        if (conditionLostEvent != null)
            conditionLostEvent.Invoke(c);
    }
    public void SpellLostEvent(Spell s)
    {
        if (spellLostEvent != null)
            spellLostEvent.Invoke(s);
    }

    public void StatImprovedEvent(Investigator i, TestStat stat)
    {
        if (statImprovedEvent != null)
            statImprovedEvent.Invoke(i, stat);
    }
}
