using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tldmschl81_InfoUIPos : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Vector3 controlOffset;

    void Update()
    {
        if (!target.GetComponent<Tldmschl81_UsingItem>().isHave)
            transform.position = Camera.main.WorldToScreenPoint(target.transform.position) + controlOffset;
        else
            Destroy(gameObject);
    }
}
