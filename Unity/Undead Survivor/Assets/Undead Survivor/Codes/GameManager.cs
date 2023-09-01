using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // static으로 선언해서 바로 메모리에 올려버린다

    public float gameTime, maxGameTime = 2 * 10f;
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
}
