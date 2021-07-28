using Photon.Pun;

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
            PhotonNetwork.CreateRoom("Test");
            PhotonNetwork.LoadLevel(launcher.levelToPlay);
        }
    }
}
