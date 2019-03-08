using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] public GameObject m_enemyPrefabs;
    [SerializeField] public float m_WaitWaveTime = 5;
    [SerializeField] public List<Vector3> m_corners = new List<Vector3>();
    [SerializeField] public List<Wave> m_waves = new List<Wave>();

    public bool m_end = false;

    private int m_indexWave = 0;
    private float m_timerSpawn = 0;

    private bool m_Wait = true;
    private float m_timerWait = 0;

    private List<Vector3> m_debugPoints = new List<Vector3>();

    private int m_AllEnemies = 0;

    public int IndexWave
    {
        get
        {
            return m_indexWave;
        }
    }

    public void ResetEnemies()
    {
        SetNumberEnemiesToPop();
    }

    private void Awake()
    {
        SetNumberEnemiesToPop();
    }

    private void Start()
    {
        GameMediator.RegisterWaveManager(this);
        
    }

    private void Update()
    {
        ManageWave();
    }

    public void SetNumberEnemiesToPop()
    {
        int enemies = 0;
        for (int i = 0; i < m_waves.Count; i++)
        {
            enemies += m_waves[i].m_numberToSpawn;
        }

        m_AllEnemies = enemies;
    }

    public int GetAllEnemiesToPop()
    {
        return m_AllEnemies;
    }

    private void ManageWave()
    {
        if (IndexWave < m_waves.Count)
        {
            if (m_Wait == false)
            {
                if (m_waves[m_indexWave].m_numberToSpawn > 0)
                {
                    if (m_timerSpawn >= m_waves[m_indexWave].m_spawnRate)
                    {
                        SpawnEnemy();
                        m_waves[m_indexWave] = new Wave(m_waves[m_indexWave].m_numberToSpawn - 1, m_waves[m_indexWave].m_spawnRate);
                        m_timerSpawn = 0;
                    }
                    else
                        m_timerSpawn += Time.deltaTime;
                }
                else
                {
                    m_indexWave++;
                    m_Wait = true;
                }
            }
            else
            {
                if (m_timerWait >= m_WaitWaveTime)
                {
                    m_Wait = false;
                    m_timerWait = 0;
                }
                else
                    m_timerWait += Time.deltaTime;
            }
        }
        else
            m_end = true;
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate<GameObject>(m_enemyPrefabs);
        enemy.GetComponent<Enemy>().Agent.Warp(GetPointToSpawn());
    }

    private Vector3 GetPointToSpawn()
    {
        m_debugPoints = GetAllPoint(100);
        return m_debugPoints[Random.Range(0, 100 - 1)];
    }

    private List<Vector3> GetAllPoint(int p_nbPoint)
    {
        List<Vector3> points = new List<Vector3>();

        Vector3 start = m_corners[0];
        Vector3 end = Vector3.zero;
        int nbPointsPerSection = p_nbPoint / m_corners.Count;
        for (int i = 1; i < m_corners.Count; i++)
        {
            end = m_corners[i];
            points.AddRange(GetPointBetweenTwo(start, end, nbPointsPerSection));
            start = end;
        }
        points.AddRange(GetPointBetweenTwo(end, m_corners[0], nbPointsPerSection));

        return points;
    }

    private List<Vector3> GetPointBetweenTwo(Vector3 p_start, Vector3 p_end, int p_nbPoints)
    {
        List<Vector3> points = new List<Vector3>();

        Vector3 startToEnd = p_end - p_start;

        Vector3 step = startToEnd / p_nbPoints;

        for (int i = 0; i < p_nbPoints; i++)
        {
            points.Add(p_start + step * i);
        }

        return points;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < m_debugPoints.Count; i++)
        {
            Gizmos.DrawWireSphere(m_debugPoints[i], 1);
        }
    }
}
