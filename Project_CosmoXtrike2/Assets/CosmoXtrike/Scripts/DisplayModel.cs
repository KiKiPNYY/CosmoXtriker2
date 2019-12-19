using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayModel : MonoBehaviour
{
    [SerializeField] private float m_modelRotationSpeed = 0;
    [SerializeField] private Material[] m_material = null;

    void Start()
    {
        
    }


    void Update()
    {
        for(int i = 0; i < m_material.Length; i++)
        {
            m_material[i].SetVector("_ThisPosition", this.transform.position);
        }
        this.transform.Rotate(new Vector3(0,90f,0) * Time.deltaTime * m_modelRotationSpeed, Space.Self);
    }
}
