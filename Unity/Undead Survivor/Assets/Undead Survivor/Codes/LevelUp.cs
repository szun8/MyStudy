using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next(); // 어떤 아이템이 판넬 창에 보여질 것인가를 선택하는 함수 호출
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {   // 게임 시작시 기본무기 지급을 위한 함수 -> 게임매니저의 start에서 호출될 예(임시)
        items[index].OnClick();
    }

    void Next()
    {
        // 1. 모든 아이템 비활성화
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. 모든 아이템중 3개만 랜덤으로 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[0] != ran[2] && ran[2] != ran[1]) break;    // 모든 아이템이 겹치지 않는 경우 탈출
        }
        Debug.Log(ran[0]);
        Debug.Log(ran[1]);
        Debug.Log(ran[2]);
        for (int index = 0; index < ran.Length; ++index)
        {
            Item ranItem = items[ran[index]];

            if(ranItem.level == ranItem.data.damages.Length)
            {   // 3. 만렙이 된 아이템의 경우는 소비 아이템으로 대체
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
