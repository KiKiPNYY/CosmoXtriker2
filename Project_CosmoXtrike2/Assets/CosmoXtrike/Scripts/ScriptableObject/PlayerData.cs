using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private Bullet m_bullet;
    [SerializeField] private Effect m_fireEffect;
    [SerializeField] private float m_acceleTime = 0;
    [SerializeField] private float m_bulletInterval = 0;
    [SerializeField] private float m_defaultSpeed = 0;
    [SerializeField] private float m_maxSpeed = 0;

    /// <summary>
    /// 使用するバレット
    /// </summary>
    public Bullet Bullet { get => m_bullet; }

    /// <summary>
    /// 使用するエフェクト
    /// </summary>
    public Effect Effect { get => m_fireEffect; }

    /// <summary>
    /// 加速する時間
    /// </summary>
    public float AcceleTime { get => m_acceleTime; }

    /// <summary>
    /// バレットを発射するインターバル時間
    /// </summary>
    public float BulletInterval { get => m_bulletInterval; }

    /// <summary>
    /// 通常時の速度
    /// </summary>
    public float DefaultSpeed { get => m_defaultSpeed; }

    /// <summary>
    /// 最大速度
    /// </summary>
    public float MaxSpeed { get => m_maxSpeed; }
}
