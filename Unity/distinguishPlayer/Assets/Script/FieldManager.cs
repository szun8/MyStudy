using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Random = UnityEngine.Random;

public class FieldManager : MonoBehaviourPunCallbacks
{
    public static FieldManager instance;
    public Action OnSettingID; // action : 콜백함수의 기능
    public int PlayerID = -1;

    Vector3[] spawnSpot = new Vector3[2];
    public GameObject player;

    private void Awake() {
        instance = this;
        spawnSpot[0] = new Vector3(-3f, 0.7f, -20f);
        spawnSpot[1] = new Vector3(3f, 0.7f, -20f);

    }

    void Start()
    {
        if(PlayerID < 1)
        {
            player = PhotonNetwork.Instantiate("player_", spawnSpot[0], Quaternion.identity);
            PhotonNetwork.Instantiate("Enemy A", new Vector3(spawnSpot[0].x, spawnSpot[0].y, spawnSpot[0].z - 3f), Quaternion.identity);
        }
        else
        {
            player = PhotonNetwork.Instantiate("player_", spawnSpot[1], Quaternion.identity);
            PhotonNetwork.Instantiate("Enemy A", new Vector3(spawnSpot[1].x, spawnSpot[1].y, spawnSpot[1].z - 3f), Quaternion.identity);
        }
        NumberingID();

        OnSettingID();
        print("FieldManager : " + PlayerID);
    }

    public int GetPlayerID() {
        return PlayerID;
    }

    private void NumberingID()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        Player[] sortedPlayers = PhotonNetwork.PlayerList;

        for (int i = 0; i < sortedPlayers.Length; ++i)
        {
            if (sortedPlayers[i].ActorNumber == actorNumber)
            {
                PlayerID = i;
                break;
            }
        }
    }
}
