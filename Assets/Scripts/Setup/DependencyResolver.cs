using UnityEngine;

public class DependencyResolver : MonoBehaviour
{

    public GameObject roomButtonObject;
    public GameObject launcherObject;
    public NetworkingService networkingService;
    public GameObject singlePlayerRoomManagerObject;
    public GameObject multiPlayerRoomManagerObject;
    public GameObject selectNameButtonObject;
    public GameObject roomBrowserPanelObject;
    public GameObject multiPlayerPanelObject;
    public GameObject createRoomPanelObject;
    public GameObject roomPanelObject;

    private Launcher launcher;
    private SinglePlayerRoomManager spRoomManager;
    private MultiPlayerRoomManager mpRoomManager;
    private RoomBrowserPanel roomBrowserPanel;
    private SelectNameButton selectNameButton;
    private MultiPlayerPanel multiPlayerPanel;
    private CreateRoomPanel createRoomPanel;
    private RoomPanel roomPanel;

    public static DependencyResolver instance;

    private void Awake()
    {
        instance = this;
        launcher = launcherObject.GetComponent<Launcher>();
        spRoomManager = singlePlayerRoomManagerObject.GetComponent<SinglePlayerRoomManager>();
        mpRoomManager = multiPlayerRoomManagerObject.GetComponent<MultiPlayerRoomManager>();
        selectNameButton = selectNameButtonObject.GetComponent<SelectNameButton>();
        roomBrowserPanel = roomBrowserPanelObject.GetComponent<RoomBrowserPanel>();
        multiPlayerPanel = multiPlayerPanelObject.GetComponent<MultiPlayerPanel>();
        createRoomPanel = createRoomPanelObject.GetComponent<CreateRoomPanel>();
        roomPanel = roomPanelObject.GetComponent<RoomPanel>();

        mpRoomManager.launcher = launcher;
        mpRoomManager.roomBrowserPanel = roomBrowserPanel;

        spRoomManager.launcher = launcher;
        
        launcher.singlePlayerRoomManager = spRoomManager;
        launcher.multiPlayerRoomManager = mpRoomManager;

        selectNameButton.multiPlayerRoomManager = mpRoomManager;

        roomBrowserPanel.launcher = launcher;
        roomBrowserPanel.multiPlayerRoomManager = mpRoomManager;

        multiPlayerPanel.launcher = launcher;

        createRoomPanel.launcher = launcher;
        createRoomPanel.multiPlayerRoomManager = mpRoomManager;

        roomPanel.launcher = launcher;
        roomPanel.multiPlayerRoomManager = mpRoomManager;
    }

    public void OnNetworkingServiceReady(NetworkingService _networkingService)
    {
        networkingService = _networkingService;
    }

    public RoomButton GetRoomButton()
    {
        return roomButtonObject.GetComponent<RoomButton>();
    }
}
