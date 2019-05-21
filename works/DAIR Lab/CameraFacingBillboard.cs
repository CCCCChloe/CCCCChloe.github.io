using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
    }
    
    void LateUpdate()
    {
        Vector3 v = m_Camera.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(m_Camera.transform.position - v);
        // transform.LookAt(transform.position - m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);

    }
}
