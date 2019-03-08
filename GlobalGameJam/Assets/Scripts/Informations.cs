using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Informations : MonoBehaviour
{

    [SerializeField]
    private Transform m_CanvasInformations;

    private bool m_FirstTime = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_FirstTime)
        {
            if (m_CanvasInformations.gameObject.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    m_FirstTime = true;
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
        

    }
}
