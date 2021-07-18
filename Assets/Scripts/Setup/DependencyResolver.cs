using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyResolver : MonoBehaviour
{

    public GameObject roomButtonObject;
    public NetworkingService networkingService;

    public static DependencyResolver instance;

    private void Awake()
    {
        instance = this;
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
