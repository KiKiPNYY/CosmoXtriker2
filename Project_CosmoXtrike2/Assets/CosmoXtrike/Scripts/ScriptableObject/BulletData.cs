using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create BulletData")]
public class BulletData : ScriptableObject
{
   
    [SerializeField] protected float m_bulletSpeed = 0;
    [SerializeField] protected int m_damege = 0;
    [SerializeField] protected float m_activeTime = 0;

    /// <summary>
    ///  弾速
    /// </summary>
    public float BulletSpeed { get => m_bulletSpeed; }

    /// <summary>
    /// 生成位置からオブジェクトを非表示にするまでの距離
    /// </summary>
    public float ActiveTime { get => m_activeTime; }

    /// <summary>
    /// 相手に与えるダメージ数
    /// </summary>
    public int Damege { get => m_damege; }

}
