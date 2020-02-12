using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    #region シングルトン
    private static TitleController m_instance = null;

    public static TitleController Instnce
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
          //  DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }
    #endregion

    [SerializeField] [Range(1, 10)] private float m_fadeTime = 1f;
    [SerializeField] [Range(1, 10)] private float m_cameraFadeTime = 1f;
    [SerializeField] [Range(0, 1)] private float m_changeFadeTime = 0.6f;
    [SerializeField] private Text m_text = null;
    [SerializeField] private SoundData m_soundData = null;
    [SerializeField] private RawImage m_fadeImage = null;

    private float m_timer = 0;
    private float m_fadeTimer = 0;
    private bool m_SceneMove = false;
    private FadeType m_fadeType = FadeType.Nun;

    /// <summary>
    /// 初期化
    /// </summary>
    public TitleController()
    {
        m_timer = 0;
        m_fadeTimer = 0;
        m_SceneMove = false;
        m_fadeType = FadeType.Nun;
    }

    /// <summary>
    /// シーン遷移のチェック
    /// </summary>
    public void ChangeScene()
    {
        if (m_SceneMove) { return; }
        // if (!Input.GetButtonDown("LeftTrigger") && !Input.GetButtonDown("RightTrigger") && !Input.GetKeyDown(KeyCode.Space)) { return; }

        SoundManager.Instnce.BGMFade(1, FadeType.fadeOut);
        //SoundManager.Instnce.SEFade(FadeType.fadeOut, 1, true);

        SoundManager.Instnce.SEPlay("TitleClick");
        m_SceneMove = true;
        m_timer = 0;
        m_fadeTimer = 0;
        m_fadeType = FadeType.fadeOut;
    }

    /// <summary>
    /// テキストのUpdates
    /// </summary>
    private void TextUpdate(float _deltaTime)
    {
        if (m_SceneMove)
        {
            m_timer += _deltaTime / m_changeFadeTime;
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, Mathf.Round(Mathf.Abs(Mathf.Sin(m_timer))));
            return;
        }
        m_timer += Time.deltaTime / m_fadeTime;
        m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, Mathf.Abs(Mathf.Sin(m_timer)));

    }

    private void FadeUpdate(float _deltaTime)
    {
        if (m_fadeType == FadeType.Nun) { return; }

        m_fadeTimer = Mathf.Clamp(m_fadeTimer + _deltaTime / m_cameraFadeTime, 0, 1);

        if (m_fadeType == FadeType.FadeIN)
        {
            m_fadeImage.color = new Color(m_fadeImage.color.r, m_fadeImage.color.g, m_fadeImage.color.b, 1 - m_fadeTimer);
        }
        else if (m_fadeType == FadeType.fadeOut)
        {
            m_fadeImage.color = new Color(m_fadeImage.color.r, m_fadeImage.color.g, m_fadeImage.color.b, m_fadeTimer);
        }

        if (m_fadeTimer < 1) { return; }

        m_fadeTimer = 0;
        m_fadeType = FadeType.Nun;

        if (!m_SceneMove) { return; }

        SceneLoadManager.Instnce.LoadScene("Game");
    }

    #region Unity関数

    private void Awake()
    {
        CreateInstnce();
    }

    private void Start()
    {
        SoundManager.Instnce.SceneStart(m_soundData);
        m_fadeType = FadeType.FadeIN;
        m_fadeImage.color = new Color(m_fadeImage.color.r, m_fadeImage.color.g, m_fadeImage.color.b, 1);
    }

    //private void Update()
    //{
    //    ChangeScene();
    //}

    private void FixedUpdate()
    {
        float deltaTime = Time.deltaTime;
        TextUpdate(deltaTime);
        FadeUpdate(deltaTime);
    }
    #endregion

}
