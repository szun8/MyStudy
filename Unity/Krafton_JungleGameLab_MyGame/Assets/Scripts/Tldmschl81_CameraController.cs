using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tldmschl81_CameraController : MonoBehaviour
{
    [SerializeField] Tldmschl81_PlayerController playerController;
    Vector3 camOffset = new Vector3(3, 0, -10);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null)
            transform.position = playerController.transform.position + camOffset;
    }
}
