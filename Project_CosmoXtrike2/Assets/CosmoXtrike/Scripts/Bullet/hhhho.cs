using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hhhho : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Effect m_effect;
    [SerializeField] Transform[] m_transforms;
    void Start()
    {
        BulletManager.Instnce.AddBullet(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        for(int i = 0; i < m_transforms.Length; i++)
        {
            BulletManager.Instnce.Fire(bullet, m_transforms[i].position + m_transforms[i].forward * 5, ((m_transforms[i].position + m_transforms[i].forward) - m_transforms[i].position).normalized, ThisType.Player);
            EffectManager.Instnce.EffectPlay(m_effect, m_transforms[i]);
        }
    }
}
