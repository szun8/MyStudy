using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class playerMove : MonoBehaviourPun
{
    float x, z;
    public PhotonView PV;
    public TextMeshPro Nickname;
    public Camera playerCam;

    void Awake()
    {
        Nickname.text = PV.IsMine ? PhotonNetwork.LocalPlayer.NickName : PV.Owner.NickName;
        //Nickname.text = PhotonNetwork.PlayerList[0].NickName;
        Nickname.color = PV.IsMine ? Color.green : Color.red;
        playerCam.enabled = PV.IsMine ? true : false;
    }
    void Update()
    {
        //if(Nickname.text != PhotonNetwork.PlayerList[0].NickName)
        //{   // 만약 host가 나가고 두번째 player가 host가 된다면 ?
        //    Nickname.text = PhotonNetwork.PlayerList[0].NickName;
        //}
        if (PV.IsMine)
        {
            x = Input.GetAxis("Horizontal") * Time.deltaTime * 7f;
            z = Input.GetAxis("Vertical") * Time.deltaTime * 7f;

            transform.Translate(x, 0, z);
        }
    }
}
