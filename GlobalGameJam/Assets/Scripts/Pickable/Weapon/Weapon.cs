using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : Pickable
{
    protected float m_attackSpeed = 0.75f;
    private float m_timerAttackSpeed = 0;
    private bool m_canAttack;

    private void Awake()
    {
        m_canAttack = false;
    }

    private void TimerManager()
    {
        if (m_timerAttackSpeed >= m_attackSpeed)
        {
            m_canAttack = true;
        }
        else
            m_timerAttackSpeed += Time.deltaTime;
    }

    public bool CanAttack()
    {
        if (m_canAttack)
        {
            m_timerAttackSpeed = 0;
            m_canAttack = false;

            return true;
        }
        return false;
    }

    protected abstract void Attack();

    public void Update()
    {
        TimerManager();
    }

    public bool TryAttack()
    {
        if(CanAttack())
        {
            Attack();
            return true;
        }
        return false;
    }

}