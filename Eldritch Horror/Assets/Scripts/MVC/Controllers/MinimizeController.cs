using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimizeController : MVC
{
    public void WindowMinimized(MinimizeCallBack callback)
    {
        App.Model.minimizeModel.WindowMinimized(callback);
    }

    public void Maximize()
    {
        App.Model.minimizeModel.Maximize();
    }

    public void HideMaximizeButton()
    {
        App.View.minimizeView.HideMaximizeButton();
    }

    public void EnableMaximizeButton()
    {
        if (App.Model.minimizeModel.currentCallBack != null)
            App.View.minimizeView.EnableMaximizeButton();
    }
}
