using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private static BulletManager m_instance = null;

    private List<BulletController> m_bulletControllers = new List<BulletController>();

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

    private void Init()
    {
        //m_bulletControllers = new List<BulletController>();
    }

    public void AddBullet(Bullet _bullet)
    {
        for(int i = 0; i < m_bulletControllers.Count; i++)
        {
            if(m_bulletControllers[i].ThisHaveBullet != null) { continue; }
            if(m_bulletControllers[i].ThisHaveBullet == _bullet) { return; }
        }

        BulletController newBulletController = new BulletController();
        newBulletController.Init(_bullet);
        m_bulletControllers.Add(newBulletController);
    }

    public void Fire(Bullet _bullet, Vector3 _instncePos, Vector3 _direction)
    {
        
        for (int i = 0; i < m_bulletControllers.Count; i++)
        {
            if (m_bulletControllers[i].ThisHaveBullet != _bullet) { continue; }
            m_bulletControllers[i].Fire(_instncePos, _direction);
        }
    }

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
        for(int i = 0; i < m_bulletControllers.Count;i++)
        {
            m_bulletControllers[i].ThisControllerUpdate(deltaTime);
        }
    }
}
