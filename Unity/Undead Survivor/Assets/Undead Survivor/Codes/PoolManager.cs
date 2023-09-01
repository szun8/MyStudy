using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹을 보관할 변수
    public GameObject[] prefabs;
    // 풀을 담당하는 리스트
    List<GameObject>[] pools;

    private void Awake()
    {
        // 생성한 프리팹의 사이즈만큼 리스트도 생성되어야함
        pools = new List<GameObject>[prefabs.Length];   // 리스트 배열 자체의 사이즈 초기화 
        for (int index = 0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();      // 리스트 내부 요소 자체 초기화
        }
    }

    public GameObject Get(int index)    // pools list안에서 놀고있는 몇번째를 선택해서 풀링할 것인가 인덱스를 매개변수로
    {
        GameObject select = null;
        // . 선택한 풀의 놀고(비활성화된) 있는 게임오브젝트 접근
        // .. 발견하면 select 변수에 할당
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {   // 비활성화된 아이템
                select = item;
                select.SetActive(true);  // 선택되었으니 필드에 나가서 뛰어놀아! 활성화
                break;
            }
        }
        // .. 못찾으면 새롭게 생성해 select 변수에 할당
        if(!select)
        {   // 데이터가 없으면 false를 반환
            select = Instantiate(prefabs[index], transform);    // 하이라키창에서 내 자식으로 적을 생성하겠다
            pools[index].Add(select);
        }
        return select;
    }
}
