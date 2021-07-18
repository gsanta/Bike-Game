using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerData
{
    private PhotonView photonView;
    public PlayerData(PhotonView _photonView)
    {
        photonView = _photonView;
    }

    public bool IsMine
    {
        get
        {
            return true;//photonView.IsMine;
        }
    }
}
