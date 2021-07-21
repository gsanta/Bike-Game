using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPanel : MonoBehaviour
{

    [HideInInspector] public Launcher launcher;
    [HideInInspector] public MultiPlayerRoomManager multiPlayerRoomManager;

    public void LeaveRoom()
    {
        multiPlayerRoomManager.LeaveRoom();
        launcher.CloseMenus();
        launcher.loadingText.text = "Leaving Room";
        launcher.loadingScreen.SetActive(true);
    }

    public void StartGame()
    {
        multiPlayerRoomManager.StartGame();
    }

    public void JoinRoom(string roomName)
    {
        multiPlayerRoomManager.JoinRoom(roomName);

        launcher.CloseMenus();
        launcher.loadingText.text = "Joining Room";
        launcher.loadingScreen.SetActive(true);
    }
}
