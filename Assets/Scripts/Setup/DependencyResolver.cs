using UnityEngine;

public class DependencyResolver : MonoBehaviour
{

    public GameObject roomButtonObject;
    public GameObject launcherObject;
    public NetworkingService networkingService;
    public GameObject singlePlayerRoomManagerObject;
    public GameObject multiPlayerRoomManagerObject;
    public GameObject selectNameButtonObject;

    public static DependencyResolver instance;

    private void Awake()
    {
        instance = this;
        Launcher launcher = launcherObject.GetComponent<Launcher>();
        SinglePlayerRoomManager spRoomManager = singlePlayerRoomManagerObject.GetComponent<SinglePlayerRoomManager>();
        MultiPlayerRoomManager mpRoomManager = multiPlayerRoomManagerObject.GetComponent<MultiPlayerRoomManager>();
        spRoomManager.launcher = launcher;
        mpRoomManager.launcher = launcher;
        launcher.singlePlayerRoomManager = spRoomManager;
        launcher.multiPlayerRoomManager = mpRoomManager;

        SelectNameButton selectNameButton = selectNameButtonObject.GetComponent<SelectNameButton>();
        selectNameButton.multiPlayerRoomManager = mpRoomManager;
    }

    public void OnNetworkingServiceReady(NetworkingService _networkingService)
    {
        networkingService = _networkingService;
        GetRoomButton().networkingService = networkingService;
    }

    public RoomButton GetRoomButton()
    {
        return roomButtonObject.GetComponent<RoomButton>();
    }
}
