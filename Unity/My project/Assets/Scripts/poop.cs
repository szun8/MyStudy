using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator =  GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 스크립트가 추가된 오브젝트가 다른 오브젝트에 접촉하면 collision을 반환
        if (collision.gameObject.tag == "Ground")
        {   // 충돌한 물체의 태그명이 Ground인가?
            GameManager.Instance.Score();
            animator.SetTrigger("poop");

            //Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player") 
        {   // 충돌한 물체의 태그명이 Player인가?
            GameManager.Instance.GameOver();
            animator.SetTrigger("poop");

            //Destroy(this.gameObject);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision){}
}
