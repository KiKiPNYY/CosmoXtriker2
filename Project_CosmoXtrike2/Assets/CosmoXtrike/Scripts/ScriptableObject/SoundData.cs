using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundParameter
{
    [SerializeField] private AudioClip m_audioClip = null;
    [SerializeField][Range(0,1)] private float m_volume = 0;
    [SerializeField] private float m_fadeTime = 0;
    [SerializeField] private bool m_loop = false;
    [SerializeField] private bool m_3DSound;

    /// <summary>
    /// 使用するAudioClip
    /// </summary>
    public AudioClip AudioClip { get => m_audioClip; }

    /// <summary>
    /// サウンドのボリューム
    /// </summary>
    public float Volume { get => m_volume; }

    /// <summary>
    /// フェードする時間
    /// </summary>
    public float FadeTime { get => m_fadeTime; }

    /// <summary>
    /// ループするかのチェック
    /// </summary>
    public bool Loop { get => m_loop; }

    /// <summary>
    /// サウンドが立体音響かのチェック
    /// </summary>
    public bool SoundType3D { get => m_3DSound; }
}

[CreateAssetMenu(menuName = "CreateScriptable/Create SoundData")]
public class SoundData : ScriptableObject
{
    [SerializeField] private SoundParameter[] m_BGM = default;
    [SerializeField] private SoundParameter[] m_SE = default;
    [SerializeField][Range(1,20)] private int m_audioNum = 0;

    /// <summary>
    /// BGMのパラメーター
    /// </summary>
    public SoundParameter[] BGMParameters { get => m_BGM; }

    /// <summary>
    /// SEのパラメーター
    /// </summary>
    public SoundParameter[] SEParameters { get => m_SE; }

    /// <summary>
    /// シーン上に生成するサウンドオブジェクトの数
    /// </summary>
    public int AudioNum { get => m_audioNum; }

}
