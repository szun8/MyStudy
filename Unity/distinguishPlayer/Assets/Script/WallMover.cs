using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WallMover : MonoBehaviourPun
{
    private Vector3 wallMover = new Vector3(0, 0.1f, 0);
    public bool isMover = false;
    public PhotonView PV;

    // Update is called once per frame
    void Update()
    {
        PV.RPC("PlayWall", RpcTarget.AllBuffered);
        
    }

    [PunRPC]
    void PlayWall()
    {
        if (PV.IsMine)
        {
            if (isMover)
            {
                //PV.RPC("playDownWall", RpcTarget.AllBuffered);
                StopCoroutine(upWall());
                StartCoroutine(downWall());
            }
            else
            {
                StopCoroutine(downWall());
                if (transform.position.y < -0.5) //PV.RPC("playUpWall", RpcTarget.AllBuffered);
                    StartCoroutine(upWall());
            }
        }
    }

    IEnumerator downWall()
    {
        while (transform.position.y > -7)
        {
            transform.position -= wallMover;
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator upWall()
    {
        while (transform.position.y > -8.5)
        {
            transform.position += wallMover;
            if (transform.position.y > -0.5f)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
