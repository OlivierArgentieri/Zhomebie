using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveWindow : WindowItem
{
    public int m_damage = 10;
    public float m_range = 5;

    private new void Update()
    {
        if (m_activated)
            base.Update();
    }

    public override bool CanInteract(Character p_actor)
    {
        return false;
    }

    public override bool CanPasse()
    {
        return true;
    }

    public override void Interact(Character p_actor)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_activated)
        {
            if (other.CompareTag("Enemy"))
            {
                Collider[] colliders = Physics.OverlapSphere(other.transform.position, m_range);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].CompareTag("Enemy"))
                    {
                        Enemy enemy = colliders[i].GetComponent<Enemy>();
                        enemy.Hit(m_damage);
                    }
                }

                m_windowRef.TriRemoveReinforcement(this);
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_activated)
            Gizmos.DrawWireSphere(transform.position, m_range);
    }
}
