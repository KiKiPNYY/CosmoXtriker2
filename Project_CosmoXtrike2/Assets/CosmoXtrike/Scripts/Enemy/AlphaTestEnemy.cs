using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTestEnemy : MonoBehaviour, CommonProcessing
{
    [SerializeField] private int m_maxHp;
    [SerializeField] private Effect m_burnEffect = null;
    [SerializeField] private Transform[] m_effctPos;
    [SerializeField] private int m_destroyNum = 0;

    private int m_hp;

    public int NowHP { get { return m_hp; } }
    public int MaxHP { get { return m_maxHp; } }
    public ThisType ReturnMyType()
    {
        return ThisType.Enemy;
    }

    public void Damege(int _addDamege)
    {
        m_hp -= _addDamege;
        if(m_hp > 0){return;}
        SoundManager.Instnce.SEPlay("EnemyDestory", this.transform);
        
        for(int i = 0; i < m_effctPos.Length; i++)
        {
            EffectManager.Instnce.EffectPlay(m_burnEffect, m_effctPos[i]);
        }
        MainGameController.Instnce.EnemyDestroyAdd(m_destroyNum);
        this.transform.gameObject.SetActive(false);
    }

    public int MeteoriteDamege()
    {
        return 0;
    }
    void Start()
    {
        m_hp = m_maxHp;
    }
}
