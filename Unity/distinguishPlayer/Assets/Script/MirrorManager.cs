using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Random = UnityEngine.Random;

public class MirrorManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("player_", new Vector3(0f,0f,0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
