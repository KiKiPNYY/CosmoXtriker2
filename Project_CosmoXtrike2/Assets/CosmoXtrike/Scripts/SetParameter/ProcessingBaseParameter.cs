using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ThisType
{
    Enemy,Player
}

public interface CommonProcessing
{
    ThisType ReturnMyType();
    /// <summary>
    /// ダメージ関数
    /// </summary>
    /// <param name="_addDamege"></param>
    void Damege(int _addDamege);
}

[CreateAssetMenu(menuName = "CreateScriptable/Create ProcessingBaseParameterData")]
public class ProcessingBaseParameterData : ScriptableObject
{
    [SerializeField] private int m_maxHp;
    [SerializeField] private float m_maxMoveSpeed;
    [SerializeField] private float m_minMoveSpeed;

    /// <summary>
    /// 最大体力
    /// </summary>
    public int MaxHp { get => m_maxHp; }

    /// <summary>
    /// 最大速度
    /// </summary>
    public float MaxMoveSpeed { get => m_maxMoveSpeed; }

    /// <summary>
    /// 最小速度
    /// </summary>
    public float MinMoveSpeed { get => m_minMoveSpeed; }
}
