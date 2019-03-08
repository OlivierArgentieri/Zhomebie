using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosives : Traps
{
    [SerializeField]
    private int m_damage = 1;

    [SerializeField]
    private float m_range = 5;


    private void OnTriggerEnter(Collider other)
    {
      
        Enemy enemy = other.GetComponent<Enemy>();

        if(enemy != null && base.m_activated)
        {

            Collider[] colliders = Physics.OverlapSphere(other.transform.position, m_range);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Enemy"))
                {
                    enemy = colliders[i].GetComponent<Enemy>();
                    enemy.Hit(m_damage);
                }
            }
            Destroy(this.gameObject);
        }

    }
}
