using Photon.Realtime;
using UnityEngine;

public class CreateRoomPanel : MonoBehaviour
{
    [HideInInspector] public Launcher launcher;
    [HideInInspector] public MultiPlayerRoomManager multiPlayerRoomManager;

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(launcher.roomNameInput.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 8;

            multiPlayerRoomManager.CreateRoom(launcher.roomNameInput.text, options);

            launcher.CloseMenus();
            launcher.loadingText.text = "Creating room...";
            launcher.loadingScreen.SetActive(true);
        }
    }
}
