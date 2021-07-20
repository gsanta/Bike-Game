using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerRoomManager : MonoBehaviourPunCallbacks
{
    public Launcher launcher;
    public bool isActive = false;

    public SinglePlayerRoomManager(Launcher _launcher)
    {
        launcher = _launcher;
    }

    public void Connect()
    {
        PhotonNetwork.OfflineMode = true;
    }

    public override void OnConnectedToMaster()
    {
        if (isActive)
        {
            launcher.CloseMenus();
            QuickJoin();
            JoinRoom("Test");
            StartGame();
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);

        launcher.CloseMenus();
        launcher.loadingText.text = "Joining Room";
        launcher.loadingScreen.SetActive(true);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(launcher.levelToPlay);
    }

    public void QuickJoin()
    {
        PhotonNetwork.CreateRoom("Test");
    }
}
