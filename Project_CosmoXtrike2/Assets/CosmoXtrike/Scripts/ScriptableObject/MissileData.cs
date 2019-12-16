﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create MissileData")]
public class MissileData : BulletData
{
    [SerializeField] protected float m_rotationTimeSpeed = 0;
    [SerializeField] protected float m_trackingTime = 0;

    /// <summary>
    /// 回転速度
    /// </summary>
    public float RotationTimeSpeed { get => m_rotationTimeSpeed; }

    /// <summary>
    /// 追尾時間
    /// </summary>
    public float TrackingTime { get => m_trackingTime; }
}