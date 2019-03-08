using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbedWireWindow : WindowItem
{
    public int m_life = 10;
    public int m_damage = 10;

    private new void Update()
    {
        if (m_activated)
        {
            base.Update();

            if (m_life <= 0)
            {
                m_windowRef.TriRemoveReinforcement(this);
                Destroy(gameObject);
            }
        }
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
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.Hit(m_damage);
                m_life--;
            }
        }
    }
}
