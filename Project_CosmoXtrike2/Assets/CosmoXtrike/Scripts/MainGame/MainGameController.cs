using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private SoundData m_soundData = null;

    private float m_timer = 0;
    private int m_destroyNum = 0;

    /// <summary>
    /// 初期化
    /// </summary>
    public MainGameController()
    {
        m_timer = 0;
        m_enemyDestroy = 0;
    }

    /// <summary>
    /// エネミーを破壊したときに撃破数の加算
    /// </summary>
    public void EnemyDestroyAdd()
    {
        m_destroyNum++;
        if(m_destroyNum < m_enemyDestroy) { return; }
        GameEnd();
    }

    /// <summary>
    /// メインゲームが終わったときに呼びゲームを遷移させる
    /// </summary>
    public void GameEnd()
    {
        SoundManager.Instnce.BGMFade(1,FadeType.fadeOut);
        SoundManager.Instnce.SEFade(FadeType.fadeOut,1,true);
        SceneLoadManager.Instnce.LoadScene("Title");
    }

    #region Unity関数
    private void Start()
    {
        SoundManager.Instnce.SceneStart(m_soundData);
    }

    private void FixedUpdate()
    {
        m_timer += Time.deltaTime;
        if (m_timer < m_gamePlayTime) { return; }
        GameEnd();
    }

    #endregion

}
