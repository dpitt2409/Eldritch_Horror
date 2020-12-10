using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientOneController : MVC
{
    public void ToggleAncientOneInfo()
    {
        App.View.ancientOneView.ToggleAncientOneInfo();
    }

    public void FlipAncientOneInfo()
    {
        App.View.ancientOneView.Flip();
    }

    public void EnablePostFlipTokens(Sprite icon, int num)
    {
        App.View.ancientOneView.EnablePostFlipTokens(icon, num);
    }

    public void UpdatePostFlipTokenVal(int num)
    {
        App.View.ancientOneView.UpdatePostFlipTokens(num);
    }
}
