using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMediator
{

    static Player m_Player = null;
    static GameRules m_Rules = null;

    static HashSet<Window> m_Windows = new HashSet<Window>();

    static WaveManager m_WaveManager = null;
    static int m_EnemiesKilled = 0;

    public static int EnemiesKilled()
    {
        return m_EnemiesKilled;
    }

    public static void UpdateEnemiesKilled(int _Value)
    {
        m_EnemiesKilled = _Value;
    }

    public static bool IsAllEnemiesKilled()
    {
        return m_EnemiesKilled >= m_WaveManager.GetAllEnemiesToPop();
    }

    public static void RegisterWaveManager(WaveManager p_WaveManager)
    {
        m_WaveManager = p_WaveManager;
    }

    public static void RegisterWindow(Window p_Window)
    {
        m_Windows.Add(p_Window);
    }

    public static void RegisterPlayer(Player p_Player)
    {
        m_Player = p_Player;
    }

    public static Player GetPlayer()
    {
        return m_Player;
    }

    public static GameRules GetGameRules()
    {
        if (m_Rules == null)
        {
            m_Rules = GameObject.FindObjectOfType<GameRules>();
        }

        return m_Rules;
    }

    public static List<Window> GetWindows()
    {
        return new List<Window>(m_Windows);
    }

    public static WaveManager GetWaveManager()
    {
        return m_WaveManager;
    }
}
