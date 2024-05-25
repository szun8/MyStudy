using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tldmschl81_RotatingItemBG : MonoBehaviour
{
    Vector3 rotateAngle = new Vector3 (0, 0, 30);
    float speed = 2;
    Tldmschl81_UsingItem item;
    // Start is called before the first frame update
    void Start()
    {
        item = GetComponentInParent<Tldmschl81_UsingItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (item.isUse)
        {
            transform.Rotate(rotateAngle * Time.deltaTime * speed);
        }
        else if(item.isHave)
        {
            transform.rotation = Quaternion.identity;
        }
        else{
            transform.Rotate(rotateAngle * Time.deltaTime * speed);
        }
    }
}
