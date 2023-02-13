using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    private Rigidbody rigid;
    private Vector3 dir = Vector3.zero;
    private Vector3 camSpot;
    private int cnt = 0;

    public PhotonView PV;
    public float JumpPower = 3f, speed = 3f;

    void Awake()
    {
        camSpot = new Vector3(0f, 0.25f, 0.2f);
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            dir.x = Input.GetAxis("Horizontal");
            dir.z = Input.GetAxis("Vertical");
            dir.Normalize();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
            Camera.main.transform.position = transform.position + camSpot;

            if(cnt >= 3)
            {
                GameObject.FindGameObjectWithTag("door").GetComponent<AnimControl>().isTrue = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            if (dir != Vector3.zero)
            {
                //바라보는 방향 부호 != 가고자할 방향 부호
                if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
                {
                    transform.Rotate(0, 1, 0);
                }
                transform.forward = Vector3.Lerp(transform.forward, dir, speed * Time.deltaTime);
            }
            rigid.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("door"))
        {
            cnt++;
            Debug.Log("Enter : " + cnt);
        }
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(cnt);
        }
        else
        {
            stream.ReceiveNext();
        }
    }
}
