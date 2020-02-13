using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create MissileData")]
public class MissileData : BulletData
{
    [SerializeField] protected float m_rotationTimeSpeed = 0;
    [SerializeField] protected float m_trackingMaxDistance = 0;
    [SerializeField] protected float m_trackingMinDistance = 0;
    [SerializeField] protected float m_trackingTime = 0;

    /// <summary>
    /// 回転速度
    /// </summary>
    public float RotationTimeSpeed { get => m_rotationTimeSpeed; }

    public float TrackingMaxDistance { get => m_trackingMaxDistance; }

    public float TrackingMinDistance { get => m_trackingMinDistance; }

    public float TrackingTime => m_trackingTime;
}
