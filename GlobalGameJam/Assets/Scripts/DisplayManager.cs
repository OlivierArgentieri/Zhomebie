using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    static DisplayManager m_Instance = null;



    public static DisplayManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType<DisplayManager>();

                if (m_Instance == null)
                {
                    GameObject container = new GameObject("DisplayManager");
                    m_Instance = container.AddComponent<DisplayManager>();
                }
            }
            return m_Instance;
        }
    }

    public DisplayManager()
    {

    }

    [SerializeField]
    private Image m_HealthBar;
    [SerializeField]
    private Text m_IndexWave;
    [SerializeField]
    private Text m_NumberAmmo;
    [SerializeField]
    private List<Image> m_WeaponsIcons = new List<Image>();

    int m_IndexSelected;

    // Use this for initialization
    void Start()
    {

        m_IndexSelected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_IndexWave.text = (GameMediator.GetWaveManager().GetAllEnemiesToPop() - GameMediator.EnemiesKilled()).ToString();
        m_HealthBar.fillAmount = GameMediator.GetPlayer().m_CurrentLife / GameMediator.GetPlayer().m_MaxLife;
        m_NumberAmmo.text = GameMediator.GetPlayer().m_AmmoCount.ToString();
    }

    public void ChangeWeaponFeedBack(int _IndexToGo)
    {
        var tempo = m_WeaponsIcons[m_IndexSelected].color;
        tempo.a = 0;
        m_WeaponsIcons[m_IndexSelected].color = tempo;

        m_IndexSelected = _IndexToGo;

        var tempo2 = m_WeaponsIcons[m_IndexSelected].color;
        tempo2.a = 1;
        m_WeaponsIcons[m_IndexSelected].color = tempo2;
    }
}
