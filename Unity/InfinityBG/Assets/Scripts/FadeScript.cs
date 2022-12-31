using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class FadeScript : MonoBehaviour
{
    public Image Panel;
    float time = 0f;    // 0부터 1까지 지속시간
    float F_time = 1f;  // 얼마동안 페이드가 진행될지 
     
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);   // 0~1까지 부드럽게 만들어주는 기능, smoothStep써도됨
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;

        yield return new WaitForSeconds(1f);
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);   // 0~1까지 부드럽게 만들어주는 기능, smoothStep써도됨
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
