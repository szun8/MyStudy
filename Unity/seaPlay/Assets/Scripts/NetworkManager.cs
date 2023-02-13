using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    static NetworkManager nm;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
        if(nm == null)
        {
            nm = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        
        PhotonNetwork.Instantiate("player", new Vector3(1.75f, 0.15f, -13.5f), Quaternion.identity);
    }
}
