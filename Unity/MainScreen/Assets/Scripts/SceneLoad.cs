using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    public Slider progressbar;
    public Text loadtext;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    // 비동기 모드 (LoadScene Async)를 위한 코루틴 사용
    // LoadScene()으로 Scene을 불러오면 완료될때까지 다른작업 수행 X
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Play");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {   // 아직 작업이 완료되지 않았다면,
            yield return null;
            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if (progressbar.value >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if(progressbar.value >= 1f)
            {
                loadtext.text = "Press SpaceBar";
            }

            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {   // alooSceneActivation이 fasle인 상태에서 progress가 0.9까지 차있고 value가 1이상이 되어 스페이스바가 눌렸다면
                operation.allowSceneActivation = true;
            }   // 씬 이동 진행
        }
    }
}
