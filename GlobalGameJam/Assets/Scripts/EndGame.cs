using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    [SerializeField]
    private Transform m_CanvasEnd;
    [SerializeField]
    private Text m_Result;

    bool test = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!test)
        {
            CheckIfEnd();
        }

    }

    void CheckIfEnd()
    {
        if (GameMediator.GetPlayer().m_CurrentLife <= 0)
        {
            test = true;
            m_Result.text = "LOOSE";
        }
        else if (GameMediator.GetWaveManager().m_end && GameMediator.IsAllEnemiesKilled())
        {
            test = true;
            m_Result.text = "WIN";
        }
        else
        {
            return;
        }

        m_CanvasEnd.gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameMediator.GetPlayer().LockControls(true);
    }
}
