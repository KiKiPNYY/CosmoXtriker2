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

    public AudioClip AudioClip { get => m_audioClip; }
    public float Volume { get => m_volume; }
    public float FadeTime { get => m_fadeTime; }
    public bool Loop { get => m_loop; }
}

[CreateAssetMenu(menuName = "CreateScriptable/Create SoundData")]
public class SoundData : ScriptableObject
{
    [SerializeField] private SoundParameter[] m_BGM = default;
    [SerializeField] private SoundParameter[] m_SE = default;
    [SerializeField] private int m_audioNum = 0;

    public SoundParameter[] BGMParameters { get => m_BGM; }
    public SoundParameter[] SEParameters { get => m_BGM; }
    public int AudioNum { get => m_audioNum; }

}
