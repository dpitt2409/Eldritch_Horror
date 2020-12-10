using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleOptionController : MVC
{
    public void StartSingleOption(string title, string text, string yesText, string noText, SingleOptionCallBack cb)
    {
        App.Model.singleOptionModel.StartSingleOption(title, text, yesText, noText, cb);
    }

    public void OptionSelected(bool response)
    {
        SingleOptionCallBack callback = App.Model.singleOptionModel.callBack;
        App.Model.singleOptionModel.FinishSingleOption();
        callback(response);
    }
}
