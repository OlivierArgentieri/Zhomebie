using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NailCarpet : Traps
{
    [SerializeField]
    private int m_damage = 1;

    [SerializeField]
    private float m_attackSpeed = 1;

    [SerializeField]
    private float m_divideSpeedBy;
    
    private float m_timerAttackSpeed;

    Dictionary<Enemy, float> m_enemies;
    private void Awake()
    {
        m_enemies = new Dictionary<Enemy, float>();

        m_timerAttackSpeed = 0;
    }

    private void AttackManager()
    {
        foreach(Enemy e in m_enemies.Keys.ToList<Enemy>())
        {

            if (m_enemies[e] >= m_attackSpeed)
            {
                e.Hit(m_damage);
                m_enemies[e] = 0;
                Debug.Log(m_damage);
            }
            else
                m_enemies[e] += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null && m_divideSpeedBy > 0 && m_enemies.ContainsKey(enemy) == false)
        {
            enemy.Speed /= m_divideSpeedBy;
            m_enemies.Add(enemy, 1);
        }

    }
   

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null && m_enemies.ContainsKey(enemy) && m_activated)
        {
            enemy.Speed *= m_divideSpeedBy;
            m_enemies.Remove(enemy);
        }
    }
    private void Update()
    {
        base.Update();

        if (m_enemies != null && m_enemies.Count>0 && m_activated)
            AttackManager();
    }
}