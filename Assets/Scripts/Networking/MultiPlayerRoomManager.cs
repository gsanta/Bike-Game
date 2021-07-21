using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiPlayerRoomManager : MonoBehaviourPunCallbacks
{
    public GameObject networkingObject;
    public GameObject launcherObject;
    public Launcher launcher;
    public RoomBrowserPanel roomBrowserPanel;
    public bool isActive = false;

    private bool hasSetNick;
    private List<TMP_Text> allPlayerNames = new List<TMP_Text>();


    public void Connect() 
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {

        if (isActive)
        {
            launcher.CloseMenus();
            launcher.menuButtons.SetActive(true);
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;

            launcher.loadingText.text = "Joining Lobby...";
        }
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

        launcher.CloseMenus();
        launcher.menuButtons.SetActive(true);

        if (!hasSetNick)
        {
            Debug.Log("Nick not set");
            launcher.CloseMenus();
            launcher.nameInputScreen.SetActive(true);

            if (PlayerPrefs.HasKey("playerName"))
            {
                launcher.nameInput.text = PlayerPrefs.GetString("playerName");
            }
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
        }
    }

    public void CreateRoom(string name, RoomOptions roomOptions)
    {
        PhotonNetwork.CreateRoom(name, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        launcher.CloseMenus();
        launcher.roomScreen.SetActive(true);

        launcher.roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        ListAllPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            launcher.startButton.SetActive(true);
        }
        else
        {
            launcher.startButton.SetActive(false);
        }
    }

    private void ListAllPlayers()
    {
        foreach (TMP_Text player in allPlayerNames)
        {
            Destroy(player.gameObject);
        }

        allPlayerNames.Clear();

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            TMP_Text newPlayerLabel = Instantiate(launcher.playerNameLabel, launcher.playerNameLabel.transform.parent);
            newPlayerLabel.text = players[i].NickName;
            newPlayerLabel.gameObject.SetActive(true);

            allPlayerNames.Add(newPlayerLabel);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        TMP_Text newPlayerLabel = Instantiate(launcher.playerNameLabel, launcher.playerNameLabel.transform.parent);
        newPlayerLabel.text = newPlayer.NickName;
        newPlayerLabel.gameObject.SetActive(true);

        allPlayerNames.Add(newPlayerLabel);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(launcher.levelToPlay);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListAllPlayers();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        launcher.CloseMenus();
        launcher.errorScreen.SetActive(true);

        launcher.errorText.text = "Failed to Create Room: " + message;
    }

    public override void OnLeftRoom()
    {
        launcher.CloseMenus();
        launcher.menuButtons.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        roomBrowserPanel.UpdateRoomList(roomList);
    }

    public void JoinRoom(string roomName)
    {
        Debug.Log("Join room multiplayer");
        PhotonNetwork.JoinRoom(roomName);
    }

    public void SetNickname()
    {
        if (!string.IsNullOrEmpty(launcher.nameInput.text))
        {
            PhotonNetwork.NickName = launcher.nameInput.text;

            launcher.CloseMenus();
            launcher.menuButtons.SetActive(true);

            hasSetNick = true;
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            launcher.startButton.SetActive(true);
        }
        else
        {
            launcher.startButton.SetActive(false);
        }
    }

    public void QuickJoin()
    {
        PhotonNetwork.CreateRoom("Test");
    }
}
