using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Traps : Carryable
{
    public override string GetTagToPlace()
    {
        return "Ground";
    }

    protected override bool CanPlace()
    {
        Vector3 viewOrigin = Camera.main.transform.position;
        Vector3 viewDirection = Camera.main.transform.forward;

        RaycastHit[] hits = Physics.RaycastAll(viewOrigin, viewDirection, m_poseLimit);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Ground"))
                {
                    return true;
                }
                
            }
        }
        return false;
    }

    protected override void Place()
    {

        RemoveFeedback();
    }

    public virtual void ReloadTrap()
    { }


}