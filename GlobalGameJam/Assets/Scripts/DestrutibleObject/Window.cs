using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Window : MonoBehaviour
{
    private OffMeshLink m_navLink;

    private WindowItem m_item;

    public bool Reinforced
    {
        get
        {
            return m_item != null;
        }
    }

    private void Awake()
    {
        GameMediator.RegisterWindow(this);
        m_navLink = GetComponent<OffMeshLink>();
        m_navLink.enabled = true;
    }

    public bool TryInteract(Character p_actor)
    {
        if (m_item == null)
            return false;

        return m_item.TryInteract(p_actor);
    }


    public bool TryReinforce(WindowItem p_item)
    {
        if (CanReinforce())
        {
            Reinforce(p_item);
            return true;
        }
        return false;
    }

    public bool CanReinforce()
    {
        return m_item == null;
    }

    private void Reinforce(WindowItem p_item)
    {
        m_item = p_item;
        m_item.transform.position = transform.position;
        m_item.m_activated = true;

        if (m_item.CanPasse() == false)
            m_navLink.enabled = false;
        else
            m_navLink.enabled = true;
    }


    public bool TriRemoveReinforcement(WindowItem p_reinforcement)
    {
        if(CanRemoveReinforcement(p_reinforcement))
        {
            RemoveReinforcement();
            return true;
        }
        return false;
    }

    public bool CanRemoveReinforcement(WindowItem p_reinforcement)
    {
        if (m_item == p_reinforcement)
            return true;
        return false;
    }

    private void RemoveReinforcement()
    {
        m_navLink.enabled = true;
        m_item.m_activated = false;
        m_item = null;
    }
}
