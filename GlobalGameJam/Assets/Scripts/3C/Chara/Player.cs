using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float m_Impulse = 10;
    public int m_MaxAmmoCount = 100;
    public int m_AmmoCount;

    Controls m_InputController;
    Vector3 m_Direction;
    Rigidbody m_Rb;
    Carryable m_ActualCarryable;
    List<Weapon> m_Weapons;
    int m_ActualWeapon = 0;
    bool m_IsJumping;
    public bool IsWalking { get; private set; }
    float m_CoolDown = 1;
    float timer = 0;

    public bool lockedControls;

    [System.NonSerialized]
    public int m_nbKeys;

    public float Speed
    {
        set
        {
            base.m_Speed = value;
        }

        get { return base.m_Speed; }
    }

    public bool Heal(int _HealValue)
    {

        if (m_CurrentLife < m_MaxLife)
        {
            m_CurrentLife += _HealValue;

            Debug.Log(m_CurrentLife);

            if (m_CurrentLife > m_MaxLife)
            {
                m_CurrentLife = m_MaxLife;
            }

            return true;
        }

        return false;
    }

    public bool TakeAmmos(int _NumberAmmos)
    {
        if (m_AmmoCount < m_MaxAmmoCount)
        {
            m_AmmoCount += _NumberAmmos;
            if (m_AmmoCount >= m_MaxAmmoCount)
            {
                m_AmmoCount = m_MaxAmmoCount;
            }

            Debug.Log(m_AmmoCount);

            return true;
        }

        return false;
    }

    public void ChangeWeapon(int p_sens)
    {
        m_Weapons[m_ActualWeapon].gameObject.SetActive(false);
        m_ActualWeapon += p_sens;

        if (m_ActualWeapon >= m_Weapons.Count)
        {
            m_ActualWeapon = m_Weapons.Count - 1;
        }

        if (m_ActualWeapon < 0)
        {
            m_ActualWeapon = 0;
        }

        DisplayManager.Instance.ChangeWeaponFeedBack(m_ActualWeapon);

        m_Weapons[m_ActualWeapon].gameObject.SetActive(true);
    }

    private void Awake()
    {
        m_nbKeys = 0;
        m_MaxLife = 100f;
        lockedControls = false;
        IsWalking = false;
    }


    protected override void Start()
    {
        Debug.LogWarning("For Jump : Put tag 'Ground' on the ground");

        base.Start();

        m_AmmoCount = m_MaxAmmoCount;
        m_IsJumping = false;
        m_Direction = Vector3.zero;
        m_Rb = GetComponent<Rigidbody>();

        InitInputs();

        m_Weapons = new List<Weapon>();
        m_Weapons.Add(GameObject.FindObjectOfType<Punch>());
        m_Weapons[0].gameObject.SetActive(true);
        m_Weapons.Add(GameObject.FindObjectOfType<Submachine>());
        m_Weapons[m_Weapons.Count - 1].gameObject.SetActive(false);

        GameMediator.RegisterPlayer(this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;

        ResetDirection();

        if (m_ActualCarryable != null)
        {
            m_ActualCarryable.ManageFeedback();
        }
    }


    void InitInputs()
    {
        m_InputController = GetComponent<Controls>();
        m_InputController.SuscribeToInputDictionnary(MoveRight, KeyCode.D);
        m_InputController.SuscribeToInputDictionnary(MoveLeft, KeyCode.Q);
        m_InputController.SuscribeToInputDictionnary(MoveForward, KeyCode.Z);
        m_InputController.SuscribeToInputDictionnary(MoveBackward, KeyCode.S);
        m_InputController.SuscribeToInputDictionnary(Jump, KeyCode.Space);
        m_InputController.SuscribeToInputDictionnary(InteractCarryable, KeyCode.E);
        m_InputController.SuscribeToInputDictionnary(ReArming, KeyCode.A);

        m_InputController.SuscribeToInputDictionnary(Attack, KeyCode.Mouse0);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag.ToLower() == "ground")
        {
            m_IsJumping = false;
        }
    }


    void Jump()
    {
        if (m_IsJumping == false)
        {
            m_Rb.AddForce(new Vector3(0, m_Impulse, 0), ForceMode.Impulse);
            m_IsJumping = true;
        }
    }

    void MoveRight()
    {
        transform.position = transform.position + transform.right * m_Speed * Time.deltaTime;
        IsWalking = true;
    }

    void MoveLeft()
    {
        transform.position = transform.position - transform.right * m_Speed * Time.deltaTime;
        IsWalking = true;

    }

    void MoveForward()
    {
        transform.position = transform.position + transform.forward * m_Speed * Time.deltaTime;
        IsWalking = true;

    }

    void MoveBackward()
    {
        transform.position = transform.position - transform.forward * m_Speed * Time.deltaTime;
        IsWalking = true;

    }

    public void ResetDirection()
    {
        m_Rb.velocity = new Vector3(0, m_Rb.velocity.y, 0);
    }


    void InteractCarryable()
    {
        if (timer >= m_CoolDown)
        {
            timer = 0;

            if (m_ActualCarryable != null)
            {
                if (m_ActualCarryable.TryPlace())
                {
                    m_ActualCarryable = null;
                    return;
                }
            }

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit[] hits = Physics.RaycastAll(ray);

            bool groundHitOnly = false;
            RaycastHit groundHit = new RaycastHit();

            for (int i = 0; i < hits.Length; i++)
            {
                WindowItem windowItem = hits[i].collider.GetComponent<WindowItem>();

                if (windowItem != null)
                {
                    if (windowItem != m_ActualCarryable && windowItem.m_activated == false)
                    {
                        Drag(hits[i]);
                        groundHitOnly = false;
                        break;
                    }
                }

                Traps trap = hits[i].collider.GetComponent<Traps>();

                if (trap != null)
                {
                    if (trap != m_ActualCarryable)
                    {
                        Drag(hits[i]);
                        groundHitOnly = false;
                        break;
                    }
                }

                Usuable usable = hits[i].collider.GetComponent<Usuable>();
                if (usable != null)
                {
                    Drag(hits[i]);
                    groundHitOnly = false;
                    break;
                }

                if (hits[i].collider.CompareTag("Ground"))
                {
                    groundHitOnly = true;
                    groundHit = hits[i];

                }
            }

            if (groundHitOnly)
            {
                Drop(groundHit);
            }
        }

    }

    void Drag(RaycastHit p_hit)
    {
        Transform objHit = p_hit.transform;

        if (objHit.GetComponent<Pickable>() != null)
        {
            if (objHit.GetComponent<Usuable>())
            {
                if (objHit.GetComponent<Usuable>().Effect())
                    Destroy(objHit.gameObject);
            }

            else if (objHit.GetComponent<Carryable>())
            {

                if (m_ActualCarryable != null)
                {
                    Drop(p_hit);
                }
                m_ActualCarryable = objHit.GetComponent<Carryable>();
                m_ActualCarryable.EnableFeedback();
                m_ActualCarryable.m_activated = false;
            }
        }
    }

    void Drop(RaycastHit p_hit)
    {
        if (m_ActualCarryable != null)
        {
            float offsetY = m_ActualCarryable.transform.lossyScale.y / 2;
            Vector3 posToPop = new Vector3(p_hit.point.x, 0, p_hit.point.z);

            m_ActualCarryable.transform.position = posToPop;
            m_ActualCarryable.RemoveFeedback();
            m_ActualCarryable = null;

            m_ActualCarryable.m_activated = true;
        }
    }



    void ReArming()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        if (hit.collider.GetComponent<Traps>())
        {
            hit.collider.GetComponent<Traps>().ReloadTrap();
        }
    }

    void Attack()
    {
        if (m_AmmoCount > 0 && m_Weapons[m_ActualWeapon].TryAttack())
        {
            if (m_Weapons[m_ActualWeapon].GetComponent<Submachine>())
            {
                m_AmmoCount--;
                Debug.Log(m_AmmoCount);
            }
        }

    }

    public void LockControls(bool _Value)
    {
        lockedControls = _Value;
    }


}