using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private EffectData m_effectData = null;

    private ParticleSystem m_particleSystem = null;

    private bool m_thisActive = false;

    private float m_timer = 0;

    public bool ThisActive { get => m_thisActive; }

    public GameObject ThisGameObject { get => this.transform.gameObject; }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="_offsetTransform"></param>
    public virtual void Init(Transform _offsetTransform)
    {

        if (m_particleSystem == null)
        {
            
        }
        m_timer = 0;

        this.transform.position = _offsetTransform.position + m_effectData.ActiveOffset;
        
        if(m_effectData.RotationStandardWorld)
        {
            this.transform.rotation = Quaternion.Euler(m_effectData.ActiveRotation);
        }
        else
        {
            Quaternion quaternion;
            quaternion = Quaternion.LookRotation(_offsetTransform.forward);
            quaternion *= Quaternion.AngleAxis(m_effectData.ActiveRotation.x, Vector3.right);
            quaternion *= Quaternion.AngleAxis(m_effectData.ActiveRotation.y, Vector3.up);
            quaternion *= Quaternion.AngleAxis(m_effectData.ActiveRotation.z, Vector3.forward);

            this.transform.rotation = quaternion;
        }
        m_thisActive = true;
        this.transform.gameObject.SetActive(true);
    }

    public virtual void EffectPlay()
    {
        m_particleSystem.Play();
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    public virtual void CallDestory()
    {
        m_thisActive = false;
        m_effectData = null;
        m_particleSystem = null;
        m_timer = 0;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="_deltaTime"></param>
    public virtual void ThisObjectUpdate(float _deltaTime)
    {
        m_timer += _deltaTime;
        if(m_timer < m_effectData.ActiveTime) { return; }

        m_thisActive = false;
        this.transform.gameObject.SetActive(false);

    }
}
