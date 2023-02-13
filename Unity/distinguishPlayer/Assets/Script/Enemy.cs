using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

public class Enemy : MonoBehaviourPunCallbacks
{
    Rigidbody rigid;
    Transform target;
    Animator anim;
    NavMeshAgent nav;
    
    float distances;

    public PhotonView PV;
    public bool isChase = true;

    private void Start()
    {
        target = GameObject.Find("FieldManger").GetComponent<FieldManager>().player.GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }


    [PunRPC]
    void RpcChase(float distance)
    {
        if (PV.IsMine)
        {
            if (distance < 2f)
            {
                anim.SetBool("isWalk", false);
                isChase = false;
            }
            else
            {
                anim.SetBool("isWalk", true);
                isChase = true;
            }
        }
    }

    private void Update()
    {
        if (target == null) return;
        if (PV.IsMine)
        {
            distances = Vector3.Distance(target.position, transform.position);

            PV.RPC("RpcChase", RpcTarget.AllBuffered, distances);

            if (isChase)
            {
                nav.SetDestination(target.position);
            }
        }
        
        
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
        //PV.RPC("FreezeVelocity", RpcTarget.All);
    }

    //[PunRPC]
    void FreezeVelocity()
    {   // 플레이어와의 충돌로 인한 물리충돌 오류 제거
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
