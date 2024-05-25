using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tldmschl81_PressBtn : MonoBehaviour
{
    public bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isPressed && collision.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y+0.07f, transform.position.z);
            isPressed = true;
        }
    }
}
