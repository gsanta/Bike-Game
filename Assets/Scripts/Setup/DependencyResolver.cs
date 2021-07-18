using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyResolver : MonoBehaviour
{

    GameObject roomButtonObject;
    private NetworkingService networkingService;

    void Start()
    {
        
    }

    void Update()
    {
        
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
