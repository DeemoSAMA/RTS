using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    public Button start;
    public Button quitgame;

    // Use this for initialization
    void Start()
    {
        start.onClick.AddListener(OnClickstart);
        quitgame.onClick.AddListener(OnClickQuitGame);
    }

    private void OnClickstart()
    {
        SceneManager.LoadScene(1);
    }
    private void OnClickQuitGame()
    {
        Application.Quit();
    }

}
