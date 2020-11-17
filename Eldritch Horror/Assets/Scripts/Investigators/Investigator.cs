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

    public InvestigatorPossessions possessions;

    public Encounter deathEncounter;

    public Location currentLocation;

    public List<string> actionsTakenThisTurn;

    private int incomingDamage = 0;
    
    public void Start()
    {
        actionsTakenThisTurn = new List<string>();
        conditions = new List<Condition>();
        assets = new List<Asset>();
        clues = new List<Clue>();
        SUCCESS = 5;
    }

    public abstract Encounter SetDeathEncounter(bool health);

    public virtual void JoinGame()
    {
        App.Controller.locationController.EnterStartingLocation(this, startingLocation);
        GainStartingItems();
        App.Model.eventModel.damageTakenEvent += DamageTakenEvent;
    }

    public virtual void LeaveGame()
    {
        App.Model.eventModel.damageTakenEvent -= DamageTakenEvent;
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
        }
    }

    public void NewRound()
    {
        actionsTakenThisTurn.Clear();
    }

    public void EnterLocation(Location l)
    {
        currentLocation = l;
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
            EventAction e = new EventAction("death", DeathEvent);
            App.Controller.queueController.AddCallBack(e);
        }
    }

    // Called when you Die
    public void DeathEvent()
    {
        if (currentHealth <= 0)
        {
            deathEncounter = SetDeathEncounter(true);
        }
        else if (currentSanity <= 0)
        {
            deathEncounter = SetDeathEncounter(false);
        }
        possessions = AccumulatePossessions();
        Location l = App.Controller.locationController.FindNearestSpaceOfType(currentLocation, LocationType.City)[0];
        App.Controller.locationController.MoveInvestigator(this, l);
        App.Controller.locationController.InvestigatorDiedOnLocation(this, currentLocation);
        LeaveGame();

        // pass lead investigator
        int playerIndex = -1;
        List<Investigator> allInvestigators = App.Model.investigatorModel.investigators;
        for (int i = 0; i < allInvestigators.Count; i++)
        {
            if (allInvestigators[i].investigatorName == investigatorName)
            {
                playerIndex = i;
            }
        }
        if (playerIndex == 0) // This Investigator was the Lead Investigator
        {
            App.Model.investigatorModel.LeadInvestigatorSelected(1); // Change this to pick a new Lead Investigator
            // Decrement Encounter and Action turn order so that the new Lead investigator still gets their turn
        }

        // Advance Doom by 1
        App.Model.doomModel.AdvanceDoom(1);
        App.Controller.queueController.CreateCallBackQueue(DeathCallBack); // Create Queue
        App.Model.eventModel.DoomAdvanced(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void DeathCallBack()
    {
        App.Controller.queueController.NextCallBack();
    }

    public int GetIncomingDamage()
    {
        return incomingDamage;
    }

    public void SetIncomingDamage(int damage)
    {
        incomingDamage = damage;
    }

    private InvestigatorPossessions AccumulatePossessions()
    {
        return new InvestigatorPossessions(shipTickets, trainTickets, focusTokens, assets, clues);
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
        clues.RemoveAt(clues.Count-1);
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
            a.ownedInvestigator = null;
            a.Lost();
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
}
