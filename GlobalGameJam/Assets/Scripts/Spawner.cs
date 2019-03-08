using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [HideInInspector]
    public Pickable m_Pickable = null;

    [SerializeField]
    public float m_TimeToPop = 5f;
    [SerializeField]
    public int probaAmmo;
    [SerializeField]
    public int probaHeal;
    [SerializeField]
    public int probaBarbele;
    [SerializeField]
    public int probaRenfo;


    private void Awake()
    {
        SpawnerManager.CheckForDelete();
        SpawnerManager.RegisterPair(this);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Pickable != null && (m_Pickable.GetComponent<WindowItem>() && m_Pickable.GetComponent<WindowItem>().m_activated))
        {
            m_Pickable = null;
        }
    }



    public bool CanSpawn()
    {
        if (m_Pickable == null)
        {
            return true;
        }

        return false;
    }

    public void Spawn(GameObject p_ToSpawn)
    {
        if (p_ToSpawn != null)
        {
            float offsetY = p_ToSpawn.transform.lossyScale.y;
            Vector3 posToPop = new Vector3(transform.position.x, offsetY / 2, transform.position.z);
            GameObject go = Instantiate(p_ToSpawn, posToPop, transform.rotation);
            m_Pickable = go.GetComponent<Pickable>();
        }
    }

    public void ResetPickable()
    {
        m_Pickable = null;
    }
}
