using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockage : MonoBehaviour
{
    [SerializeField] public GameObject m_MedPack_Prefab;
    [SerializeField] public GameObject m_AmmoPack_Prefab;
    [SerializeField] public GameObject m_WoodBoard_Prefab;
    [SerializeField] public GameObject m_Barbele_Prefab;

    private void Awake()
    {
        GameRules.m_refStockage = this;
    }
}
