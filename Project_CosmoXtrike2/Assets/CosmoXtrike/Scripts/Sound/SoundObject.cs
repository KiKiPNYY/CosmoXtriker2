using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FadeType
{
    Nun, fadeOut, FadeIN
}


public class SoundObject : MonoBehaviour
{
    private float m_aoudioTime = 0;
    private float m_fadeTime = 0;
    private float m_volume = 0;
    private float m_recordVolume = 0;
    private FadeType m_fade = FadeType.Nun;
    private AudioSource m_audioSource = null;

    /// <summary>
    /// 初期化
    /// </summary>
    public SoundObject()
    {
        m_aoudioTime = 0;
        m_fadeTime = 0;
        m_volume = 0;
        m_recordVolume = 0;
        m_fade = FadeType.Nun;
        m_audioSource = null;
    }

    /// <summary>
    /// Updateを行うかのチェック
    /// </summary>
    public bool UpdateAction { get; private set; }

    /// <summary>
    /// サウンドを再生しているかのチェック
    /// </summary>
    public bool SoundPlayNow { get; private set; }

    /// <summary>
    /// 再生中のサウンドのハッシュ値を取得
    /// </summary>
    public int PlaySoundHash
    {
        get
        {
            if(m_audioSource.clip ==null)
            {
                return int.MaxValue;
            }
            return m_audioSource.clip.GetHashCode();
        }
    }

    /// <summary>
    /// 再生しているサウンドの割合を0～1の間で返す
    /// </summary>
    public float SoundPlayTime { get => Mathf.Clamp((m_aoudioTime / m_audioSource.clip.length), 0, 1); }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init()
    {
        m_aoudioTime = 0;
        m_fadeTime = 0;
        m_volume = 0;
        m_recordVolume = 0;
        m_fade = FadeType.Nun;
        UpdateAction = false;
        SoundPlayNow = false;

        if (m_audioSource == null)
        {
            
            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
        }
        m_audioSource.clip = null;

        if(transform.parent == null) { return; }
        transform.parent = null;
    }

    /// <summary>
    /// サウンド再生
    /// </summary>
    /// <param name="_audioClip"></param>
    /// <param name="_volume"></param>
    /// <param name="_loop"></param>
    /// <param name="_3DSound"></param>
    /// <param name="_parent"></param>
    public void SoundPlay(AudioClip _audioClip, float _volume, bool _loop, bool _3DSound, Transform _parent)
    {
        
        m_volume = _volume;

        m_audioSource.clip = _audioClip;
        m_audioSource.volume = m_volume;
        m_audioSource.loop = _loop;
        m_audioSource.spatialBlend = _3DSound == true ? 1 : 0;

        if (!m_audioSource.loop)
        {
            UpdateAction = true;
        }

        SoundPlayNow = true;
        m_audioSource.Play();

        if (_parent == null) { return; }

        transform.position = _parent.position;
        transform.parent = _parent;
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void End()
    {
        m_aoudioTime = 0;
        m_fadeTime = 0;
        m_recordVolume = 0;
        UpdateAction = false;
        SoundPlayNow = false;
        m_fade = FadeType.Nun;
        if(m_audioSource != null)
        {
            m_audioSource.clip = null;
        }

    }

    /// <summary>
    /// サウンドのフェード実行
    /// </summary>
    /// <param name="_fadeType"></param>
    /// <param name="_fadeTime"></param>
    /// <param name="_volume"></param>
    public void FadeCall(FadeType _fadeType, float _fadeTime, float _volume)
    {
        if (_fadeType == FadeType.Nun || (_fadeType == FadeType.fadeOut && UpdateAction)) { return; }
        UpdateAction = true;

        m_fade = _fadeType;
        m_aoudioTime = 0;
        m_fadeTime = _fadeTime;
        if (m_fade == FadeType.FadeIN)
        {
            m_volume = _volume;
            return;
        }

        m_recordVolume = m_audioSource.volume;
    }

    /// <summary>
    /// このオブジェクトのUpdate
    /// </summary>
    /// <param name="_deltaTime"></param>
    public void ThisObjectUpdate(float _deltaTime)
    {
        m_aoudioTime += _deltaTime;

        if (m_fade == FadeType.FadeIN)
        {
            float fadevolume = Mathf.Clamp(m_aoudioTime / m_fadeTime, 0, 1);
            fadevolume = m_volume * fadevolume;
            m_audioSource.volume = fadevolume;
            if (fadevolume < 1) { return; }
            m_aoudioTime = 0;
            m_fade = FadeType.Nun;
            UpdateAction = false;
            return;
        }
        else if (m_fade == FadeType.fadeOut)
        {

            float fadevolume = Mathf.Clamp(m_aoudioTime / m_fadeTime, 0, 1);

            fadevolume = m_recordVolume - (m_recordVolume * fadevolume);
            m_audioSource.volume = fadevolume;

            if (fadevolume < 1) { return; }

            End();
            return;
        }

        if (m_audioSource.loop) { return; }
        if (m_aoudioTime < m_audioSource.clip.length) { return; }
        End();
    }

}
