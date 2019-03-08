using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private NavMeshAgent m_agent;
    private Vector3 m_playerPosition;
    [SerializeField] private int m_damage = 1;
    [SerializeField] private float m_attackSpeed = 1;
    private List<Window> m_list_windows;

    private float m_timerAttackSpeed = 0;
    private bool m_canAttack = false;

    private Vector3 m_target;
    private bool m_canGoToPlayer = false;
    private bool m_dead = false;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.speed = 5;
    }
    public float Speed
    {
        set
        {
            //base.m_Speed = value;
            m_agent.speed = value;
        }
        get { return m_agent.speed; }
    }

    protected override void Start()
    {
        base.Start();

        m_target = GetClosestWindowPosition();
        m_agent.destination = m_target;
    }

    protected override void Update()
    {
        base.Update();
  
       

        if (Arrived())
        {
            GetComponent<Animation>().Stop();
        }
        else
        {
            GetComponent<Animation>().Play();
        }

        if (m_canGoToPlayer == true)
        {
            if (m_dead == false)
                m_agent.destination = GameMediator.GetPlayer().transform.position;
        }

        TimerManager();
    }

    public override void Death()
    {
        base.Death();
        int nb = GameMediator.EnemiesKilled();
        nb++;
        GameMediator.UpdateEnemiesKilled(nb);
        m_dead = true;
        Destroy(gameObject);
    }

    private void TimerManager()
    {
        if (m_timerAttackSpeed >= m_attackSpeed)
        {
            m_canAttack = true;
        }
        else
            m_timerAttackSpeed += Time.deltaTime;
    }

    private bool Arrived()
    {
        Vector3 actorPos = transform.position;
        actorPos.y = 0;
        Vector3 targetPos = m_target;
        targetPos.y = 0;

        float sqrDistanceToTarget = (m_target - transform.position).sqrMagnitude;

        if (sqrDistanceToTarget <= 10)
            return true;
        return false;
    }

    public NavMeshAgent Agent
    {
        get { return m_agent; }
    }

    private Vector3 GetClosestWindowPosition()
    {
        Vector3 closest = Vector3.one;
        float sqrDistance = Mathf.Infinity;

        for (int i = 0; i < GameMediator.GetWindows().Count; i++)
        {
            if (GameMediator.GetWindows()[i] != null)
            {
                float actualSqrDistance = (GameMediator.GetWindows()[i].transform.position - transform.position).sqrMagnitude;

                if (actualSqrDistance < sqrDistance)
                {
                    sqrDistance = actualSqrDistance;
                    closest = GameMediator.GetWindows()[i].transform.position;
                }
            }

        }
        return closest;
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerWindow(other);
        TriggerPlayer(other);
    }

    private void TriggerWindow(Collider other)
    {
        if (m_canAttack)
        {
            Window window = other.GetComponent<Window>();
            if (window != null)
            {
                if (window.TryInteract(this))
                {
                    m_canGoToPlayer = false;
                    m_canAttack = false;
                    m_timerAttackSpeed = 0;
                }
                else
                {
                    m_canGoToPlayer = true;
                }
                return;
            }
        }
    }

    private void TriggerPlayer(Collider other)
    {
        if (m_canAttack)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Hit(m_damage);

                m_canAttack = false;
                m_timerAttackSpeed = 0;
            }
        }
    }
}
