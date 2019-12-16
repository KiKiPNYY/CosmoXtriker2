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
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }
    #endregion

    [SerializeField] [Range(1, 10)] private float m_fadeTime = 1f;
    [SerializeField] [Range(0, 1)] private float m_changeFadeTime = 0.6f;
    [SerializeField] private Text m_text = null;
    [SerializeField] private SoundData m_soundData = null;

    private float m_timer = 0;
    private bool m_isChange = false;

    /// <summary>
    /// 初期化
    /// </summary>
    public TitleController()
    {
        m_timer = 0;
        m_isChange = false;
    }

    /// <summary>
    /// シーン遷移のチェック
    /// </summary>
    private void ChangeScene()
    {
        if (m_isChange) { return; }
        if (!Input.GetButtonDown("LeftTrigger") && !Input.GetButtonDown("RightTrigger") && !Input.GetKeyDown(KeyCode.Space)) { return; }

        m_isChange = true;
        m_timer = 0;
        SoundManager.Instnce.BGMFade(1, FadeType.fadeOut);
        SoundManager.Instnce.SEFade(FadeType.fadeOut, 1, true);
        SceneLoadManager.Instnce.LoadScene("Game");
    }

    /// <summary>
    /// テキストのUpdates
    /// </summary>
    private void TextUpdate()
    {
        
        if (m_isChange)
        {
            m_timer += Time.deltaTime / m_changeFadeTime;
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, Mathf.Round(Mathf.Abs(Mathf.Sin(m_timer))));
            return;
        }
        m_timer += Time.deltaTime / m_fadeTime;
        m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, Mathf.Abs(Mathf.Sin(m_timer)));

    }

    #region Unity関数

    private void Start()
    {
        SoundManager.Instnce.SceneStart(m_soundData);
    }

    private void Update()
    {
        ChangeScene();
    }

    private void FixedUpdate()
    {
        TextUpdate();
    }
    #endregion

}
