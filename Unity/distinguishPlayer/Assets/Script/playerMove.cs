using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class playerMove : MonoBehaviourPun
{
    public float JumpPower, speed = 3f;
    public PhotonView PV;
    public TextMeshPro Nickname;
    public AudioClip JumpSound;
    public AudioClip BGM;

    private AudioSource _audio;
    private Rigidbody rigid;
    private Vector3 dir = Vector3.zero;
    private Vector3 camSpot;
    private bool isCam = false;

    void Awake()
    {
        Nickname.text = PV.IsMine ? PhotonNetwork.LocalPlayer.NickName : PV.Owner.NickName;
        //Nickname.text = PhotonNetwork.PlayerList[0].NickName;
        Nickname.color = PV.IsMine ? Color.green : Color.red;
        camSpot = new Vector3(0f, 1f, 0.4f);

        _audio = this.gameObject.AddComponent<AudioSource>();
        _audio.PlayOneShot(BGM, 0.5f);
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (!PV.IsMine) return;

        //StartCoroutine(MoveToPlayer());
        //StopCoroutine(MoveToPlayer());
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
                _audio.PlayOneShot(JumpSound, 0.5f);
            }
            Camera.main.transform.position = transform.position + camSpot;
        }
    }

    private void FixedUpdate()
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

    IEnumerator MoveToPlayer() {
        
        while (true)
        {
            Camera.main.transform.position -= new Vector3(0f, 0f, 0.1f);
            if (Camera.main.transform.position.z <= -19f)
            {
                isCam = true;
                yield break;
            }
            yield return new WaitForSeconds(0.03f);
        }
    }

}
