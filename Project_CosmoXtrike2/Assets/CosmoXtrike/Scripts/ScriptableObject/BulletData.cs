using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create BulletData")]
public class BulletData : ScriptableObject
{

    [SerializeField] private float m_bulletSpeed = 0;
    [SerializeField] private int m_damege = 0;
    [SerializeField] private float m_instanceDistance = 0;
    
    // 弾速
    public float BulletSpeed { get => m_bulletSpeed; }

    //生成位置からオブジェクトを非表示にするまでの距離
    public float InstanceDistance { get => m_instanceDistance; }

    // 相手に与えるダメージ数
    public int Damege { get => m_damege; }

}
