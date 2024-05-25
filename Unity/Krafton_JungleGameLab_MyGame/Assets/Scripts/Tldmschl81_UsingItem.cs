using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum ItemType
{
    Ladder,
    Lantern,
    key
};

public class Tldmschl81_UsingItem : MonoBehaviour
{
    [SerializeField] Tldmschl81_PlayerController playerController;
    [SerializeField] Vector3 havingOffset;

    public bool isHave = false;
    public bool isUse = false;
    public ItemType type;

    void Update()
    {
        if (isHave)
        {
            Vector3 viewportPos = Camera.main.ScreenToWorldPoint(transform.position);
            transform.position = new Vector3(viewportPos.x + havingOffset.x, viewportPos.y+havingOffset.y, 0);
        }
        if (type == ItemType.Ladder)
        {
            isUse = playerController.useLadder ? true : false;
        }
        else if(type == ItemType.Lantern)
        {
            isUse = playerController.useLantern ? true : false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case ItemType.Ladder:
                    playerController.havingLadder = true;
                break;

                case ItemType.Lantern:
                    playerController.havingLantern = true;
                break;
                case ItemType.key:
                    playerController.havingKey = true;
                break;
            }
            isHave = true;
        }
    }
}
