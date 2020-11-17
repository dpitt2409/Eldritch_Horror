using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuModel : MVC
{
    public List<GameObject> openMenus;

    public bool testing = false;

    public void MenuOpened(GameObject menu)
    {
        openMenus.Add(menu);
    }

    public void MenuClosed()
    {
        openMenus.RemoveAt(openMenus.Count-1);
    }

    public void StartTesting()
    {
        testing = true;
    }

    public void FinishTesting()
    {
        testing = false;
    }
}
