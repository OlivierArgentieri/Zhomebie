using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    static Dictionary<Spawner, float> m_Spawners = new Dictionary<Spawner, float>();
    Factory.PickableType m_TypeToPop;

    //to modify
    static int m_NumberSpawners = 4;

    private void Awake()
    {



    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Browse();
    }

    public static void CheckForDelete()
    {
        if (m_Spawners.Count >= m_NumberSpawners)
        {
            m_Spawners.Clear();
        }
    }

    public static void RegisterPair(Spawner p_Spawner)
    {
        m_Spawners.Add(p_Spawner, 0);
    }

    void Browse()
    {
        List<Spawner> keyToChange = new List<Spawner>();

        foreach (var spawner in m_Spawners)
        {
            keyToChange.Add(spawner.Key);
        }

        for (int i = 0; i < keyToChange.Count; i++)
        {
            if (keyToChange[i].m_Pickable == null)
            {
                m_Spawners[keyToChange[i]] -= Time.deltaTime;
            }

            if (m_Spawners[keyToChange[i]] <= 0)
            {
                //m_TypeToPop = Factory.RandomEnumValue<Factory.PickableType>();
                SetPickableToPop(keyToChange[i]);
                PopItem(keyToChange[i]);
                m_Spawners[keyToChange[i]] = keyToChange[i].m_TimeToPop;
            }
        }
    }


    bool PopItem(Spawner p_Spawner)
    {
        if (p_Spawner.CanSpawn())
        {
            p_Spawner.Spawn(Factory.Create(m_TypeToPop));
            return true;
        }

        return false;
    }

    private void SetPickableToPop(Spawner _Test)
    {
        int nbRandom = Random.Range(0, 99);

        if (nbRandom >= 0 && nbRandom < _Test.probaAmmo)
        {
            m_TypeToPop = Factory.PickableType.ammoPack;
        }

        else if (nbRandom >= _Test.probaAmmo && nbRandom < _Test.probaAmmo + _Test.probaHeal)
        {
            m_TypeToPop = Factory.PickableType.healPack;
        }

        else if (nbRandom >= _Test.probaAmmo + _Test.probaHeal && nbRandom < _Test.probaAmmo + _Test.probaHeal + _Test.probaRenfo)
        {
            m_TypeToPop = Factory.PickableType.reinforcement;
        }

        else if (nbRandom >= _Test.probaAmmo + _Test.probaHeal + _Test.probaRenfo && nbRandom < _Test.probaAmmo + _Test.probaHeal + _Test.probaRenfo + _Test.probaBarbele)
        {
            m_TypeToPop = Factory.PickableType.barbele;
        }
    }
}
