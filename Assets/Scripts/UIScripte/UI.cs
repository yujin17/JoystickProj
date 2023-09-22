using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    //public GameObject pauseButton;
    public GameObject playerGame;
    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPause()
    {
        pauseButton.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameObject.Find("Player").GetComponent<PlayerMove>().JumpCnt = 1;
        GameObject.Find("Player").GetComponent<PlayerMove>().move = false;
    }

    public void OnClickClose()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<PlayerMove>().JumpCnt = 0;
        GameObject.Find("Player").GetComponent<PlayerMove>().move = true;

    }

    public void OnClickResume()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<PlayerMove>().JumpCnt = 0;
        GameObject.Find("Player").GetComponent<PlayerMove>().move = true;

    }

    public void OnClickMenu()
    {
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<PlayerMove>().JumpCnt = 0;
        GameObject.Find("Player").GetComponent<PlayerMove>().move = true;
        SceneManager.LoadScene("UIScene");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
