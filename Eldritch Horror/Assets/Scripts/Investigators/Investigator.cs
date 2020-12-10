using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Investigator : MVC
{
    public string investigatorName;
    public Sprite investigatorPortrait;

    public string occupation;
    public string flavorText;
    public string actionAbilityText;
    public string passiveAbilityText;
    public string bioText;

    public Location startingLocation;

    public StartingItem[] startingItems;

    public int health;
    public int sanity;

    public int currentHealth;
    public int currentSanity;

    public int strength;
    public int will;
    public int influence;
    public int observation;
    public int lore;

    public int strengthMod = 0;
    public int willMod = 0;
    public int influenceMod = 0;
    public int observationMod = 0;
    public int loreMod = 0;

    public int focusTokens = 0;
    public int shipTickets = 0;
    public int trainTickets = 0;

    public int SUCCESS = 5;

    public List<Clue> clues;

    public List<Condition> conditions;

    public List<Asset> assets;

    public List<Spell> spells;

    public InvestigatorPossessions possessions;

    public Encounter deathEncounter;

    public Location currentLocation;

    public List<string> actionsTakenThisTurn;

    private int incomingDamage = 0;

    public bool delayed = false;

    public void Start()
    {
        actionsTakenThisTurn = new List<string>();
        conditions = new List<Condition>();
        assets = new List<Asset>();
        spells = new List<Spell>();
        clues = new List<Clue>();
        SUCCESS = 5;
    }

    public abstract Encounter SetDeathEncounter(bool health);

    public virtual void JoinGame()
    {
        App.Controller.locationController.EnterStartingLocation(this, startingLocation);
        GainStartingItems();

        App.Model.eventModel.damageTakenEvent += DamageTakenEvent;
        GameManager.SingleInstance.App.Model.eventModel.newRoundEvent += RoundReset;
    }

    public virtual void LeaveGame()
    {
        App.Model.eventModel.damageTakenEvent -= DamageTakenEvent;
        GameManager.SingleInstance.App.Model.eventModel.newRoundEvent -= RoundReset;
    }

    public void GainStartingItems()
    {
        foreach (StartingItem item in startingItems)
        {
            if (item.type == StartingItemType.Asset)
            {
                Asset a = App.Model.assetModel.DrawAssetByName(item.val);
                if (a != null)
                {
                    assets.Add(a);
                    a.ownedInvestigator = this;
                    a.Gained();
                }
            }
            if (item.type == StartingItemType.Spell)
            {
                Spell s = App.Model.spellModel.DrawSpellByName(item.val);
                if (s != null)
                {
                    spells.Add(s);
                    s.owner = this;
                    s.Gained();
                }
            }
        }
    }

    public void RoundReset()
    {
        actionsTakenThisTurn.Clear();
    }

    public void EnterLocation(Location l)
    {
        currentLocation = l;
    }

    public void BecomeDelayed()
    {
        delayed = true;
        App.View.locationView.LocationUpdated(currentLocation);
    }

    public void StopBeingDelayed()
    {
        delayed = false;
        App.View.locationView.LocationUpdated(currentLocation);
    }

    public int CheckStat(TestStat stat)
    {
        if (stat == TestStat.Influence)
            return influence;
        if (stat == TestStat.Lore)
            return lore;
        if (stat == TestStat.Observation)
            return observation;
        if (stat == TestStat.Strength)
            return strength;
        if (stat == TestStat.Will)
            return will;
        return 0;
    }

    public int CheckMod(TestStat stat)
    {
        if (stat == TestStat.Influence)
            return influenceMod;
        if (stat == TestStat.Lore)
            return loreMod;
        if (stat == TestStat.Observation)
            return observationMod;
        if (stat == TestStat.Strength)
            return strengthMod;
        if (stat == TestStat.Will)
            return willMod;
        return 0;
    }

    public List<TestStat> ImprovableSkills()
    {
        List<TestStat> improvable = new List<TestStat>();
        if (influenceMod < 2)
            improvable.Add(TestStat.Influence);
        if (loreMod < 2)
            improvable.Add(TestStat.Lore);
        if (observationMod < 2)
            improvable.Add(TestStat.Observation);
        if (strengthMod < 2)
            improvable.Add(TestStat.Strength);
        if (willMod < 2)
            improvable.Add(TestStat.Will);
        return improvable;
    }

    public List<TestStat> ImpairableSkills()
    {
        List<TestStat> improvable = new List<TestStat>();
        if (influenceMod > -2)
            improvable.Add(TestStat.Influence);
        if (loreMod > -2)
            improvable.Add(TestStat.Lore);
        if (observationMod > -2)
            improvable.Add(TestStat.Observation);
        if (strengthMod > -2)
            improvable.Add(TestStat.Strength);
        if (willMod > -2)
            improvable.Add(TestStat.Will);
        return improvable;
    }

    public void ImproveStat(TestStat stat)
    {
        if (stat == TestStat.Influence)
            influenceMod++;
        if (stat == TestStat.Lore)
            loreMod++;
        if (stat == TestStat.Observation)
            observationMod++;
        if (stat == TestStat.Strength)
            strengthMod++;
        if (stat == TestStat.Will)
            willMod++;
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void ImpairStat(TestStat stat)
    {
        if (stat == TestStat.Influence)
            influenceMod--;
        if (stat == TestStat.Lore)
            loreMod--;
        if (stat == TestStat.Observation)
            observationMod--;
        if (stat == TestStat.Strength)
            strengthMod--;
        if (stat == TestStat.Will)
            willMod--;
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void GainHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > health)
            currentHealth = health;

        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void GainSanity(int amount)
    {
        currentSanity += amount;
        if (currentSanity > sanity)
            currentSanity = sanity;

        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void LoseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void LoseSanity(int amount)
    {
        currentSanity -= amount;
        if (currentSanity < 0)
            currentSanity = 0;

        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void DamageTakenEvent()
    {
        if (currentHealth <= 0 || currentSanity <= 0 && deathEncounter == null)
        {
            EventAction e = new EventAction(EventType.Mandatory, DeathEvent); // This will get updated to provide more information about the event
            App.Controller.queueController.AddCallBack(e);
        }
    }

    // Called when you Die, not Devoured
    public void DeathEvent()
    {
        LeaveGame();
        if (currentHealth <= 0)
        {
            deathEncounter = SetDeathEncounter(true);
        }
        else if (currentSanity <= 0)
        {
            deathEncounter = SetDeathEncounter(false);
        }
        possessions = new InvestigatorPossessions(shipTickets, trainTickets, focusTokens, assets, clues, spells);

        bool relocate = false;
        if (currentLocation.type != LocationType.City)
        {
            relocate = true;
            Location l = App.Controller.locationController.FindNearestSpaceOfType(currentLocation, LocationType.City)[0]; // Choose which City to move body to?
            App.Controller.locationController.MoveInvestigator(this, l);
        }
        App.Controller.locationController.InvestigatorDiedOnLocation(this, currentLocation);

        // Return conditions to pool
        foreach (Condition c in conditions)
        {
            c.Lost();
            c.owner = null;
            App.Model.conditionModel.ReturnConditionToPool(c);
        }
        conditions.Clear();
        App.Controller.previewedInvestigatorController.UpdateInvestigator();

        App.Controller.deadInvestigatorMenuController.InvestigatorDied(this, relocate);
    }

    public void Devoured()
    {
        LeaveGame();
        deathEncounter = SetDeathEncounter(true);
        App.Controller.locationController.LeaveLocation(this);
        
        foreach (Clue c in clues)
        {
            App.Model.clueModel.ClueSpent(c);
        }
        foreach (Condition c in conditions)
        {
            c.Lost();
            c.owner = null;
            App.Model.conditionModel.ReturnConditionToPool(c);
        }
        foreach (Asset a in assets)
        {
            a.Lost();
            a.ownedInvestigator = null;
            App.Model.assetModel.DiscardAsset(a);
        }
        foreach (Spell s in spells)
        {
            s.Lost();
            s.owner = null;
            App.Model.spellModel.ReturnSpellToPool(s);
        }

        loreMod = 0;
        influenceMod = 0;
        observationMod = 0;
        willMod = 0;
        strengthMod = 0;
        focusTokens = 0;
        shipTickets = 0;
        trainTickets = 0;
        clues.Clear();
        conditions.Clear();
        assets.Clear();
        spells.Clear();
       
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public int GetIncomingDamage()
    {
        return incomingDamage;
    }

    public void SetIncomingDamage(int damage)
    {
        incomingDamage = damage;
    }

    public void GainPossessions(InvestigatorPossessions possessions)
    {
        shipTickets += possessions.shipTickets;
        if (shipTickets > 2)
            shipTickets = 2;
        trainTickets += possessions.trainTickets;
        if (trainTickets > 2)
            trainTickets = 2;
        focusTokens += possessions.focusTokens;
        if (focusTokens > 2)
            focusTokens = 2;

        foreach(Asset a in possessions.assets)
        {
            assets.Add(a);
            a.ownedInvestigator = this;
        }
        foreach (Spell s in possessions.spells)
        {
            spells.Add(s);
            s.owner = this;
        }
        foreach(Clue c in possessions.clues)
        {
            clues.Add(c);
        }
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void GainClue(Clue c)
    {
        clues.Add(c);
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void SpendClue()
    {
        Clue c = clues[clues.Count - 1];
        clues.Remove(c);
        App.Model.clueModel.ClueSpent(c);
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void GainAsset(Asset a)
    {
        if (a != null)
        {
            assets.Add(a);
            a.ownedInvestigator = this;
            a.Gained();
            App.Controller.previewedInvestigatorController.UpdateInvestigator();
        }
    }

    public void LoseAsset(Asset a)
    {
        int index = -1;
        for (int i = 0; i < assets.Count; i++)
        {
            if (assets[i].assetName == a.assetName)
            {
                index = i;
            }
        }
        if (index != -1)
        {
            assets.RemoveAt(index);
            a.Lost();
            a.ownedInvestigator = null;
            App.Model.assetModel.DiscardAsset(a);
            App.Controller.previewedInvestigatorController.UpdateInvestigator();
        }
    }

    public void GainSpell(Spell s)
    {
        if (s != null)
        {
            spells.Add(s);
            s.owner = this;
            s.Gained();
            App.Controller.previewedInvestigatorController.UpdateInvestigator();
        }
    }

    public void LoseSpell(Spell s) // Spell is discarded, not traded or lost in some other way
    {
        int index = -1;
        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i].spellName == s.spellName)
            {
                index = i;
            }
        }
        if (index != -1)
        {
            spells.RemoveAt(index);
            s.Lost();
            s.owner = null;
            App.Model.spellModel.ReturnSpellToPool(s);
            App.Controller.previewedInvestigatorController.UpdateInvestigator();
        }
    }

    public void GainCondition(Condition c)
    {
        if (c != null)
        {
            c.owner = this;
            c.Gained();
            conditions.Add(c);
            App.Controller.previewedInvestigatorController.UpdateInvestigator();
        }
    }

    public void LoseCondition(Condition c)
    {
        int index = -1;
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].conditionName == c.conditionName)
            {
                index = i;
            }
        }
        if (index != -1)
        {
            c.owner = null;
            c.Lost();
            App.Model.conditionModel.ReturnConditionToPool(c);
            conditions.RemoveAt(index);
            App.Controller.previewedInvestigatorController.UpdateInvestigator();
        }
    }

    public void ActionPerformed(string s)
    {
        actionsTakenThisTurn.Add(s);
    }

    public List<BasicActions> AvailableActions()
    {
        List<BasicActions> actions = new List<BasicActions>();

        if (!actionsTakenThisTurn.Contains("" + BasicActions.Travel) && CheckTravelAction())
            actions.Add(BasicActions.Travel);
        if (!actionsTakenThisTurn.Contains("" + BasicActions.Rest) && CheckRestAction())
            actions.Add(BasicActions.Rest);
        if (!actionsTakenThisTurn.Contains("" + BasicActions.Prepare) && CheckPrepareAction())
            actions.Add(BasicActions.Prepare);
        if (!actionsTakenThisTurn.Contains("" + BasicActions.Trade) && CheckTradeAction())
            actions.Add(BasicActions.Trade);
        if (!actionsTakenThisTurn.Contains("" + BasicActions.Acquire) && CheckAcquireAction())
            actions.Add(BasicActions.Acquire);
        if (!actionsTakenThisTurn.Contains("" + BasicActions.Focus) && CheckFocusAction())
            actions.Add(BasicActions.Focus);

        return actions;
    }

    private bool CheckTravelAction()
    {
        return true;
    }

    private bool CheckAcquireAction()
    {
        return (currentLocation.type == LocationType.City && currentLocation.monstersOnLocation.Count == 0);
    }

    private bool CheckTradeAction()
    {
        return (currentLocation.investigatorsOnLocation.Count > 1);
    }

    private bool CheckRestAction()
    {
        return (currentLocation.monstersOnLocation.Count == 0);
    }

    private bool CheckPrepareAction()
    {
        return (currentLocation.type == LocationType.City);
    }

    private bool CheckFocusAction()
    {
        return focusTokens < 2;
    }

    public void PerformBasicAction(BasicActions action)
    {
        if (action == BasicActions.Travel)
            TravelBasicAction();
        if (action == BasicActions.Acquire)
            AcquireBasicAction();
        if (action == BasicActions.Trade)
            TradeBasicAction();
        if (action == BasicActions.Rest)
            RestBasicAction();
        if (action == BasicActions.Prepare)
            PrepareBasicAction();
        if (action == BasicActions.Focus)
            FocusBasicAction();
    }

    private void TravelBasicAction()
    {
        App.Controller.travelController.StartTravelAction();
    }

    private void AcquireBasicAction()
    {
        App.Controller.acquireController.StartAcquireAction();
    }

    private void TradeBasicAction()
    {
        App.Controller.tradeController.StartTradeAction();
    }

    private void RestBasicAction()
    {
        App.Controller.restController.StartRestAction();
    }

    private void PrepareBasicAction()
    {
        App.Controller.prepareController.StartPrepareAction();
    }

    private void FocusBasicAction()
    {
        focusTokens++;
        App.Controller.previewedInvestigatorController.UpdateInvestigator();

        actionsTakenThisTurn.Add("" + BasicActions.Focus);
        App.Controller.actionPhaseController.ActionPerformed();
    }

    public void SpendFocusToken()
    {
        focusTokens--;
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void GetShipTicket()
    {
        shipTickets++;
        if (shipTickets + trainTickets > 2)
        {
            if (shipTickets > 2)
            {
                shipTickets--;
            }
            else
            {
                trainTickets--;
            }
        }

        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void GetTrainTicket()
    {
        trainTickets++;
        if (shipTickets + trainTickets > 2)
        {
            if (trainTickets > 2)
            {
                trainTickets--;
            }
            else
            {
                shipTickets--;
            }
        }

        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void SpendShipTicket()
    {
        shipTickets--;
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void SpendTrainTicket()
    {
        trainTickets--;
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void TradeShipTicket(bool gained)
    {
        if (gained)
        {
            shipTickets++;
        }
        else
        {
            shipTickets--;
        }
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void TradeTrainTicket(bool gained)
    {
        if (gained)
        {
            trainTickets++;
        }
        else
        {
            trainTickets--;
        }
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void TradeFocusTokens(bool gained)
    {
        if (gained)
        {
            focusTokens++;
        }
        else
        {
            focusTokens--;
        }
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void TradeClue(Clue c, bool gained)
    {
        if (gained)
        {
            clues.Add(c);
        }
        else
        {
            clues.Remove(c);
        }
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void TradeAsset(Asset a, bool gained)
    {
        if (gained)
        {
            assets.Add(a);
            a.ownedInvestigator = this;
        }
        else
        {
            assets.Remove(a);
        }
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }

    public void TradeSpell(Spell s, bool gained)
    {
        if (gained)
        {
            spells.Add(s);
            s.owner = this;
        }
        else
        {
            spells.Remove(s);
        }
        App.Controller.previewedInvestigatorController.UpdateInvestigator();
    }
}
