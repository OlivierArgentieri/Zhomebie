using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : BodyToBody
{
    [SerializeField]
    private int m_damage = 1;
    private Bounds m_attackBox;


    private void Awake()
    {
       
    }

    private void Start()
    {
        m_attackBox = new Bounds(Vector3.one, new Vector3(2.5f, 2, 2.5f));
    }

    protected override void Attack()
    {
        m_attackBox.center = GameMediator.GetPlayer().transform.position;
        m_attackBox.center += GameMediator.GetPlayer().transform.forward * 1.125f;
        Collider[] hitColliders = Physics.OverlapBox(m_attackBox.center, m_attackBox.extents, Quaternion.identity);

        int i = 0;

        while (i < hitColliders.Length)
        {
            Enemy enemy = hitColliders[i].GetComponent<Enemy>();
            if (enemy != null)
                enemy.Hit(m_damage);
            i++;
        }

        GetComponent<Animation>().Play();
           
    }

    private void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    private void OnDrawGizmos()
    {
        //        Gizmos.color = Color.red;
        //        Gizmos.DrawWireCube(m_attackBox.center, m_attackBox.size);
    }
}

