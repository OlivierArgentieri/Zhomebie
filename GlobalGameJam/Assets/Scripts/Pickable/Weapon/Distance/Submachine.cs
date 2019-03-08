using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Submachine : Distance
{
    [SerializeField]
    protected GameObject m_fire_prefab;

    protected override void Attack()
    {
    	GameObject temp = Instantiate(m_fire_prefab, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(transform.forward * 400);
        GetComponent<Animation>().Play();
    }

    private void Update()
    {
        base.Update();

    }
}
