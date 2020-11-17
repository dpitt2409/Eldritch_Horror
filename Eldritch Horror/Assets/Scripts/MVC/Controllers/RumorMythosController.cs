using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumorMythosController : MVC
{
    public void StartRumorEvent()
    {
        Mythos m = App.Model.mythosModel.activeMythos;
        m.SpawnRumor();
        App.Model.rumorMythosModel.StartRumor(m.ongoingEffect);
        App.Controller.ongoingEffectController.NewOngoingEffect(m.ongoingEffect);
        if (m.ongoingEffect.location != "")
        {
            Location l = App.Model.locationModel.FindLocationByName(m.ongoingEffect.location);
            App.Controller.locationController.SpawnOngoingEffect(m.ongoingEffect, l);
        }
    }

    public void Continue()
    {
        App.View.rumorMythosView.EventFinished();
        App.Controller.mythosController.NextMythosEffect();
    }
}
