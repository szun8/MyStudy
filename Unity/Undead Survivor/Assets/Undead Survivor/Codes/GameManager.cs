using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // static으로 선언해서 바로 메모리에 올려버린다
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;    // 한 프레임을 돌리는데 소요된 시간

        if (gameTime > maxGameTime)
        {   
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {   // 만약 레벨 0인 경우 다음 레벨로 가기 위해 exp 3가 필요! 해당 exp를 모았다면 다음 레벨로 이동하는 로직
            level++;
            exp = 0;

        }
    }
}
