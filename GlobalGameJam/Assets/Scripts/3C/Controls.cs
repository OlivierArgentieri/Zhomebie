using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public delegate void InputFunction();
    public Dictionary<KeyCode, InputFunction> m_DictionnaryInputs;
    private CameraFPS m_Camera;
    private Player m_Player;
    private float m_mouseX;
    private float m_mouseY;

    void Awake()
    {
        m_DictionnaryInputs = new Dictionary<KeyCode, InputFunction>();
        m_Camera = GameObject.FindObjectOfType<CameraFPS>();
        m_Player = GetComponent<Player>();
    }

    void Update()
    {
        if(m_Player.IsWalking)
            m_Player.ResetDirection();
        if (m_Player.lockedControls)
        {
            return;
        }

        foreach (KeyValuePair<KeyCode, InputFunction> entry in m_DictionnaryInputs)
        {
            if (Input.GetKey(entry.Key))
            {
                entry.Value();
            }
        }


        MouseRotation();
        MouseScrollWheel();
        


    }

    public void SuscribeToInputDictionnary(InputFunction p_function, KeyCode p_key)
    {
        m_DictionnaryInputs.Add(p_key, p_function);
    }

    private void MouseRotation()
    {
        m_mouseX += 2 * Input.GetAxis("Mouse X");
        m_mouseY -= 2 * Input.GetAxis("Mouse Y");

        if (!(m_mouseY < 90f && m_mouseY > -90f))
        {
            m_mouseY += 2 * Input.GetAxis("Mouse Y");
        }

        m_Player.Rotate(m_mouseX);
        m_Camera.Rotate(m_mouseX, m_mouseY);
    }

    private void MouseScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            m_Player.ChangeWeapon(1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            m_Player.ChangeWeapon(-1);
        }
    }
}