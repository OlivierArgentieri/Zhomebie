using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBoard : WindowItem
{
    public int m_life = 10;

    public override bool CanInteract(Character p_actor)
    {
        return true;
    }

    public override bool CanPasse()
    {
        return false;
    }

    public override void Interact(Character p_actor)
    {
        m_life--;
        if (m_life <= 0)
        {
            m_windowRef.TriRemoveReinforcement(this);
            Destroy(gameObject);
        }
    }
}
