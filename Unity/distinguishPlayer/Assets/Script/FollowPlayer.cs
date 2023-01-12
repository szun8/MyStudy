using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FollowPlayer : MonoBehaviourPunCallbacks
{
    public GameObject player;
    public GameObject InfoPanel;

    public Camera playerCam;
    private void Awake()
    {
        playerCam = player.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InfoPanel.activeInHierarchy && PhotonNetwork.IsConnected)
        {
            playerCam.enabled = true;
            this.enabled = false;
            //transform.position = player.transform.position + new Vector3(0, 2f, -6.5f);
            //transform.localEulerAngles = new Vector3(13f, 0, 0);
        }
    }
}
