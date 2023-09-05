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
    Collider2D coll;
    Animator anim;
    SpriteRenderer sprite;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth; // 죽어서 비활성화된 친구가 다시 활성화되는 과정일때 체력 재조정해주기
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;   // knockBack()을 위해 hit됐을때는 잠시 움직임을 멈춰줌

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
        if (!collision.CompareTag("Bullet") || !isLive) // 간발의 차로 두번 트리거 함수에 입장했을 때의 에러를 대비해 중복 체크
            return;

        health -= collision.GetComponent<Bullet>().damage;  // 적과 무기가 만났기때문에 피격 true
        StartCoroutine(KnockBack());

        if(health > 0)
        {
            // is Live !, Hit Action
            anim.SetTrigger("Hit");
        }
        else
        {   // 죽었을때 이렇게 변수값이 바뀌면 다시 재활용될때의 변수값도 원상 복귀 시켜줘야함
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            sprite.sortingOrder = 1; // 살아있는 애들을 가리지 않기 위해 한단계 낮춰주기
            anim.SetBool("Dead", true);

            // 몬스터의 사망과 동시에 플레이어의 킬수와 경험치도 증가시켜줘야함
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {   // 피격을 당하면 플레이어의 반대 방향으로 튕튕 ~
        yield return wait;  // 다음 하나의 물리 프레임 딜레이주기
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 reverseDirPlayer = transform.position - playerPos;  // 플레이어와의 반대 방향 구하기
        rigid.AddForce(reverseDirPlayer.normalized * 3, ForceMode2D.Impulse); // 위에서 구한 dir자체에는 크기도 가지고 있기에 정확하게 방향만 가지기 위해서는 정규화 작업을 해줘서 값 제공
    }

    void Dead()
    {
        gameObject.SetActive(false);    // 해당 오브젝트 비활성화
    }
}
