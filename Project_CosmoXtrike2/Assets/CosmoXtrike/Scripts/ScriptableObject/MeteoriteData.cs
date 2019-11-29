using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create MeteoriteData")]
public class MeteoriteData : ScriptableObject
{
    [SerializeField] private Vector3 m_minRotation = Vector3.zero;
    [SerializeField] private Vector3 m_maxRotation = Vector3.zero;
    [SerializeField] private ForceMode m_forceMode = ForceMode.Acceleration;
    [SerializeField] private int m_playerDamege = 0;
    [SerializeField] private int m_maxHp = 0;

    /// <summary>
    /// 回転する最低速度
    /// </summary>
    public Vector3 MinRotation { get => m_minRotation; }

    /// <summary>
    /// 回転する最高速度
    /// </summary>
    public Vector3 MaxRotation { get => m_maxRotation; }

    /// <summary>
    /// 回転するときの加える力のモード
    /// </summary>
    public ForceMode ForceMode { get => m_forceMode; }

    /// <summary>
    /// ダメージ数
    /// </summary>
    public int PlayerDamege { get => m_playerDamege; }

    /// <summary>
    /// 最大体力
    /// </summary>
    public int MaxHp { get => m_maxHp; }
}
