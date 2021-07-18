using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomButton : MonoBehaviour
{
    public TMP_Text buttonText;
    public NetworkingService networkingService;

    private RoomInfo info;

    public void SetButtonDetails(RoomInfo inputInfo)
    {
        info = inputInfo;

        buttonText.text = info.Name;
    }

    public void OpenRoom()
    {
        networkingService.GetRoomManager().JoinRoom(info);
    }
}
