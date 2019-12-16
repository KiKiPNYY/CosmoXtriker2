using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    #region シングルトン
    private static SceneLoadManager m_instance = null;

    public static SceneLoadManager Instnce
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

    private bool m_loadNow = false;

    /// <summary>
    /// 指定したシーンをロード
    /// </summary>
    /// <param name="_sceneName"></param>
    public void LoadScene(string _sceneName)
    {
        if (m_loadNow) { return; }
        m_loadNow = true;
        StartCoroutine(LoadNextScene(_sceneName));
    }

    private IEnumerator LoadNextScene(string _sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(_sceneName);       
        while (!async.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1);

        m_loadNow = false;
    }

    #region Unity関数
    private void Awake()
    {
        CreateInstnce();
    }

    private void Start()
    {
        m_loadNow = false;
    }

    #endregion
}
