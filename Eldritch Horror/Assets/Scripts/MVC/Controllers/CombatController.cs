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
        App.Model.combatModel.TestStarted();
        Monster m = App.Model.combatModel.currentMonster;
        App.View.combatView.Test1Started();
        App.Controller.testController.StartTest(m.tests[0], m.testMods[0], TestType.Combat, Test1Complete);
    }

    public void Test1Complete(List<int> results)
    {
        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= App.Model.investigatorModel.activeInvestigator.SUCCESS)
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
            Investigator active = App.Model.investigatorModel.activeInvestigator;
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
        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;
        active.LoseSanity(active.GetIncomingDamage());
        active.SetIncomingDamage(0);

        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(SanityDamageDealt); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void SanityDamageDealt()
    {
        if (App.Model.combatModel.currentInvestigator.deathEncounter != null) // The Investigator died from the sanity test
        {
            App.View.combatView.NextFight();
            FinishFight();
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
        App.Controller.testController.StartTest(m.tests[1], m.testMods[1], TestType.Combat, Test2Complete);
    }

    public void Test2Complete(List<int> results)
    {
        int numSuccesses = 0;
        foreach (int num in results)
        {
            if (num >= App.Model.investigatorModel.activeInvestigator.SUCCESS)
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
            Investigator active = App.Model.investigatorModel.activeInvestigator;
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
        Investigator active = GameManager.SingleInstance.App.Model.investigatorModel.activeInvestigator;
        active.LoseHealth(active.GetIncomingDamage());
        active.SetIncomingDamage(0);

        GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(DealDamageToMonster); // Create Queue
        GameManager.SingleInstance.App.Model.eventModel.DamageTakenEvent(); // Populate Queue
        GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
    }

    public void DealDamageToMonster()
    {
        int success = App.Model.combatModel.numSuccesses;
        Monster m = App.Model.combatModel.currentMonster;
        if (success > 0)
        {
            m.damageTaken += App.Model.combatModel.numSuccesses;
            App.View.combatView.DamageTaken();
            Location l = App.Model.investigatorModel.activeInvestigator.currentLocation;
            if (m.damageTaken >= m.toughness)
            {
                // Monster is dead
                App.Controller.locationController.RemoveMonsterFromLocation(m, l);
                App.Model.monsterModel.ReturnMonsterToPool(m);

                GameManager.SingleInstance.App.Controller.queueController.CreateCallBackQueue(MonsterKilled); // Create Queue
                GameManager.SingleInstance.App.Model.eventModel.MonsterKilledEvent(m); // Populate Queue
                GameManager.SingleInstance.App.Controller.queueController.StartCallBackQueue(); // Start Queue
            }
            else
            {
                // Monster is not dead
                m.FinishTest2(success);
                App.View.locationView.LocationUpdated(l);
            }
        }
        else
        {
            m.FinishTest2(success);
        }
    }

    public void MonsterKilled()
    {
        int success = App.Model.combatModel.numSuccesses;
        Monster m = App.Model.combatModel.currentMonster;
        m.FinishTest2(success);
    }

    public void FinishFight()
    {
        App.Model.combatModel.FightFinished();
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
