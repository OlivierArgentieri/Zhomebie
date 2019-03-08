using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAtTime : MonoBehaviour {

    [SerializeField]
    private float m_time = 10.0f;

	void Update () {
        m_time -= Time.deltaTime;
        if (m_time <= 0.0f)
            Destroy(this.gameObject);
    }
}
