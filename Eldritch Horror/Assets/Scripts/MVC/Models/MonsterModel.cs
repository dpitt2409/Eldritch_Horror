﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel : MVC
{
    public List<Monster> monsterPool;
    public List<Monster> activeMonsters;


    // Start is called before the first frame update
    void Start()
    {
        monsterPool = new List<Monster>();
        activeMonsters = new List<Monster>();

        monsterPool.Add(new GhoulMonster());
        monsterPool.Add(new GhoulMonster());
        monsterPool.Add(new GhoulMonster());
        monsterPool.Add(new GhoulMonster());
        monsterPool.Add(new GhoulMonster());
        monsterPool.Add(new GhoulMonster());
        monsterPool.Add(new GhoulMonster());
        monsterPool.Add(new GhoulMonster());
    }

    public Monster SpawnMonster()
    {
        int index = Random.Range(0, monsterPool.Count);
        Monster m = monsterPool[index];
        m.damageTaken = 0;
        monsterPool.RemoveAt(index);
        activeMonsters.Add(m);
        return m;
    }

    public void ReturnMonsterToPool(Monster m)
    {
        activeMonsters.Remove(m);
        monsterPool.Add(m);
    }
}