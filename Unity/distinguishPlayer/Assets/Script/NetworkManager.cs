using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text Name;
    public AudioClip BGM;

    private AudioSource _audio;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
        _audio = this.gameObject.AddComponent<AudioSource>();
        _audio.PlayOneShot(BGM, 0.5f);
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        // 이름이 동일하면 룸 접속 거부하는 코드를 넣어야할듯 싶음 
        PhotonNetwork.LocalPlayer.NickName = Name.text;
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 2 }, null);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("play");
    }
}
