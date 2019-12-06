using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    #region シングルトン
    private static BulletManager m_instance = null;

    public static BulletManager Instnce
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

    private List<BulletController> m_bulletControllers = new List<BulletController>();

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        //m_bulletControllers = new List<BulletController>();
    }

    /// <summary>
    /// 新しくバレットのcontrollerを追加
    /// </summary>
    /// <param name="_bullet"></param>
    public void AddBullet(Bullet _bullet)
    {
        for (int i = 0; i < m_bulletControllers.Count; i++)
        {
            if (m_bulletControllers[i].ThisHaveBullet != null) { continue; }
            if (m_bulletControllers[i].ThisHaveBullet == _bullet) { return; }
        }
        
        BulletController newBulletController = new BulletController(_bullet);
        m_bulletControllers.Add(newBulletController);
    }

    /// <summary>
    /// 通常の弾を発射
    /// </summary>
    /// <param name="_bullet"></param>
    /// <param name="_instncePos"></param>
    /// <param name="_direction"></param>
    /// <param name="_thisType"></param>
    public void Fire(Bullet _bullet, Vector3 _instncePos, Vector3 _direction, ThisType _thisType)
    {

        for (int i = 0; i < m_bulletControllers.Count; i++)
        {
            if (m_bulletControllers[i].ThisHaveBullet != _bullet) { continue; }
            m_bulletControllers[i].Fire(_instncePos, _direction, _thisType);
        }
    }

    /// <summary>
    /// ミサイル発射
    /// </summary>
    /// <param name="_bullet"></param>
    /// <param name="_instncePos"></param>
    /// <param name="_direction"></param>
    /// <param name="_thisType"></param>
    /// <param name="_target"></param>
    public void Fire(Bullet _bullet, Vector3 _instncePos, Vector3 _direction, ThisType _thisType, GameObject _target)
    {

        for (int i = 0; i < m_bulletControllers.Count; i++)
        {
            if (m_bulletControllers[i].ThisHaveBullet != _bullet) { continue; }
            m_bulletControllers[i].Fire(_instncePos, _direction, _thisType, _target);
        }
    }

    #region Unity関数
    private void Awake()
    {
        CreateInstnce();
    }

    void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < m_bulletControllers.Count; i++)
        {
            m_bulletControllers[i].ThisControllerUpdate(deltaTime);
        }
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    private void OnDestroy()
    {
        for (int i = 0; i < m_bulletControllers.Count; i++)
        {
            m_bulletControllers[i].CallDestroy();
        }
    }
    #endregion
}
