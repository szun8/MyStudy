using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // poolManager에서 받아온 근접무기를 게임씬에 소환하는 스크립트

    public int id, prefabId, count; // 무기 번호, 풀매니저속 등록된 프리팹의 번호, 몇개를 배치할것인가 
    public float damage, speed;    // 해당 무기의 피격 강도, 회전 속도

    float timer;    // 일정 속도로 나가야하기에 필요한 변수
    Player player;  // 부모 컴포넌트 가져오기

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        Init(); 
    }
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if(timer > speed)
                {   // 여기서 속도는 연사속도를 의미 = 즉 작을수록 많이 발사 -> 0.3초마다 발사하는거로 !
                    timer = 0;
                    Fire();     // 발사하는 로직함수 호출
                }
                break;
        }

        if (Input.GetButtonDown("Jump")) LevelUp(20, 5);    // test code...
    }
    public void LevelUp(float damage, int count)
    {   // 다른 스크립트에서 해당 함수 호출 예정 -> Level up을 했다면 damage랑 count가 증가했으면 좋겠어
        this.damage = damage;
        this.count += count;

        if(id == 0)
            Batch();
    }

    public void Init()
    {
        // 무기ID에 따라 로직을 분리할 switch문
        switch (id)
        {
            case 0:
                speed = 150;    // 근접무기
                Batch();
                break;
            default:
                speed = 0.3f;   // 원거리 무기
                break;
        }
    }
     
    void Batch()
    {   // 근접무기
        for(int index = 0; index < count; index++)
        {
            Transform bullet;

            if(index < transform.childCount)
            {   // 기존에 있던 무기는 새로이 가져오지 말고 세팅 
                bullet = transform.GetChild(index);
            }
            else
            {   // 더 필요하면 그때 poolmanager에서 새롭게 가져오자 
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform; // 플레이어를 따라야하기 때문에 poolManager에서 변경 -> Weapon 0 의 자식으로 쏘옥 들어감
            }
            
            bullet.localPosition = Vector3.zero;    // 레벨업에 따른 무기 생성시 이상한 위치에 생기는것을 수정 = 즉 플레이어의 위치에서 무기가 생성됨
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count; // 4개라면 0번째는 0, 1번째는 90, 2번쨰는 180, 3번째는 270도
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);    // 회전 후 up방향으로 1.5f씩 이동하고 그것은 world로 위치한다
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // 근접무기의 관통여부는 무한이니까 -1처리
        }
    }

    void Fire()
    {   // 원거리
        if (!player.scanner.nearestTarget) return;  // 근처에 가까운 적이 없다면 총알을 발사하지 않음

        // 총알이 나아가고자하는 방향
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;   // prefab Id : 2, 총알 생성
        bullet.position = transform.position;   // 총알 위치 초기화

        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);  // 지정된 축을 중심으로 목표를 향해 회전하는 함수, z축으로 돌기위해 up방향
        bullet.GetComponent<Bullet>().Init(damage, count, dir); // 근접무기의 관통여부는 무한이니까 -1처리

    }
}
