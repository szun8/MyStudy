using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // damage
    public float damage;
    public int per;
    // 관통여부

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per > -1)
        {   // 원거리 무기라면 per은 -1보다 클 것이고
            rigid.velocity = dir * 15f;   // 그렇게 들어온 방향을 물리적 속력을 적용해줌
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) return;

        per--;  // 한번 맞으면 관통력을 줄임
        if (per == -1)
        {
            rigid.velocity = Vector2.zero;  // 비활성화 이전에 물리 속도 재초기화해서 재활용에 용이하게 함
            gameObject.SetActive(false);
        }
    }
}
