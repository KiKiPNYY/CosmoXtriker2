using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    [SerializeField] private MissileData m_missileData = null;

    private GameObject m_target = null;

    public override void Init()
    {
        base.Init();
        m_target = null;
    }

    public override void Fire(Vector3 _instncePos, Vector3 _direction, ThisType _thisType, GameObject _target)
    {
        base.Fire(_instncePos, _direction, _thisType, _target);
        
        if (_target == null) { return; }
        m_target = _target;

    }

    protected override void Move(float _deltaTime)
    {
        base.Move(_deltaTime);
        if (m_target == null) { return; }
        Quaternion targetrotation = Quaternion.LookRotation((m_target.transform.position - this.transform.position).normalized);
        float magnification = Vector3.Distance(m_target.transform.position, this.transform.position);
        if(magnification > m_missileData.TrackingMaxDistance)
        {
            magnification = 0;
        }
        else
        {
            magnification = Mathf.Clamp((m_missileData.TrackingMaxDistance - magnification) / (m_missileData.TrackingMaxDistance - m_missileData.TrackingMinDistance),0,1);
        }
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetrotation, Time.deltaTime * m_missileData.RotationTimeSpeed * magnification);
    }

    public override void ThisObjectShowCheck(float _deltaTime)
    {
        if(m_target != null && !m_target.activeSelf)
        {
            Hidden();
        }

        base.ThisObjectShowCheck(_deltaTime);
    }

    public override void CallDestroy()
    {
        m_instanceOrigin = Vector3.zero;
        m_targetType = ThisType.Enemy;
        m_missileData = null;
        m_target = null;
    }
}
