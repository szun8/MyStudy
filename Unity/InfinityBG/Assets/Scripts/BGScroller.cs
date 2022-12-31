using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private MeshRenderer render;

    public float speed;
    private float offset;
    private Player player;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset += player.horizontal * speed * 0.1f;
        render.material.mainTextureOffset = new Vector2(offset , 0);
    }
}
