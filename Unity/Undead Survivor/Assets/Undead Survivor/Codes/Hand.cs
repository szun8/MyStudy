using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0f);
    Vector3 rightPosReverse = new Vector3(-0.1f, -0.15f, 0f);

    Quaternion leftRot = Quaternion.Euler(0, 0, -35f);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135f);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
        // 자기 자신한테도 SpriteRenderer가 포함되어있으면 0번째가 본인이고 그다음으로 부모를 가져온다
    }

    private void LateUpdate()
    {   // 플레이어가 오른쪽을 바라보느냐 왼쪽을 바라보느냐에 따른 무기 회전 및 위치 이동 필요
        bool isReverse = player.flipX; // 처음에는 오른쪽을 바라보고 있는 상태로 false
        if (isLeft)
        {   // 왼손인경우 - 근접무기
            transform.localRotation = isReverse ? leftRotReverse : leftRot; // 플레이어가 왼쪽을 보고 있으면 reverse값 적용
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {   // 오른손인경우 - 원거리무기
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
