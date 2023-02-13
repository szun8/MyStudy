using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public PhotonView player;
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.Instantiate(player.name, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
