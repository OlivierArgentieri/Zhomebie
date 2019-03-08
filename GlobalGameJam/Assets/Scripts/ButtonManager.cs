using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    bool m_IsPaused = false;
    [SerializeField]
    Transform m_MenuCanvas;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        CheckIfPause();
    }

    private void CheckIfPause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!m_MenuCanvas.gameObject.activeSelf)
            {
                Pause();
                m_MenuCanvas.gameObject.SetActive(true);
            }
            else
            {
                m_MenuCanvas.gameObject.SetActive(false);
                Resume();
            }

        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameMediator.GetPlayer().LockControls(true);
    }

    public void LaunchGame()
    {

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        if (GameMediator.GetPlayer() != null)
        {
            GameMediator.GetPlayer().LockControls(false);
        }

        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GameMediator.GetPlayer().LockControls(false);
    }

    public void GoBackMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Test()
    {
        SceneManager.LoadScene("SampleScene");
        Resume();
        GameMediator.GetWaveManager().ResetEnemies();
        GameMediator.UpdateEnemiesKilled(0);
    }
}
