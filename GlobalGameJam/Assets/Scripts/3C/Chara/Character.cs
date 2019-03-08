using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float m_Speed = 350;
    public float m_MaxLife = 10;
    public float m_CurrentLife;
    protected bool m_HasMove;


    protected virtual void Start()
    {
        m_CurrentLife = m_MaxLife;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        m_HasMove = false;

        if (m_CurrentLife <= 0)
            Death();
    }

    public virtual void Hit(int p_damage)
    {
        m_CurrentLife -= p_damage;
    }

    public virtual void Death()
    {
        Debug.Log(name + " is dead");
    }

    protected void TryMove(Vector3 p_Direction)
    {
        p_Direction.y = 0;
        p_Direction.Normalize();

        if (CanMove(p_Direction))
        {
            Move(p_Direction);
        }
    }

    bool CanMove(Vector3 p_Direction)
    {
        return !m_HasMove;  // && Si le movement reste dans la limite
    }

    void Move(Vector3 p_Direction)
    {

        transform.position += p_Direction * m_Speed * Time.deltaTime;
        m_HasMove = true;
    }

    public void Rotate(float p_MouseX)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, p_MouseX, 0));
    }
}