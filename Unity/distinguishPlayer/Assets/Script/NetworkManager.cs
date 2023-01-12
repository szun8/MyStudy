using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text Name;

    public GameObject InfoPanel;
    public GameObject player;
    Camera playerCam;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = Name.text;
        
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 2 }, null);
    }
    public override void OnJoinedRoom()
    {
        InfoPanel.SetActive(false);
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0.7f, 0), Quaternion.identity);
    }

}
