using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tldmschl81_PlayerController : MonoBehaviour
{
    [Header("Plater-State")]
    public float speed;
    public float jumpForce;
    public float horizontalInput, verticalInput;
    public bool isOnGround = true;
    public bool isClear = false;

    [Header("Item-Ladder")]
    public bool useLadder = false;
    public bool havingLadder = false;
    public bool isWall = false;

    [Header("Item-Lantern")]
    public bool useLantern = false;
    public bool havingLantern = false;
    public List<GameObject> lanterns;
    // [0] : Lower(Default) [1] : Right(Item)
    public float lanternOffsetX;
    public float lanternOffsetZ;

    public bool havingKey = false;

    Rigidbody2D rigid;
    Tldmschl81_GameManager gm;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<Tldmschl81_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isActive) return;

        if (isClear && havingKey && Input.GetKeyDown(KeyCode.O))
        {
            gm.GameClear();
        }

        if (havingLadder && Input.GetKeyDown(KeyCode.L))
        {
            useLadder = !useLadder;
            if (!useLadder)
            {
                rigid.gravityScale = 1;
            }
        }

        if(havingLantern && Input.GetKeyDown(KeyCode.F))
        {
            useLantern = !useLantern;
            if (!useLantern)
            {
                lanterns[0].SetActive(true);
                lanterns[1].SetActive(false);
            }
            else
            {
                lanterns[0].SetActive(false);
                lanterns[1].SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || isWall))
        {   // Jump
            isOnGround = false;
            if (isWall)
            {
                useLadder = false;
                isWall = false;
                rigid.gravityScale = 1;
            }
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (havingLadder && useLadder && isWall)
        {
            rigid.gravityScale = 0;
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector2.up * Time.deltaTime * speed * verticalInput);
        }
        else
        {
            // Move Horizontal
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
            if(useLantern)
            {
                if(horizontalInput > 0)
                {
                    lanterns[1].transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    lanterns[1].transform.position = new Vector3(transform.position.x + lanternOffsetX, lanterns[1].transform.position.y, lanternOffsetZ);
                }
                else if (horizontalInput < 0)
                {
                    lanterns[1].transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                    lanterns[1].transform.position = new Vector3(transform.position.x - lanternOffsetX, lanterns[1].transform.position.y, lanternOffsetZ);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = 1;
            isOnGround = true;
            isWall = false;
            useLadder = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (useLadder)
            {
                isWall = true;
                rigid.gravityScale = 0;
            }
            isOnGround = false;
        }
        else if (collision.gameObject.CompareTag("GameOver"))
        {
            Destroy(gameObject);
            Debug.Log("GameOver=!");
            gm.over.gameObject.SetActive(true);
            gm.restart.gameObject.SetActive(true);
            gm.exit.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = 1;
            isWall = false;
        }
        if (collision.gameObject.CompareTag("Clear"))
        {
            isClear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Clear"))
        {
            isClear = false;
        }
    }
}
