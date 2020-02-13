using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAnimation : MonoBehaviour
{
    [SerializeField] private Transform[] m_animationPlayTrans = null;
    [SerializeField] private Effect m_effect = null;
    [SerializeField] private float m_animePlayTime = 0;

    private float m_timer = 0;
    private int counter = 0;

    void Start()
    {
        m_timer = 0;
        counter = 0;
    }

    void FixedUpdate()
    {
        m_timer += Time.deltaTime;

        if(m_animePlayTime > m_timer) { return; }

        EffectManager.Instnce.EffectPlay(m_effect, m_animationPlayTrans[counter]);
        counter++;
        if(m_animationPlayTrans.Length > counter) { return; }
        counter = 0;
    }
}
