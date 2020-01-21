using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region シングルトン
    private static SoundManager m_instance = null;

    public static SoundManager Instnce
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
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }
    #endregion

    private SoundObject[] m_soundObjects = default;

    private SoundData m_soundData = default;

    private int[] m_BGMHash = default;
    private int[] m_SEHash = default;

    /// <summary>
    /// 初期化
    /// </summary>
    public SoundManager()
    {
        m_soundObjects = new SoundObject[0];
        m_soundData = null;
        m_BGMHash = new int[0];
        m_SEHash = new int[0];
    }

    /// <summary>
    /// シーン移動後に初期化
    /// </summary>
    /// <param name="_soundData"></param>
    public void SceneStart(SoundData _soundData)
    {
        m_soundData = _soundData;

        m_soundObjects = new SoundObject[m_soundData.AudioNum];
        for (int i = 0; i < m_soundObjects.Length; i++)
        {
            GameObject audioObject = new GameObject();
            audioObject.AddComponent<SoundObject>();
            audioObject.AddComponent<AudioSource>();
            audioObject.name = "audioObject" + (i + 1).ToString();
            m_soundObjects[i] = audioObject.GetComponent<SoundObject>();
            m_soundObjects[i].Init();
        }

        if (m_soundObjects.Length < 1) { return; }

       // m_soundObjects[0].Init();
      
        m_soundObjects[0].SoundPlay(m_soundData.BGMParameters[0].AudioClip, 0, m_soundData.BGMParameters[0].Loop, m_soundData.BGMParameters[0].SoundType3D, null);
        m_soundObjects[0].FadeCall(FadeType.FadeIN, m_soundData.BGMParameters[0].FadeTime, m_soundData.BGMParameters[0].Volume);

        m_BGMHash = new int[m_soundData.BGMParameters.Length];
        m_SEHash = new int[_soundData.SEParameters.Length];

        for (int i = 0; i < m_BGMHash.Length; i++)
        {
            m_BGMHash[i] = m_soundData.BGMParameters[i].AudioClip.name.GetHashCode();
        }

        for (int i = 0; i < m_SEHash.Length; i++)
        {
            m_SEHash[i] = m_soundData.SEParameters[i].AudioClip.name.GetHashCode();
        }

    }

    /// <summary>
    /// シーン移動するときに初期化
    /// </summary>
    public void SceneEnd()
    {
        m_soundData = null;
        for (int i = 0; i < m_soundObjects.Length; i++)
        {
            m_soundObjects[i].End();
        }
        m_soundObjects = null;
    }

    /// <summary>
    /// SE再生
    /// </summary>
    /// <param name="_SEname"></param>
    public void SEPlay(string _SEname, Transform generatTrans = null)
    {
        if (m_soundObjects.Length < 1) { return; }
        
        int SEHash = _SEname.GetHashCode();
        
        int recordSENum = -1;
        for (int i = 0; i < m_SEHash.Length; i++)
        {
            if (SEHash != m_SEHash[i]) { continue; }
            recordSENum = i;
            break;
        }

        if (recordSENum < 0) { return; }
        
        float maxSoundPlayTime = -1;
        int recordObjectNum = -1;
        for (int i = 1; i < m_soundObjects.Length; i++)
        {
            if (!m_soundObjects[i].SoundPlayNow)
            {
                
                m_soundObjects[i].Init();
                m_soundObjects[i].SoundPlay(m_soundData.SEParameters[recordSENum].AudioClip, m_soundData.SEParameters[recordSENum].Volume, m_soundData.SEParameters[recordSENum].Loop, m_soundData.SEParameters[recordSENum].SoundType3D, generatTrans);
                return;
            }
            if (m_soundObjects[i].SoundPlayTime < maxSoundPlayTime) { continue; }
            maxSoundPlayTime = m_soundObjects[i].SoundPlayTime;
            recordObjectNum = i;
        }

        if (recordObjectNum < 1) { return; }
        m_soundObjects[recordObjectNum].SoundPlay(m_soundData.SEParameters[recordSENum].AudioClip, m_soundData.SEParameters[recordSENum].Volume, m_soundData.SEParameters[recordSENum].Loop, m_soundData.SEParameters[recordSENum].SoundType3D, generatTrans);

    }

    /// <summary>
    /// SEフェード
    /// </summary>
    /// <param name="_SEname"></param>
    public void SEFade(FadeType _fadeType, float _fadeTime, bool _allFadeOut, string _SEname = null, Transform generatTrans = null)
    {
        if (m_soundObjects.Length < 1) { return; }

        if (_fadeType == FadeType.Nun) { return; }

        if (_allFadeOut && _fadeType == FadeType.fadeOut)
        {
            for (int i = 1; i < m_soundObjects.Length; i++)
            {
                m_soundObjects[i].FadeCall(_fadeType, _fadeTime, 0);
            }
        }

        if (_SEname == null) { return; }

        int SEHash = _SEname.GetHashCode();
        int recordSENum = -1;
        for (int i = 0; i < m_SEHash.Length; i++)
        {
            if (SEHash != m_SEHash[i]) { continue; }
            recordSENum = i;
            break;
        }

        if (recordSENum < 0) { return; }

        for (int i = 1; i < m_soundObjects.Length; i++)
        {
            if (m_soundObjects[i].PlaySoundHash != m_SEHash[recordSENum]) { continue; }
            if (_fadeType == FadeType.FadeIN)
            {
                m_soundObjects[i].Init();
                m_soundObjects[i].SoundPlay(m_soundData.SEParameters[recordSENum].AudioClip, 0, m_soundData.SEParameters[recordSENum].Loop, m_soundData.SEParameters[recordSENum].SoundType3D, generatTrans);
                m_soundObjects[i].FadeCall(_fadeType, _fadeTime, m_soundData.SEParameters[recordSENum].Volume);
                return;
            }
            else
            {
                m_soundObjects[i].FadeCall(_fadeType, _fadeTime, 0);
            }
        }
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    /// <param name="_BGMname"></param>
    public void BGMPlay(string _BGMname)
    {
        if (m_soundObjects.Length < 1) { return; }

        int BGMHash = _BGMname.GetHashCode();
        int recordBGMNum = -1;
        for (int i = 0; i < m_BGMHash.Length; i++)
        {
            if (BGMHash != m_BGMHash[i]) { continue; }
            recordBGMNum = i;
            break;
        }

        if (recordBGMNum < 0) { return; }

        m_soundObjects[0].SoundPlay(m_soundData.BGMParameters[recordBGMNum].AudioClip, m_soundData.BGMParameters[recordBGMNum].Volume, m_soundData.BGMParameters[recordBGMNum].Loop, m_soundData.BGMParameters[recordBGMNum].SoundType3D, null);
    }

    /// <summary>
    /// BGMフェード
    /// </summary>
    /// <param name="_BGMname"></param>
    public void BGMFade(float _fadeTime, FadeType _fadeType, string _BGMname = null)
    {
        if (m_soundObjects.Length < 1) { return; }

        if (_fadeType == FadeType.fadeOut)
        {
            m_soundObjects[0].FadeCall(_fadeType, _fadeTime, 0);
            return;
        }

        int BGMHash = _BGMname.GetHashCode();
        int recordBGMNum = -1;
        for (int i = 0; i < m_BGMHash.Length; i++)
        {
            if (BGMHash != m_BGMHash[i]) { continue; }
            recordBGMNum = i;
            break;
        }

        if (recordBGMNum < 0) { return; }
        m_soundObjects[0].Init();
        m_soundObjects[0].SoundPlay(m_soundData.BGMParameters[recordBGMNum].AudioClip, 0, m_soundData.BGMParameters[recordBGMNum].Loop, m_soundData.BGMParameters[recordBGMNum].SoundType3D, null);
        m_soundObjects[0].FadeCall(_fadeType, _fadeTime, m_soundData.BGMParameters[recordBGMNum].Volume);
    }

    #region Unity関数

    void Awake()
    {
        CreateInstnce();
    }

    private void FixedUpdate()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < m_soundObjects.Length; i++)
        {
            if (!m_soundObjects[i].UpdateAction) { continue; }
            m_soundObjects[i].ThisObjectUpdate(deltaTime);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < m_soundObjects.Length; i++)
        {
            m_soundObjects[i].End();
        }
    }

    #endregion
}