using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usuable : Pickable
{
    public bool m_Used;

    public bool TryUse()
    {
        if (CanUse())
        {
            Use();
            return true;
        }
        return false;
    }

    protected abstract bool CanUse();
    protected abstract void Use();

    public abstract bool Effect();
 
}

