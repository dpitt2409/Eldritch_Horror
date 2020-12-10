using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MultipleOptionCallBack(int selected);

public class MultipleOptionModel : MVC
{
    public List<MultipleOptionMenuObject> currentOptions;
    public string menuTitle;
    public MultipleOptionCallBack currentCallBack;

    public void StartMultipleOption(List<MultipleOptionMenuObject> options, string title, MultipleOptionCallBack callback)
    {
        currentOptions = new List<MultipleOptionMenuObject>(options);
        menuTitle = title;
        currentCallBack = callback;
        App.View.multipleOptionView.MultipleOptionStarted();
    }

    public void FinishMultipleOption()
    {
        currentCallBack = null;
        App.View.multipleOptionView.MultipleOptionFinished();
    }
}
