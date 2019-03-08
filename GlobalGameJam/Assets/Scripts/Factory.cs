using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    public enum PickableType
    {
        ammoPack,
        healPack,
        reinforcement,
        barbele
    }

    public static T RandomEnumValue<T>()
    {
        var values = System.Enum.GetValues(typeof(T));
        int rdm = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(rdm);
    }


    public static GameObject Create(PickableType _Type)
    {
        switch (_Type)
        {
            case PickableType.ammoPack:
                return GameRules.m_refStockage.m_AmmoPack_Prefab;
            case PickableType.healPack:
                return GameRules.m_refStockage.m_MedPack_Prefab;
            case PickableType.reinforcement:
                return GameRules.m_refStockage.m_WoodBoard_Prefab;
                case PickableType.barbele:
                    return GameRules.m_refStockage.m_Barbele_Prefab;
            default:
                break;
        }

        return null;
    }

}
