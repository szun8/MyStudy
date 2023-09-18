using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;    // 아래에 만든 클래스를 그대로 타입으로 활용하여 배열 변수 선언

    float timer;
    int level;  // 10초마다 레벨이 하나씩 증가

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // 주의. children은 자기 자신도 포함이기에 부모(Spawner)에 대한 정보도 가져온다
        // 즉 진짜 자식 오브젝트들만 사용하고자한다면 0번째를 제외한 1번째부터 사용해야 정상작동이 된다
    }

    private void Update()
    {
        if (!GameManager.instance.isLive) return;   // 시간이 0배속 처리가 되었으면 소환 타이밍을 멈추기

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length-1);
        // 소수점 버리고 int화, 시간이 지날수록 설정된 레벨보다 큰 숫자로 가면 가져올 데이터가 없어서 에러가 발생하므로 에러 방지 min 설저
        if(timer > spawnData[level].spawnTime){
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);    // 프리팹하나로 계속 재활용
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 마구 지정한 16개의 point중 하나의 위치에서 적이 소환될것임
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{   // 데이터를 enemy.cs에 전달해줄것임
    public float spawnTime;
    public int spawnType;
    public int health;
    public float speed;
}
