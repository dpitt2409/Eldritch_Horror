using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MinimizeCallBack();

public class MinimizeModel : MVC
{
    public MinimizeCallBack currentCallBack;

    public void WindowMinimized(MinimizeCallBack callback)
    {
        currentCallBack = callback;
        App.View.minimizeView.WindowMinimized();
    }

    public void Maximize()
    {
        App.View.minimizeView.WindowMaximized();
        if (currentCallBack == null)
            Debug.Log("Error: null maximize callback");
        else
            currentCallBack();

        currentCallBack = null;
    }
}
