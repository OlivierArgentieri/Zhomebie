using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTrap : Traps
{
    [SerializeField]
    private int m_damage = 1;

    [SerializeField]
    private float m_attackSpeed = 1;

    [SerializeField]
    private float m_stunSpeed = 5;


    [SerializeField]
    private Mesh m_close_trap;

    [SerializeField]
    private Mesh m_open_trap;


    //private float m_timerAttackSpeed; 
    private float m_timerStunSpeed;
    private bool m_canAttack;
    private bool m_isStun;

    private float m_save_speed_enemy;
    private void Awake()
    {
        m_canAttack = true;
        m_isStun = false;
        m_timerStunSpeed = 0;
        m_save_speed_enemy = 0;
        GetComponent<MeshFilter>().mesh = m_open_trap;
    }

    private void TimerManager()
    {
        if (m_isStun && m_timerStunSpeed >= m_stunSpeed)
        {
            m_isStun = false;
        }
        else
            m_timerStunSpeed += Time.deltaTime;
    }

    private void Update()
    {
        TimerManager();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if ( m_canAttack && enemy != null && m_activated)
        {
            enemy.Hit(m_damage);
            m_canAttack = false;
            m_timerStunSpeed = 0;
            m_isStun = true;


            m_save_speed_enemy = enemy.Speed;
            enemy.Speed = 0;
            GetComponent<MeshFilter>().mesh = m_close_trap;
        }



    }

    private void OnTriggerStay(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null && m_isStun == false && m_activated)
        {
            enemy.Speed = m_save_speed_enemy;
        }
    }

    public override void ReloadTrap()
    {
        base.ReloadTrap();
        m_canAttack = true;
        GetComponent<MeshFilter>().mesh = m_open_trap;

    }
}