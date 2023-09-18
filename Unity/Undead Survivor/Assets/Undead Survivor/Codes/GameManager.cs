using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // static으로 선언해서 바로 메모리에 올려버린다
    [Header("# Game Control")]
    public bool isLive; // 시간 컨트롤을 위한 변수
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;
        uiLevelUp.Select(0);    // 기본 무기인 삽(0) 지급
    }

    void Update()
    {
        if (!isLive) return;
        gameTime += Time.deltaTime;    // 한 프레임을 돌리는데 소요된 시간

        if (gameTime > maxGameTime)
        {   
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;
        // 최대 레벨까지만 보여주기
        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {   // 만약 레벨 0인 경우 다음 레벨로 가기 위해 exp 3가 필요! 해당 exp를 모았다면 다음 레벨로 이동하는 로직
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0f;    // 유니티의 시간을 0으로 만드는 것 -> 즉 정지를 가리킴
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1f;    // 기본 시간흐름이 1배속인 것 
    }
}
