using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class RoomBrowserPanel : MonoBehaviour
{

    [HideInInspector] public MultiPlayerRoomManager multiPlayerRoomManager;
    [HideInInspector] public Launcher launcher;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();

    public void ClosePanel()
    {
        launcher.CloseMenus();
        launcher.menuButtons.SetActive(true);
    }

    public void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomButton rb in allRoomButtons)
        {
            Destroy(rb.gameObject);
        }

        allRoomButtons.Clear();
        launcher.theRoomButton.gameObject.SetActive(false);

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount != roomList[i].MaxPlayers && !roomList[i].RemovedFromList)
            {
                RoomButton newButton = Instantiate(launcher.theRoomButton, launcher.theRoomButton.transform.parent);
                newButton.SetButtonDetails(roomList[i]);
                newButton.gameObject.SetActive(true);
                newButton.multiPlayerRoomManager = multiPlayerRoomManager;

                allRoomButtons.Add(newButton);
            }
        }
    }
}
