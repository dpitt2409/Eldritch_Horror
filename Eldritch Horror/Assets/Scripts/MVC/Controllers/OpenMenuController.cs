using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuController : MVC
{
    public void OpenMenu(GameObject menu)
    {
        // Could Hide previously open menu here if applicable?
        // that way nothing else needs to worry about closing menus
        App.Model.openMenuModel.MenuOpened(menu);
    }

    public void CloseMenu()
    {
        App.Model.openMenuModel.MenuClosed();
    }

    public void TestStarted()
    {
        App.Model.openMenuModel.StartTesting();
    }
    
    public void TestFinished()
    {
        App.Model.openMenuModel.FinishTesting();
    }

    public void MinimizeOpenMenu()
    {
        HideOpenMenu();
        App.Controller.minimizeController.WindowMinimized(MaximizeOpenMenu);
        if (App.Model.openMenuModel.testing)
        {
            App.Controller.testController.MinimizeTest();
        }
    }

    public void MaximizeOpenMenu()
    {
        EnableOpenMenu();
        if (App.Model.openMenuModel.testing)
        {
            App.Controller.testController.MaximizeTest();
        }
    }

    public void HideOpenMenu()
    {
        List<GameObject> menus = App.Model.openMenuModel.openMenus;
        if (menus.Count > 0)
            menus[menus.Count - 1].SetActive(false);
        if (App.Model.openMenuModel.testing)
        {
            App.Controller.testController.MinimizeTest();
        }
    }

    public void EnableOpenMenu()
    {
        List<GameObject> menus = App.Model.openMenuModel.openMenus;
        if (menus.Count > 0)
            menus[menus.Count - 1].SetActive(true);
        if (App.Model.openMenuModel.testing)
        {
            App.Controller.testController.MaximizeTest();
        }
    }

    public void OpenPopup(GameObject popup)
    {
        if (App.Model.openMenuModel.openPopup != null)
        {
            App.Model.openMenuModel.openPopup.SetActive(false);
        }
        App.Model.openMenuModel.OpenPopup(popup);
    }

    public void ClosePopup()
    {
        App.Model.openMenuModel.ClosePopup();
    }
}
