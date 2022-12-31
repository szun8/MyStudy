using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontal, speed = 3;
    private Rigidbody2D rigidbody;
    private SpriteRenderer renderer;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0)
        { // 음수면 왼쪽 보기
            renderer.flipX = true;
        }
        else
        { // 양수면 오른쪽 보기
            renderer.flipX = false;
        }
        rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y); // velocity - 컴토넌트의 속도값

        ScreenChk();
    }

    private void ScreenChk()
    {
        Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position); // 현재 오브젝트의 월드공간에서의 위치값을 뷰포트공간으로 바꿔서 가져소
        if (worlpos.x < 0.05f) worlpos.x = 0.05f;
        if (worlpos.x > 0.95f) worlpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }
}
