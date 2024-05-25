using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tldmschl81_LightControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Input.mousePosition - transform.position;

        float angle = Vector2.Angle(Vector2.right, dir);
        if (dir.y < 0)
        {
            transform.rotation = Quaternion.Euler(angle-20f, transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(-angle+20f, transform.rotation.eulerAngles.y, 0);
        }
    }
}
