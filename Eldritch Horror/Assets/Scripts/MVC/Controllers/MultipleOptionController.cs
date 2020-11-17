using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleOptionController : MVC
{
    public void StartMultipleOption(List<MultipleOptionMenuObject> options, string menuTitle, MultipleOptionCallBack callback)
    {
        App.Model.multipleOptionModel.StartMultipleOption(options, menuTitle, callback);
    }

    public void SelectOption(int index)
    {
        MultipleOptionCallBack callback = App.Model.multipleOptionModel.currentCallBack;
        App.Model.multipleOptionModel.FinishMultipleOption();
        callback(index);
    }
}
