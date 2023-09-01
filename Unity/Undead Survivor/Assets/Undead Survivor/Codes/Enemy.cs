using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed, health, maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth; // 죽어서 비활성화된 친구가 다시 활성화되는 과정일때 체력 재조정해주기
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = target.position - rigid.position;  // 위치차이
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;    // 가야할 방향
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; //부딪혔을때 밀려나는 물리력을 없앰 
    }

    void LateUpdate()
    {
        if (!isLive) return;

        sprite.flipX = target.position.x < rigid.position.x;    // 내가 플레이어(타겟)보다 오른쪽에 있으면 flip하고 왼쪽에 있으면 flip하지 말아라!
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spawnType]; // 맞는 타입의 em=nemy controller로 설정해주기
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;  // 적과 무기가 만났기때문에 피격 true
        if(health > 0)
        {
            // is Live !, Hit Action
        }
        else
        {   
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);    // 해당 오브젝트 비활성화
    }
}
