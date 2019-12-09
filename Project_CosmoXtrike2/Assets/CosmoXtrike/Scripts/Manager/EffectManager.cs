using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    #region シングルトン
    private static EffectManager m_instance = null;

    public static EffectManager Instnce
    {
        get
        {
            if (m_instance == null)
            {
                Debug.LogError("BulletManagerがありません");
            }
            return m_instance;
        }
    }

    /// <summary>
    /// シングルトン作成
    /// </summary>
    private void CreateInstnce()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }
    #endregion

    [SerializeField] private UseEffectData m_useEffectData = null;

    private List<EffectController> m_effectControllers = new List<EffectController>();

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        m_effectControllers = new List<EffectController>();

        for(int i = 0; i < m_useEffectData.Effects.Length; i++)
        {
            AddEffect(m_useEffectData.Effects[i]);
            m_useEffectData.Effects[i].Init(this.transform);
        }

    }

    /// <summary>
    /// エフェクトの追加
    /// </summary>
    /// <param name="_effect"></param>
    public void AddEffect(Effect _effect)
    {
        for (int i = 0; i < m_effectControllers.Count; i++)
        {
            if(m_effectControllers[i] != null) { continue; }
            if (m_effectControllers[i].ThisHaveEffect == _effect) { return; }
        }

        EffectController effectController = new EffectController(_effect);
        m_effectControllers.Add(effectController);
    }

    /// <summary>
    /// エフェクトの再生
    /// </summary>
    /// <param name="_effect"></param>
    /// <param name="_offsetTransform"></param>
    public void EffectPlay(Effect _effect ,Transform _offsetTransform)
    {
        for (int i = 0; i < m_effectControllers.Count; i++)
        {
            if (m_effectControllers[i].ThisHaveEffect == null) { continue; }
            if (m_effectControllers[i].ThisHaveEffect != _effect) { continue; }
            m_effectControllers[i].EffectPlay(_offsetTransform);
            break;
        }
    }

    #region Unity関数

    void Start()
    {
        Init();
    }

    private void Awake()
    {
        CreateInstnce();
    }

    private void FixedUpdate()
    {
        float deltaTime = Time.deltaTime;

        for(int i = 0; i < m_effectControllers.Count; i++)
        {
            m_effectControllers[i].ThisControllerUpdate(deltaTime);
        }
    }

    #endregion
}
