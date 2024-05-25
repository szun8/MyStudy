using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingType
{
    Vertical,
    Horizontal
};

public class Tldmschl81_MovingStep : MonoBehaviour
{
    [SerializeField] MovingType m_type;
    [SerializeField] float leftSpot, rightSpot;
    [SerializeField] float topSpot, bottomSpot;
    [SerializeField] float speed;
    [SerializeField] Tldmschl81_PressBtn pressBtn;
    [SerializeField] Tldmschl81_PlayerController playerController;

    float offsetX;
    BoxCollider2D boxCollider;

    float dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        offsetX = boxCollider.offset.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressBtn == null || pressBtn.isPressed)
        {
            if (m_type == MovingType.Vertical)
            {
                MoveStep(Vector2.up, transform.position.y, bottomSpot, topSpot);
            }
            else if (m_type == MovingType.Horizontal)
            {
                MoveStep(Vector2.right, transform.position.x, leftSpot, rightSpot);
            }
        }

        
    }

    void MoveStep(Vector2 moveDir, float stepPos, float minSpot, float maxSpot)
    {
        if (minSpot >= stepPos)
        {
            dir = 1;
        }
        else if (maxSpot <= stepPos)
        {
            dir = -1;
        }

        transform.Translate(moveDir * Time.deltaTime * speed * dir);
    }
}
