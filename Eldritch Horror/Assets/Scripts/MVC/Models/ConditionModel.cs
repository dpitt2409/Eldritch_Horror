using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionModel : MVC
{
    List<Condition> conditionDeck;

    public void Start()
    {
        conditionDeck = new List<Condition>();

        conditionDeck.Add(new BlessedCondition());
        conditionDeck.Add(new DebtCondition(0));
        conditionDeck.Add(new DebtCondition(0));
        conditionDeck.Add(new DebtCondition(0));
        conditionDeck.Add(new DebtCondition(1));
        conditionDeck.Add(new DebtCondition(1));
    }

    public Condition DrawConditionByName(string name)
    {
        List<Condition> possibilities = new List<Condition>();
        foreach (Condition c in conditionDeck)
        {
            if (c.conditionName == name)
            {
                possibilities.Add(c);
            }
        }
        if (possibilities.Count == 0)
            return null;

        Condition con = possibilities[Random.Range(0, possibilities.Count)];
        conditionDeck.Remove(con);
        return con;
    }

    public void ReturnConditionToPool(Condition c)
    {
        conditionDeck.Add(c);
    }

}
