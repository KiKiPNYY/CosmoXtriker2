using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CreateScriptable/Create EnemyDate")]
public class EnemyDate : ScriptableObject
{
    [SerializeField]
    [Header("初期HP")]
    int hitpoint;
    [SerializeField]
    [Header("移動速度")]
    float speed = 1.0f;
    [SerializeField]
    [Header("攻撃力")]
    int attack;

    public int HP { get { return hitpoint; } }
    public float Speed { get { return speed; } }
    public int Attack { get { return attack; } }
}