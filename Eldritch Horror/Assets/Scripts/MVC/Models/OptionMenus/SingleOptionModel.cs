using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SingleOptionCallBack(bool response); 

public class SingleOptionModel : MVC
{
    public string menuTitle;
    public string menuText;
    public string yesButtonText;
    public string noButtonText;
    public SingleOptionCallBack callBack;

    public void StartSingleOption(string title, string text, string yesText, string noText, SingleOptionCallBack cb)
    {
        menuTitle = title;
        menuText = text;
        yesButtonText = yesText;
        noButtonText = noText;
        callBack = cb;
        App.View.singleOptionView.SingleOptionStarted();
    }

    public void FinishSingleOption()
    {
        callBack = null;
        App.View.singleOptionView.EventFinished();
    }
}
