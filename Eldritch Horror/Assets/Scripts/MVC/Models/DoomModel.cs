using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomModel : MVC
{
    public int currentDoom;

    public void EnableDoomUI()
    {
        App.View.doomView.DoomUIEnabled();
        currentDoom = App.Model.ancientOneModel.ancientOne.doom;
        App.View.doomView.DoomUpdated();
    }

    public void AdvanceDoom(int amount)
    {
        currentDoom -= amount;
        if (currentDoom < 0)
            currentDoom = 0;
        App.View.doomView.DoomUpdated();
    }

    public void RetreatDoom(int amount)
    {
        currentDoom += amount;
        App.View.doomView.DoomUpdated();
    }
}
