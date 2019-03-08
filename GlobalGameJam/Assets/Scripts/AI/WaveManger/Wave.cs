using UnityEngine;
using System;

[Serializable]
public struct Wave
{
    public int m_numberToSpawn;
    public float m_spawnRate;

    public Wave(int p_numberToSpawn, float p_spawnRate)
    {
        m_numberToSpawn = p_numberToSpawn;
        m_spawnRate = p_spawnRate;
    }
}
