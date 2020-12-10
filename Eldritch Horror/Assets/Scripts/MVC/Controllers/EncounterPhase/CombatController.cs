using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MVC
{
    // Investigator is on space with monsters
    public void StartCombatEncounter(Investigator i, List<Monster> monsters, bool ambush, CombatCallBack callback)
    {
        App.Model.combatModel.CombatStarted(i, monsters, ambush, callback);
        NextFight();
    }

    public void NextFight()
    {
        List<Monster> monstersRemaining = new List<Monster>();
        List<Monster> totalMonsters;
        List<Monster> foughtMonsters;
        if (App.Model.combatModel.fightingEpicMonsters)
        {
            totalMonsters = App.Model.combatModel.epicMonstersToFight;
            foughtMonsters = App.Model.combatModel.epicMonstersFought;
        }
        else
        {
            totalMonsters = App.Model.combatModel.normalMonstersToFight;
            foughtMonsters = App.Model.combatModel.normalMonstersFought;
        }

        foreach(Monster m in totalMonsters)
        {
            if (!foughtMonsters.Contains(m))
            {
                monstersRemaining.Add(m);
            }
        }

        if (monstersRemaining.Count == 0)
        {
            if (App.Model.combatModel.fightingEpicMonsters) // Done fighting Epic Monsters
            {
                FinishCombat();
            }
            else // Still need to fight Epic Monsters
            {
                App.Model.combatModel.FinishNormalMonsters();
                NextFight();
            } 
        }
        else
        {
            App.Model.combatModel.SetMonsterOptions(monstersRemaining);
            List<MultipleOptionMenuObject> options = new List<MultipleOptionMenuObject>();
            for (int i = 0; i < monstersRemaining.Count; i++)
            {
                options.Add(new MultipleOptionMenuObject(MultipleOptionType.Monster, monstersRemaining[i]));
            }
            App.Controller.multipleOptionController.StartMultipleOption(options, "Select a Monster to Fight", MonsterSelected);
        }
    }

    public void MonsterSelected(int index)
    {
        List<Monster> monsters = App.Model.combatModel.currentMonsterOptions;
        StartFight(monsters[index]);
    }

    public void StartFight(Monster m)
    {
        App.Model.combatModel.StartFight(m);
        m.StartCombat();
    }

    public void StartTest1()
    {
        if (App.Model.combatModel.currentInvestigator.deathEncounter != null) // The Investigator died during the pre test
        {
            App.View.combatView.NextFight();
            FinishCombat();
            return;
        }

        App.Model.combatModel.TestStarted();
        Monster m = App.Model.combatModel.currentMonster;
        App.View.combatView.Test1Started();
        App.Controller.testController.StartTest(m.tests[0], m.testMods[0], TestType.Combat, App.Model.combatModel.currentInvestigator, Test1Complete);
    }

    public void Test1Complete(List<int> results)
    {
        Investigator active = App.Model.combatModel.currentInvestigator;
        if (active.deathEncounter != null) // If the Investigator died during the test trying to buff up or add results before finishing
        {
            App.View.combatView.NextFight();
            FinishCombat();
            return;
        }

        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= active.SUCCESS)
            {
                numSuccesses++;
            }
        }
        App.Model.combatModel.TestFinished(numSuccesses);
        App.View.combatView.Test1Complete(numSuccesses);

        Monster m = App.Model.combatModel.currentMonster;
        if (numSuccesses < m.horror)
        {
            int damage = m.horror - numSuccesses;
            // Lose Sanity
            active.SetIncomingDamage(damage);
            App.Controller.queueController.CreateCallBackQueue(LoseSanity); // Create Queue
            App.Model.eventModel.LoseSanityEvent(active, damage); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            m.FinishTest1(numSuccesses); // Will call StartTest2
        }
    }

    public void LoseSanity()
    {
        Investigator active = App.Model.combatModel.currentInvestigator;
        if (active.deathEncounter != null)
        {
            App.View.combatView.NextFight();
            FinishCombat();
        }
        else
        {
            active.LoseSanity(active.GetIncomingDamage());
            active.SetIncomingDamage(0);

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(SanityDamageDealt); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void SanityDamageDealt()
    {
        if (App.Model.combatModel.currentInvestigator.deathEncounter != null) // The Investigator died from the sanity test
        {
            App.View.combatView.NextFight();
            FinishCombat();
        }
        else
        {
            Monster m = App.Model.combatModel.currentMonster;
            m.FinishTest1(App.Model.combatModel.numSuccesses);
        }
    }

    public void StartTest2()
    {
        App.Model.combatModel.TestStarted();
        Monster m = App.Model.combatModel.currentMonster;
        App.View.combatView.Test2Started();
        App.Controller.testController.StartTest(m.tests[1], m.testMods[1], TestType.Combat, App.Model.combatModel.currentInvestigator, Test2Complete);
    }

    public void Test2Complete(List<int> results)
    {
        Investigator active = App.Model.combatModel.currentInvestigator;
        if (active.deathEncounter != null) // If the Investigator died during the test trying to buff up or add results before finishing
        {
            App.View.combatView.NextFight();
            FinishCombat();
            return;
        }

        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= active.SUCCESS)
            {
                numSuccesses++;
            }
        }
        App.Model.combatModel.TestFinished(numSuccesses);
        App.View.combatView.Test2Complete(numSuccesses);

        Monster m = App.Model.combatModel.currentMonster;
        if (numSuccesses < m.damage)
        {
            int damage = m.damage - numSuccesses;
            // Lose Health
            active.SetIncomingDamage(damage);
            App.Controller.queueController.CreateCallBackQueue(LoseHealth); // Create Queue
            App.Model.eventModel.LoseHealthEvent(active, damage); // Populate Queue
            App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
        else
        {
            DealDamageToMonster();
        }
    }

    public void LoseHealth()
    {
        Investigator active = App.Model.combatModel.currentInvestigator;
        if (active.deathEncounter != null)
        {
            App.View.combatView.NextFight();
            FinishCombat();
        }
        else
        {
            active.LoseHealth(active.GetIncomingDamage());
            active.SetIncomingDamage(0);

            GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DealDamageToMonster); // Create Queue
            GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
            GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
        }
    }

    public void DealDamageToMonster()
    {
        Investigator active = App.Model.combatModel.currentInvestigator;
        int success = App.Model.combatModel.numSuccesses;
        Monster m = App.Model.combatModel.currentMonster;
        if (success > 0)
        {
            m.damageTaken += success;
            App.View.combatView.DamageTaken();
            Location l = active.currentLocation;
            if (m.damageTaken >= m.toughness) // Monster is dead
            {
                App.Controller.locationController.RemoveMonsterFromLocation(m, l);
                App.Model.monsterModel.ReturnMonsterToPool(m);

                GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(MonsterKilled); // Create Queue
                GameManager.SingleInstance.App.Model.eventModel.MonsterKilledEvent(m); // Populate Queue
                GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
            }
            else // Monster is not dead
            {
                App.View.locationView.LocationUpdated(l);
                if (active.deathEncounter != null) // Investigator died from the damage test
                {
                    App.View.combatView.NextFight();
                    FinishCombat();
                }
                else
                {
                    m.FinishTest2(success);
                }
            }
        }
        else
        {
            if (active.deathEncounter != null) // Investigator died from the damage test
            {
                App.View.combatView.NextFight();
                FinishCombat();
            }
            else
            {
                m.FinishTest2(success);
            }
        }
    }

    public void MonsterKilled()
    {
        if (App.Model.combatModel.currentInvestigator.deathEncounter != null) // The Investigator died but also killed the monster
        {
            App.View.combatView.NextFight();
            FinishCombat();
        }
        else
        {
            int success = App.Model.combatModel.numSuccesses;
            Monster m = App.Model.combatModel.currentMonster;
            m.FinishTest2(success);
        }
    }

    public void FinishFight()
    {
        if (App.Model.combatModel.currentInvestigator.deathEncounter != null) // The Investigator died from the post test 2 event
        {
            App.View.combatView.NextFight();
            NextFight();
        }
        else
        {
            App.Model.combatModel.FightFinished();
        }
    }
    
    public void MonsterContinue()
    {
        App.View.combatView.NextFight();
        NextFight();
    }

    public void FinishCombat()
    {
        CombatCallBack callBack = App.Model.combatModel.combatFinishedCallBack;
        App.Model.combatModel.FinishCombat();
        callBack();
    }
}
