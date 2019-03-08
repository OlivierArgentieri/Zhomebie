using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carryable : Pickable
{
    private MeshRenderer m_renderer;
    private MeshFilter m_filter;

    public Material m_originMaterial;

    public Mesh m_feedbackMesh;
    public Material m_feebackMaterialOn;
    public Material m_feebackMaterialOff;

    public bool On = false;
    protected float m_poseLimit = 5;
    public bool feedBack = true;

    public bool m_activated = false;


    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
    }

    protected void Update()
    {
        /*ManageFeedback();

        if (Input.GetMouseButtonDown(0))
            TryPlace();*/
    }

    public void DisplayFeedback()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_filter = GetComponent<MeshFilter>();

        feedBack = true;

        m_renderer.sharedMaterial = m_feebackMaterialOn;
        m_filter.sharedMesh = m_feedbackMesh;
    }

    public void RemoveFeedback()
    {
        m_renderer = GetComponent<MeshRenderer>();
        feedBack = false;
        m_renderer.material = m_originMaterial;
    }

    public void SetFeedbackOn()
    {
        m_renderer.sharedMaterial = m_feebackMaterialOn;
        On = true;
    }

    public void SetFeedbackOff()
    {
        m_renderer.sharedMaterial = m_feebackMaterialOff;
        On = false;
    }

    public void DiseableFeeback()
    {
        m_renderer.sharedMaterial = m_originMaterial;
        feedBack = false;
    }

    public void EnableFeedback()
    {
        On = true;
        SetFeedbackOn();
        feedBack = true;
    }

    public void ManageFeedback()
    {
        if (feedBack)
        {
            Vector3 viewOrigin = Camera.main.transform.position;
            Vector3 viewDirection = Camera.main.transform.forward;

            RaycastHit[] hits = Physics.RaycastAll(viewOrigin, viewDirection, m_poseLimit);

            if (hits.Length > 0)
            {
                Vector3 hitPoint = viewOrigin + viewDirection * m_poseLimit;
                bool targetHit = false;

                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.CompareTag(GetTagToPlace()))
                    {
                        hitPoint = hits[i].point;
                        targetHit = true;
                        break;
                    }
                    else if (hits[i].collider.CompareTag("Enemy") == false && hits[i].collider.CompareTag("Player") == false && hits[i].collider.gameObject != gameObject)
                    {
                        hitPoint = hits[i].point;
                    }
                }

                transform.position = hitPoint;

                if (targetHit)
                {
                    SetFeedbackOn();
                }
                else
                {
                    SetFeedbackOff();
                }
            }
            else
            {
                transform.position = viewOrigin + viewDirection * m_poseLimit;
                SetFeedbackOff();
            }
        }
    }

    public abstract string GetTagToPlace();

    public bool TryPlace()
    {
        if (CanPlace())
        {
            m_activated = true;
            Place();
            return true;
        }
        return false;
    }

    protected abstract bool CanPlace();

    protected abstract void Place();

}
