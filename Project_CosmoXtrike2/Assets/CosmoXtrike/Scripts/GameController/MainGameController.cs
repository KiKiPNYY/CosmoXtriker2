using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{
    #region シングルトン
    private static MainGameController m_instance = null;

    public static MainGameController Instnce
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

    [SerializeField][Range(1, 20)] private int m_enemyDestroy = 1;
    [SerializeField] [Range(60, 300)] private float m_gamePlayTime = 60;
    [SerializeField] [Range(0.1f, 10)] private float m_fadeTime = 0.1f;
    [SerializeField] private RawImage m_rawImage = null;
    [SerializeField] private SoundData m_soundData = null;

    private float m_timer = 0;
    private float m_fadeTimer = 0;
    private int m_destroyNum = 0;
    private bool m_sceneMove = false;
    private bool m_sceneStart = false;
    private FadeType m_fade = FadeType.Nun;

    /// <summary>
    /// 初期化
    /// </summary>
    public MainGameController()
    {
        m_timer = 0;
        m_fadeTimer = 0;
        m_enemyDestroy = 0;
        m_sceneMove = false;
        m_sceneStart = false;
        m_fade = FadeType.Nun;
    }

    /// <summary>
    /// エネミーを破壊したときに撃破数の加算
    /// </summary>
    public void EnemyDestroyAdd()
    {
        m_destroyNum++;
        if(m_destroyNum < m_enemyDestroy) { return; }
        SceneMove();
    }

    /// <summary>
    /// メインゲームが終わったときに呼びゲームを遷移させる
    /// </summary>
    private void SceneMove()
    {
        SoundManager.Instnce.BGMFade(1,FadeType.fadeOut);
        SoundManager.Instnce.SEFade(FadeType.fadeOut,1,true);
        SceneLoadManager.Instnce.LoadScene("Title");
    }

    public void MainGameEnd()
    {
        m_sceneMove = true;
        m_fade = FadeType.fadeOut;
    }

    #region Unity関数
    private void Start()
    {
        CosmoXtrikerController.CallCameraSetting();
        m_fade = FadeType.FadeIN;
        m_fadeTimer = 0;
        m_sceneMove = false;
        m_sceneStart = false;
        m_rawImage.color = new Color(m_rawImage.color.r, m_rawImage.color.g, m_rawImage.color.b, 1);
        SoundManager.Instnce.SceneStart(m_soundData);
    }

    private void FixedUpdate()
    {
        if(m_fade == FadeType.FadeIN)
        {
            m_fadeTimer = Mathf.Clamp(m_fadeTimer + Time.deltaTime / m_fadeTime, 0,1);
            m_rawImage.color = new Color(m_rawImage.color.r, m_rawImage.color.g, m_rawImage.color.b, 1 - m_fadeTimer);
            if(m_fadeTimer < 1) { return; }
            m_fade = FadeType.Nun;
            m_fadeTimer = 0;

            if (m_sceneStart) { return; }
            PlayerManager.Instance.MoveStart();
            m_sceneStart = true;
            return;
        }
        else if(m_fade == FadeType.fadeOut)
        {
            m_fadeTimer = Mathf.Clamp(m_fadeTimer + Time.deltaTime / m_fadeTime, 0, 1);
            m_rawImage.color = new Color(m_rawImage.color.r, m_rawImage.color.g, m_rawImage.color.b,  m_fadeTimer);
            if (m_fadeTimer < 1) { return; }
            m_fade = FadeType.Nun;
            m_fadeTimer = 0;

            if (!m_sceneMove) { return; }
            
            SceneMove();
            return;
        }

        m_timer += Time.deltaTime;
        if (m_timer < m_gamePlayTime) { return; }

        MainGameEnd();
    }

    #endregion

}
