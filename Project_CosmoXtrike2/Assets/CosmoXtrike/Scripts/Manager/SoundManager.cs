using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("SoundManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (SoundManager)this;
            DontDestroyOnLoad(this.gameObject);
            return true;
        }
        else if (Instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
            return true;
        }

        Destroy(this);
        return false;
    }
    #endregion

    [SerializeField]
    private AudioClip[] BGM = default;
    [SerializeField]
    private AudioClip[] SE = default;

    private AudioSource[] audioSource;
    [SerializeField]
    float fadeTime = 0.5f;

    public enum SE_Name
    {
        SE_00_AAA,
        SE_01_BBB,
        SE_02_CCC,
    };

    public enum BGM_Name
    {
        BGM_00_Opening,
        BGM_01_Game,
    };

    void Awake()
    {
        CheckInstance();
        audioSource = GetComponents<AudioSource>();
    }

    private void Start()
    {

    }

    /// <summary>
    /// その時間軸内でのBGM切り替え
    /// </summary>
    /// <param name="_Name"></param>
    public void PlayBGM(BGM_Name _Name)
    {
        switch (_Name)
        {
            case BGM_Name.BGM_00_Opening:
                break;
            case BGM_Name.BGM_01_Game:
                break;
            default:
                Debug.Log("不明な値");
                break;
        }
    }

    /// <summary>
    /// fadein
    /// </summary>
    IEnumerator FadeIn()
    {
        float time = 0;
        audioSource[0].Play();
        while (time <= fadeTime)
        {
            audioSource[0].volume = time / fadeTime;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        audioSource[0].volume = 1;
        yield break;
    }

    /// <summary>
    /// fadeout
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        bool act1 = audioSource[1].isPlaying;
        float time = 0;
        while (time <= fadeTime)
        {
            if (act1)
            {
                audioSource[1].volume = 1 - (time / fadeTime);
                audioSource[0].volume = (1 - (time / fadeTime)) / 2;
            }
            else
            {
                audioSource[0].volume = 1 - (time / fadeTime);
            }
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        audioSource[0].volume = 0;
        audioSource[0].Stop();
        if (act1)
        {
            audioSource[1].volume = 0;
            audioSource[1].Stop();
        }
        yield break;
    }

    /// <summary>
    /// 止める
    /// </summary>
    public void StopBGM()
    {
        StartCoroutine(FadeOut());
    }

    /// <summary>SEの再生(音量調整)</summary>
    /// <param name="_Name"></param>
    /// <param name="_Vol"></param>
    public void PlaySE(SE_Name _Name, float _Vol = 1)
    {
        audioSource[2].PlayOneShot(SE[(int)_Name], _Vol);
    }
    public void PlaySE(int num, float _Vol = 1)
    {
        audioSource[2].PlayOneShot(SE[num], _Vol);
    }

}