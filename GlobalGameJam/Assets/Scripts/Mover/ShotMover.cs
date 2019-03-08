using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMover : MonoBehaviour
{ 
    [SerializeField]
    private float m_speed = 60.0f;


    [SerializeField]
    private int m_damage = 1;

    private void Update()
    {
        transform.position += transform.forward * m_speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
            enemy.Hit(m_damage);
        if(other.tag.ToLower() != "player")
            Destroy(this.gameObject);
    }
}