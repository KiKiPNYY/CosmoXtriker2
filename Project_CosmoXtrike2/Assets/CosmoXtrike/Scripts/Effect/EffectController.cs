using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController
{
    private Effect m_effectOrigin = null;
    private List<Effect> m_effects = null;

    /// <summary>
    /// このコントローラーの所持してるBulletを返す
    /// </summary>
    public Effect ThisHaveEffect { get => m_effectOrigin; }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="_bullet"></param>
    public EffectController(Effect _effect)
    {
        m_effectOrigin = _effect;
        m_effects = new List<Effect>();
        m_effects.Add(CreateBullet());
    }
    
    public void EffectPlay(Transform _offsetTransform)
    {
        Effect effect = GetBullet();
        effect.Init(_offsetTransform);
        effect.EffectPlay();
    }

    /// <summary>
    /// Listの中で使用していないBulletを取得
    /// </summary>
    /// <returns></returns>
    private Effect GetBullet()
    {
        for (int i = 0; i < m_effects.Count; i++)
        {
            if (m_effects[i].ThisActive) { continue; }
            return m_effects[i];
        }

        m_effects.Add(CreateBullet());
        return m_effects[m_effects.Count - 1];
    }

    /// <summary>
    /// Listの中のBulletがすべて使用されていた場合生成する関数
    /// </summary>
    /// <returns></returns>
    private Effect CreateBullet()
    {
        Effect newEffect = null;
        GameObject bulletObj = Object.Instantiate(m_effectOrigin.ThisGameObject);
        bulletObj.name = m_effectOrigin.name + m_effects.Count + 1;
        newEffect = bulletObj.GetComponent<Effect>();
        newEffect.Init(newEffect.ThisGameObject.transform);
        return newEffect;
    }

    /// <summary>
    /// Updateのまとめ
    /// </summary>
    /// <param name="_deltaTime"></param>
    public void ThisControllerUpdate(float _deltaTime)
    {
        for (int i = 0; i < m_effects.Count; i++)
        {
            if (!m_effects[i].ThisActive) { continue; }
            m_effects[i].ThisObjectUpdate(_deltaTime);
        }
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    public void CallDestroy()
    {
        for (int i = 0; i < m_effects.Count; i++)
        {
            m_effects[i].CallDestory();
        }

        m_effectOrigin = null;
        m_effects = null;
    }

}
