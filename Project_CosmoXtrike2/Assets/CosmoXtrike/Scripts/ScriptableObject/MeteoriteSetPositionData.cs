using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create MeteoriteSetPositionData")]
public class MeteoriteSetPositionData : ScriptableObject
{
    [SerializeField] private Vector3 m_instanceOffset = Vector3.zero;
    [SerializeField] private float m_instanceRange = 0;
    [SerializeField] private int m_instanceNum = 0;
    [SerializeField] private Meteorite[] m_meteorites = null;

    /// <summary>
    /// 隕石の生成位置の基準地
    /// </summary>
    public Vector3 InstanceOffset { get => m_instanceOffset; }

    /// <summary>
    /// 基準地からのレンジ
    /// </summary>
    public float InstanceRange { get => m_instanceRange; }

    /// <summary>
    /// 隕石生成数
    /// </summary>
    public int InstanceNum { get => m_instanceNum; }

    /// <summary>
    /// 生成するMeteoriteクラス
    /// </summary>
    public Meteorite[] Meteorites { get => m_meteorites; }
}
