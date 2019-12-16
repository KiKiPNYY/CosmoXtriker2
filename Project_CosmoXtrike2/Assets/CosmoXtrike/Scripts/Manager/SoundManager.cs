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
        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }
    #endregion

    private AudioSource[] m_audioSources = default;

    private SoundData m_soundData = default;

    private int[] m_BGMHash = default;
    private int[] m_SEHash = default;

    void Awake()
    {
        CreateInstnce();
    }

    /// <summary>
    /// シーン移動後に初期化
    /// </summary>
    /// <param name="_soundData"></param>
    public void SceneStart(SoundData _soundData)
    {
        m_soundData = _soundData;

        m_audioSources = new AudioSource[m_soundData.AudioNum];
        for (int i = 0; i < m_audioSources.Length; i++)
        {
            GameObject audioObject = new GameObject();
            audioObject.AddComponent<AudioSource>();
            m_audioSources[i] = audioObject.GetComponent<AudioSource>();
        }
    
        m_audioSources[0].clip = m_soundData.BGMParameters[0].AudioClip;
        m_audioSources[0].loop = m_soundData.BGMParameters[0].Loop;
        m_audioSources[0].volume = m_soundData.BGMParameters[0].Volume;

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
        for (int i = 0; i < m_audioSources.Length; i++)
        {
            m_audioSources[i].clip = null;
        }
        m_audioSources = null;
    }

    public void SEPlay(string _SEname)
    {
        int SEHash = _SEname.GetHashCode();
    }

    public void SEFade(string _SEname)
    {
        int SEHash = _SEname.GetHashCode();
    }

    public void BGMPlay(string _BGMname)
    {
        int BGMHash = _BGMname.GetHashCode();
    }

    public void BGMFade(string _BGMname)
    {
        int BGMHash = _BGMname.GetHashCode();
    }
}