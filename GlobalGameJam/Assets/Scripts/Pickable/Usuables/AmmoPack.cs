using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : Usuable
{
    int m_AmmoCount = 10;

    protected override bool CanUse()
    {
        return false;
    }

    public override bool Effect()
    {
        return GameMediator.GetPlayer().TakeAmmos(m_AmmoCount);
    }

    protected override void Use()
    {
        // todo
    }
}
