using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTestEnemy : MonoBehaviour, CommonProcessing
{
    [SerializeField] private int m_maxHp;
    [SerializeField] private Effect m_burnEffect = null;

    [SerializeField] private Vector3[] m_effctPos;
    private int m_hp;
    public ThisType ReturnMyType()
    {
        return ThisType.Enemy;
    }

    public void Damege(int _addDamege)
    {
        m_hp -= _addDamege;
        if(m_hp > 0){return;}
        SoundManager.Instnce.SEPlay("EnemyDestory", this.transform);
        EffectManager.Instnce.EffectPlay(m_burnEffect,this.transform);
        for(int i = 0; i < m_effctPos.Length; i++)
        {
            
        }
        MainGameController.Instnce.EnemyDestroyAdd();
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
