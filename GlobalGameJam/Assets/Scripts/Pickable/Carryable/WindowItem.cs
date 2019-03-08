using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WindowItem : Carryable
{
 
    protected Window m_windowRef;

    protected override bool CanPlace()
    {
        Vector3 viewOrigin = Camera.main.transform.position;
        Vector3 viewDirection = Camera.main.transform.forward;

        RaycastHit[] hits = Physics.RaycastAll(viewOrigin, viewDirection, m_poseLimit);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Window"))
                {
                    m_windowRef = hits[i].collider.GetComponent<Window>();

                    return m_windowRef.CanReinforce();
                }
            }
        }
        return false;
    }

    protected override void Place()
    {
        m_windowRef.TryReinforce(this);
        RemoveFeedback();
    }

    public override string GetTagToPlace()
    {
        return "Window";
    }

    public bool TryInteract(Character p_actor)
    {
        if(CanInteract(p_actor))
        {
            Interact(p_actor);
            return true;
        }
        return false;
    }

    public abstract bool CanPasse();

    public abstract bool CanInteract(Character p_actor);
    public abstract void Interact(Character p_actor);
}
