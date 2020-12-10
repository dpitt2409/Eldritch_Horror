using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumorMythosModel : MVC
{
    public OngoingEffect currentRumor;

    public void StartRumor(OngoingEffect rumor)
    {
        currentRumor = rumor;
        App.View.rumorMythosView.RumorStarted();
    }
}
