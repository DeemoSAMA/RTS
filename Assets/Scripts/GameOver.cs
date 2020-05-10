using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public Button restart;
    public Button quitgame;
    // Use this for initialization
    void Start () {
        restart.onClick.AddListener(OnClickRestart);
        quitgame.onClick.AddListener(OnClickQuitGame);
	}
	
	private void OnClickRestart()
    {
        SceneManager.LoadScene(1);
    }
    private void OnClickQuitGame()
    {
        Application.Quit();
    }

}
