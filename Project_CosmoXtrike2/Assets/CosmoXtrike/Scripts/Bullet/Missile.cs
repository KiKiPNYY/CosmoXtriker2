﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    [SerializeField] private MissileData m_missileData = null;

    private GameObject m_target = null;

    public virtual void Fire(Vector3 _instncePos, Vector3 _direction, ThisType _thisType, GameObject _target)
    {
        if(_target == null) { return; }
        m_target = _target;

        this.transform.position = _instncePos;
        m_instanceOrigin = this.transform.position;
        Quaternion rotation = Quaternion.LookRotation(_direction);
        this.transform.rotation = rotation;
        m_targetType = _thisType;
    }

    protected override void Move(float _deltaTime)
    {
        base.Move(_deltaTime);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.m_target.transform.rotation,Time.deltaTime * m_missileData.RotationTimeSpeed);
    }
}
