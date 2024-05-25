using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tldmschl81_GameManager : MonoBehaviour
{
    public Button start, restart, exit;
    public TextMeshProUGUI clear, over;
    public bool isActive = false;


    public void StartGame()
    {
        start.gameObject.SetActive(false);
        isActive = true;
    }

    public void RestartGame()
    {
        restart.gameObject.SetActive(false);
        over.gameObject.SetActive(false);
        isActive = true;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GameClear()
    {
        isActive = false;
        clear.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
    }
}
