using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // 스캔을 할 범위(반지름)
    public LayerMask targetLayer;   // 누구(어느 오브젝트)를 스캔할 것이냐
    public RaycastHit2D[] targets;  // 근처에 감지된 타겟들
    public Transform nearestTarget; // 그 중 가장 가까운 타겟의 위치

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        // 원형의 캐스트를 쏘고 모든 결과를 반환하는 함수 : 1) 어디 위치에서 2) 얼마의 크기로 3) 어디 방향으로 4) 얼마의 거리를 둬서 5) 누구를 검출할 것이냐
        nearestTarget = GetNearest();   // 계속 가까운 타겟을 검색한다
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100f;

        foreach(RaycastHit2D target in targets)
        {   // 가장 최소의 거리로 있는 타켓의 트랜스폼이 저장되어 해당 반복문을 나올것임
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff < diff)
            {   // 현재 등록된 차이보다 더 가까운 것이 있다면 그 정보로 갱신
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
