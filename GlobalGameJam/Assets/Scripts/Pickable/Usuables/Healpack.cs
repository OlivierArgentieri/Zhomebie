using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Healpack : Usuable
{
    private int m_HealValue = 15;

    protected override bool CanUse()
    {
        return false;
    }

    public override bool Effect()
    {
        return GameMediator.GetPlayer().Heal(m_HealValue);
    }

    protected override void Use()
    {
        // todo
    }
}
