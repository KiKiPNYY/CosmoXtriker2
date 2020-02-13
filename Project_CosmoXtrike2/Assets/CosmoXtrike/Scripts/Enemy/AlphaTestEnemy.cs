using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DestoryEffect
{
    [SerializeField] private Transform m_effctPos = null;
    [SerializeField] private float m_effectTime = 0;

    public Transform EffectTrans => m_effctPos;
    public float EffectTime => m_effectTime;
}


public class AlphaTestEnemy : MonoBehaviour, CommonProcessing
{
    [SerializeField] private int m_maxHp;
    [SerializeField] private Effect m_burnEffect = null;
    [SerializeField] private Transform[] m_effctPos;
    [SerializeField] private DestoryEffect[] m_destoryEffects = null;
    [SerializeField] private Vector3 m_offset = Vector3.zero;
   // [SerializeField] private float m_destroyTime = 0;

    //private int m_hp;

    private bool m_destroy = false;
    private float m_timer = 0;
    private int m_effectPlayCount = 0;

    public int NowHP { get; private set; }
    public int MaxHP { get { return m_maxHp; } }
    public Vector3 OffSet => m_offset;

    public ThisType ReturnMyType()
    {
        return ThisType.Enemy;
    }

    public void Damege(int _addDamege)
    {
        if (m_destroy) { return; }
        NowHP -= _addDamege;
        if(NowHP > 0){return;}

        m_destroy = true;

        //SoundManager.Instnce.SEPlay("EnemyDestory", this.transform);
        
        //for(int i = 0; i < m_effctPos.Length; i++)
        //{
        //    EffectManager.Instnce.EffectPlay(m_burnEffect, m_effctPos[i]);
        //}
        //MainGameController.Instnce.EnemyDestroyAdd(m_destroyNum);
        //this.transform.gameObject.SetActive(false);
    }

    public int MeteoriteDamege()
    {
        return 0;
    }
    void Start()
    {
        NowHP = m_maxHp;
        m_destroy = false;
        m_timer = 0;
        m_effectPlayCount = 0;
    }

    private void FixedUpdate()
    {
        if (!m_destroy) { return; }

        m_timer += Time.deltaTime;
        if(m_timer < m_destoryEffects[m_effectPlayCount].EffectTime) { return; }

        SoundManager.Instnce.SEPlay("EnemyDestory", m_destoryEffects[m_effectPlayCount].EffectTrans);
        EffectManager.Instnce.EffectPlay(m_burnEffect, m_destoryEffects[m_effectPlayCount].EffectTrans);

        m_timer = 0;
        m_effectPlayCount++;
        if(m_effectPlayCount < m_destoryEffects.Length) { return; }

        MainGameController.Instnce.EnemyDestroyAdd(m_maxHp);
        this.transform.gameObject.SetActive(false);
    }
}
