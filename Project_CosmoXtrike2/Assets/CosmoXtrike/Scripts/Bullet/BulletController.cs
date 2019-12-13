using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController
{
    private Bullet m_bulletOrigin = null;
    private List<Bullet> m_bullets = null;

    /// <summary>
    /// このコントローラーの所持してるBulletを返す
    /// </summary>
    public Bullet ThisHaveBullet { get => m_bulletOrigin; }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="_bullet"></param>
    public BulletController(Bullet _bullet)
    {
        m_bulletOrigin = _bullet;
        m_bullets = new List<Bullet>();
        m_bullets.Add(CreateBullet());
    }

    /// <summary>
    /// 発射関数(ミサイル)
    /// </summary>
    /// <param name="_instncePos"></param>
    /// <param name="_direction"></param>
    public virtual void Fire(Vector3 _instncePos, Vector3 _direction, ThisType _thisType, GameObject _target)
    {
        Bullet bullet = GetBullet();
        bullet.Init();
        bullet.Fire(_instncePos, _direction, _thisType, _target);
    }

    /// <summary>
    /// 発射関数
    /// </summary>
    /// <param name="_instncePos"></param>
    /// <param name="_direction"></param>
    public virtual void Fire(Vector3 _instncePos ,Vector3 _direction, ThisType _thisType)
    {
        Bullet bullet = GetBullet();
        bullet.Init();
        bullet.Fire(_instncePos, _direction, _thisType);
    }

    /// <summary>
    /// Listの中で使用していないBulletを取得
    /// </summary>
    /// <returns></returns>
    private Bullet GetBullet()
    {
        for(int i = 0; i < m_bullets.Count; i++)
        {
            if (m_bullets[i].ThisObjectActive) { continue; }
            return m_bullets[i];
        }

        m_bullets.Add(CreateBullet());
        return m_bullets[m_bullets.Count - 1];
    }

    /// <summary>
    /// Listの中のBulletがすべて使用されていた場合生成する関数
    /// </summary>
    /// <returns></returns>
    private Bullet CreateBullet()
    {
        Bullet newBullet = null;
        GameObject bulletObj = Object.Instantiate(m_bulletOrigin.ThisGameObject);
        bulletObj.name = m_bulletOrigin.name + m_bullets.Count + 1;
        newBullet = bulletObj.GetComponent<Bullet>();
        newBullet.Generate(m_bulletOrigin.BulletData);
        return newBullet;
    }

    /// <summary>
    /// Updateのまとめ
    /// </summary>
    /// <param name="_deltaTime"></param>
    public void ThisControllerUpdate(float _deltaTime)
    {
        for(int i = 0; i < m_bullets.Count; i++)
        {
            if (!m_bullets[i].ThisObjectActive) { continue; }
            m_bullets[i].ThisObjectUpdate(_deltaTime);
        }
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    public void CallDestroy()
    {
        for (int i = 0; i < m_bullets.Count; i++)
        {
            m_bullets[i].CallDestroy();
        }

        m_bulletOrigin = null;
        m_bullets = null;
    }

}
