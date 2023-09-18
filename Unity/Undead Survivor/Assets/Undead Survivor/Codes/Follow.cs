using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
        // 월드 상에 있는 플레이어 좌표를 스크린 좌표계에 맞는 pos로 변환
        
    }
}
