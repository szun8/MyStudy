using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private Animator animator;

    private SpriteRenderer renderer;

    private float speed = 3;
    private float horizontal;

    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>(); // 이동 이미지 좌우반전 적
    }

    // Update is called once per frame
    void Update(){
        horizontal = Input.GetAxis("Horizontal");
        PlayerMove();
        ScreenChk();
    }

    private void PlayerMove(){
        animator.SetFloat("speed", Mathf.Abs(horizontal));   // float파라미터에 값을 줌
        if(horizontal < 0) { // 음수면 왼쪽 보기
            renderer.flipX = true;
        } else { // 양수면 오른쪽 보기
            renderer.flipX = false;
        }
        rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y); // velocity - 컴토넌트의 속도값																	
	}

    private void ScreenChk()
    {
        Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position); // 현재 오브젝트의 월드공간에서의 위치값을 뷰포트공간으로 바꿔서 가져소
        if (worlpos.x < 0.05f) worlpos.x = 0.05f;
        if (worlpos.x > 0.95f) worlpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }
}
