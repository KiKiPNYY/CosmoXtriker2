using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private Bullet m_bullet;
    [SerializeField] private Effect m_fireEffect;
    [SerializeField] private Effect m_explosion = null;
    [SerializeField] private float m_acceleTime = 0;
    [SerializeField] private float m_bulletInterval = 0;
    [SerializeField] private float m_defaultSpeed = 0;
    [SerializeField] private float m_maxSpeed = 0;
    [SerializeField] private float m_sphereCastRadius = 0;
    [SerializeField] private float m_sphereCastDistance = 0;
    [SerializeField] private int m_maxHP = 0;
    [SerializeField] private int m_meteoriteDamege = 0;
    [SerializeField] private PlayerLookCursor m_targetCursor = null;
    [SerializeField] private float m_explosionTime = 0;
    [SerializeField] private float m_fadeTime = 0;


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

    /// <summary>
    /// SphereCastの半径
    /// </summary>
    public float SphereCastRadius { get => m_sphereCastRadius; }

    /// <summary>
    /// SphereCast距離
    /// </summary>
    public float SphereCastDistance { get => m_sphereCastDistance; }

    /// <summary>
    /// 最大HP
    /// </summary>
    public int MaxHp { get => m_maxHP; }

    /// <summary>
    /// 隕石に与えるダメージ
    /// </summary>
    public int MeteoriteDamege { get => m_meteoriteDamege; }

    public Effect Explosion => m_explosion;

    public PlayerLookCursor PlayerLookCursor { get => m_targetCursor; }

    public float FadeTime => m_fadeTime;

    public float ExplosionTime => m_explosionTime;
}
