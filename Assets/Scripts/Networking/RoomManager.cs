using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject networkingObject;
    public GameObject launcherObject;
    private bool hasSetNick;
    private List<TMP_Text> allPlayerNames = new List<TMP_Text>();
    private List<RoomButton> allRoomButtons = new List<RoomButton>();

    void Start()
    {
        if (GetNetworkingService().isMultiplayer)
        {
            PhotonNetwork.ConnectUsingSettings();
        }    
    }

    private NetworkingService GetNetworkingService()
    {
        return networkingObject.GetComponent<NetworkingService>();
    }

    private Launcher GetLauncher()
    {
        return launcherObject.GetComponent<Launcher>();
    }

    public override void OnConnectedToMaster()
    {
        Launcher launcher = GetLauncher();

        launcher.CloseMenus();
        launcher.menuButtons.SetActive(true);
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;

        launcher.loadingText.text = "Joining Lobby...";
    }

    public override void OnJoinedLobby()
    {
        Launcher launcher = GetLauncher();

        launcher.CloseMenus();
        launcher.menuButtons.SetActive(true);

        PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

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

    public void OpenRoomCreate()
    {
        Launcher launcher = GetLauncher();

        launcher.CloseMenus();
        launcher.createRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        Launcher launcher = GetLauncher();

        if (!string.IsNullOrEmpty(launcher.roomNameInput.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 8;
            PhotonNetwork.CreateRoom(launcher.roomNameInput.text, options);

            launcher.CloseMenus();
            launcher.loadingText.text = "Creating room...";
            launcher.loadingScreen.SetActive(true);
        }
    }

    public override void OnJoinedRoom()
    {
        Launcher launcher = GetLauncher();

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
        Launcher launcher = GetLauncher();

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
        Launcher launcher = GetLauncher();

        TMP_Text newPlayerLabel = Instantiate(launcher.playerNameLabel, launcher.playerNameLabel.transform.parent);
        newPlayerLabel.text = newPlayer.NickName;
        newPlayerLabel.gameObject.SetActive(true);

        allPlayerNames.Add(newPlayerLabel);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListAllPlayers();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Launcher launcher = GetLauncher();

        launcher.CloseMenus();
        launcher.errorScreen.SetActive(true);

        launcher.errorText.text = "Failed to Create Room: " + message;
    }

    public void LeaveRoom()
    {
        Launcher launcher = GetLauncher();

        PhotonNetwork.LeaveRoom();
        launcher.CloseMenus();
        launcher.loadingText.text = "Leaving Room";
        launcher.loadingScreen.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        Launcher launcher = GetLauncher();

        launcher.CloseMenus();
        launcher.menuButtons.SetActive(true);
    }

    public void OpenRoomBrowser()
    {
        Launcher launcher = GetLauncher();

        launcher.CloseMenus();
        launcher.roomBrowserScreen.SetActive(true);
    }

    public void CloseRoomBrowser()
    {
        Launcher launcher = GetLauncher();

        launcher.CloseMenus();
        launcher.menuButtons.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Launcher launcher = GetLauncher();

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

                allRoomButtons.Add(newButton);
            }
        }
    }

    public void JoinRoom(RoomInfo inputInfo)
    {
        Launcher launcher = GetLauncher();

        PhotonNetwork.JoinRoom(inputInfo.Name);

        launcher.CloseMenus();
        launcher.loadingText.text = "Joining Room";
        launcher.loadingScreen.SetActive(true);
    }

    public void SetNickname()
    {
        Launcher launcher = GetLauncher();

        if (!string.IsNullOrEmpty(launcher.nameInput.text))
        {
            PhotonNetwork.NickName = launcher.nameInput.text;

            launcher.CloseMenus();
            launcher.menuButtons.SetActive(true);

            hasSetNick = true;
        }
    }

    public void StartGame()
    {
        Launcher launcher = GetLauncher();

        PhotonNetwork.LoadLevel(launcher.levelToPlay);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Launcher launcher = GetLauncher();

        if (PhotonNetwork.IsMasterClient)
        {
            launcher.startButton.SetActive(true);
        }
        else
        {
            launcher.startButton.SetActive(false);
        }
    }
}
