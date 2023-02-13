using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AnimControl : MonoBehaviourPun
{
    public bool isTrue = false;
    BoxCollider bc;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrue)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        anim.SetBool("isOpen", true);
        StartCoroutine(upCollider());
        StopCoroutine(upCollider());
    }

    IEnumerator upCollider()
    {
        while(bc.bounds.center.y <= 9)
        {
            bc.center += Vector3.up;
            yield return new WaitForSeconds(1f);
        }
    }
}
