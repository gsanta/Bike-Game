using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerPanel : MonoBehaviour
{

    [HideInInspector] public Launcher launcher;

    public void OpenRoomBrowser()
    {
        launcher.CloseMenus();
        launcher.roomBrowserScreen.SetActive(true);
    }

    public void OpenRoomCreate()
    {
        launcher.CloseMenus();
        launcher.createRoomScreen.SetActive(true);
    }
}
