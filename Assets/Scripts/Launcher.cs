using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{

    public static Launcher instance;

    private void Awake()
    {
        Debug.Log("launcher: " + gameObject.name);
        instance = this;
    }

    public GameObject loadingScreen;
    public GameObject menuButtons;
    public TMP_Text loadingText;
    public GameObject createRoomScreen;
    public GameObject mainMenuScreen;
    public TMP_InputField roomNameInput;
    public GameObject roomScreen;
    public TMP_Text roomNameText, playerNameLabel;
    private List<TMP_Text> allPlayerNames = new List<TMP_Text>();

    public SinglePlayerRoomManager singlePlayerRoomManager;
    public MultiPlayerRoomManager multiPlayerRoomManager;

    public GameObject errorScreen;
    public TMP_Text errorText;

    public GameObject roomBrowserScreen;
    public RoomButton theRoomButton;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();

    public GameObject nameInputScreen;
    public TMP_InputField nameInput;
    private bool hasSetNick;

    public string levelToPlay;

    public GameObject startButton;

    public GameObject roomTestButton;

    void Start()
    {
        Debug.Log("Launcher started");
        CloseMenus();

        mainMenuScreen.SetActive(true);

#if UNITY_EDITOR
        roomTestButton.SetActive(true);
#endif
    }

    public void OnSinglePlayerModeSelected()
    {
        CloseMenus();
        singlePlayerRoomManager.isActive = true;
        multiPlayerRoomManager.isActive = false;
        singlePlayerRoomManager.Connect();
    }

    public void OnMultiplayerModeSelected()
    {
        singlePlayerRoomManager.isActive = false;
        multiPlayerRoomManager.isActive = true;
        multiPlayerRoomManager.Connect();
    }

    public void CloseMenus()
    {
        loadingScreen.SetActive(false);
        menuButtons.SetActive(false);
        createRoomScreen.SetActive(false);
        roomScreen.SetActive(false);
        errorScreen.SetActive(false);
        roomBrowserScreen.SetActive(false);
        nameInputScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
    } 

    public void CloseErrorScreen()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    public void QuickJoin()
    {
        PhotonNetwork.CreateRoom("Test");
        CloseMenus();
        loadingText.text = "Creating Room";
        loadingScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
