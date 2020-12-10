using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngoingEffectController : MVC
{
    public void NewOngoingEffect(OngoingEffect oe)
    {
        App.Model.ongoingEffectModel.NewOngoingEffect(oe);
    }

    public void ResolveOngoingEffect(OngoingEffect oe)
    {
        App.Model.ongoingEffectModel.OngoingEffectFinished(oe);
        Location l = App.Model.locationModel.FindLocationByName(oe.location);
        App.Controller.locationController.ResolveOngoingEffect(oe, l);
    }

    public void ToggleOngoingEffectList()
    {
        App.View.ongoingEffectView.ToggleOngoingEffectList();
    }

    public void OngoingEffectUpdated(OngoingEffect oe)
    {
        App.View.ongoingEffectView.OngoingEffectsChanged();
        App.View.locationView.LocationUpdated(App.Model.locationModel.FindLocationByName(oe.location));
    }
}
