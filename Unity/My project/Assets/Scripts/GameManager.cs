using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance 
    {   // poop에서 ground와 충돌될 때마다 score 증가함수 호출을 위한 public설정
        get{
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField] //직렬화
    private GameObject poop;

    private int score;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text bestScore;

    [SerializeField]
    private GameObject panel;

    public bool stopTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("BestScore");
        PlayerPrefs.SetInt("BestScore", 0);
    }

    private void Init()
    {
        stopTrigger = true;
        score = 0;
        scoreTxt.text = "Score : " + score;
    }
    
    public void GameStart()
    {
        Init();
        StartCoroutine(CreatepoopRoutine());
        panel.SetActive(false);     // 게임이 시작되면 판넬 비활성
    }

    public void Score()
    {
        score++;
        scoreTxt.text = "Score : " + score;
        //Debug.Log("score : " + score);
    }

    public void GameOver()
    {
        stopTrigger = false;
        StopCoroutine(CreatepoopRoutine());

        if(score > PlayerPrefs.GetInt("BestScore",0))
            PlayerPrefs.SetInt("BestScore", score);

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        GameObject[] clone = GameObject.FindGameObjectsWithTag("poop");
        // 게임오버가 된 상태에서 화면에 게임오버 직전에 나와있던 poop clone들을 일괄 삭제로 게임오버 이후에 score 상승 경우 제거
        for (int i = 0; i < clone.Length; i++){
            Destroy(clone[i]);
        }

        panel.SetActive(true);
    }

    IEnumerator CreatepoopRoutine()
    {
        while (stopTrigger)
        {
            CreatePoop();
            yield return new WaitForSeconds(1); //대기시간 1초후 실행
        }
        
    }
    private void CreatePoop()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 0.0f));
        // 똥 생성 위치 랜덤 vector3(x, y, z)
        pos.z = 0.0f;
        Instantiate(poop, pos, Quaternion.identity);
    }
}
