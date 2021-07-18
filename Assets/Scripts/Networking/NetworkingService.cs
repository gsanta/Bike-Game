using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingService : MonoBehaviour
{
    public GameObject roomManagerObject;
    public GameObject dependencyResolverObj;

    public bool isMultiplayer;

    private void Awake()
    {
        GetDependencyResolver().OnNetworkingServiceReady(this);
    }

    public RoomManager GetRoomManager()
    {
        return roomManagerObject.GetComponent<RoomManager>();
    }

    public DependencyResolver GetDependencyResolver()
    {
        return dependencyResolverObj.GetComponent<DependencyResolver>();
    }
}
