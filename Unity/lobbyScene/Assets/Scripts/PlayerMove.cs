using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPunCallbacks
{
    //public PhotonView PV;
    float hAxis;
    float vAxis;
    public int playerId;

    Vector3 moveVec;

    private void Start()
    {
        playerId = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            hAxis = Input.GetAxisRaw("Horizontal") * 5f * Time.deltaTime;
            vAxis = Input.GetAxisRaw("Vertical") * 5f * Time.deltaTime;
            transform.Translate(hAxis, 0, vAxis);
        }
    }
}