using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] //직렬화
    private GameObject poop;
    [SerializeField]
    private GameObject pHop;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatepoopRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    IEnumerator CreatepoopRoutine()
    {
        while (true)
        {
            CreatePoop();
            yield return new WaitForSeconds(1.5f); //대기시간 1초후 실행
        }
        
    }
    private void CreatePoop()
    {
        Vector3 pos = new Vector3(0,6,0);
        Instantiate(poop, pos, Quaternion.identity);
        Instantiate(pHop, pos, Quaternion.identity);
    }
}
