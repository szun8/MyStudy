using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    public TextMeshProUGUI txt;
    Vector3 defaultScale;

    public CanvasGroup mainG;
    public CanvasGroup optionG;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    bool isSound;
    public void onBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                SceneLoader.LoadSceneHandle("Play", 0);
                Debug.Log("새게임");
                break;
            case BTNType.Continue:
                SceneLoader.LoadSceneHandle("Play", 1);
                Debug.Log("이어하기");
                break;
            case BTNType.Option:
                CanvasGroupOn(optionG);
                CanvasGroupOff(mainG);
                Debug.Log("옵션");
                break;
            case BTNType.Sound:
                if (isSound){   // 토글기능
                    txt.text = "Sound OFF";
                    //soundTxt.text = "Sound OFF";
                    Debug.Log("사운드 OFF");
                }
                else {
                    txt.text = "Sound ON";
                    //soundTxt.text = "Sound ON";
                    Debug.Log("사운드 ON");
                }
                isSound = !isSound;
                break;
            case BTNType.Back:
                CanvasGroupOn(mainG);
                CanvasGroupOff(optionG);
                Debug.Log("뒤로가기");
                break;
            case BTNType.Quit:
                Application.Quit() ;
                Debug.Log("앱 종료");
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true; 
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {   // 스크립트가 붙어있는 오브젝트에 마우스가 닿으면 해당 메서드 호출
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {   // 스크립트가 붙어있는 오브젝트에 닿아있던 마우스가 해제되면 해당 메서드 호출
        buttonScale.localScale = defaultScale;
    }
}
