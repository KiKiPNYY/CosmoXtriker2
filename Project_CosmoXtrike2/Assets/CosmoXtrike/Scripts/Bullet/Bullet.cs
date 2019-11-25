using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] BulletData m_bulletData = null;
    private Rigidbody m_rb = null;
    private Vector3 m_instanceOrigin = Vector3.zero;

    public GameObject ThisGameObject { get => this.transform.gameObject; }

    public bool ThisObjectActive { get => this.transform.gameObject.activeSelf; }

    public BulletData BulletData { get => m_bulletData; }

    /// <summary>
    /// 生成時の処理
    /// </summary>
    public void Generate(BulletData _bulletData)
    {
        m_bulletData = _bulletData;
        this.transform.gameObject.SetActive(false);  
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public virtual void Init()
    {
        if (m_rb == null)
        {
            m_rb = this.transform.gameObject.GetComponent<Rigidbody>();
        }

        BoxCollider collider = this.transform.gameObject.GetComponent<BoxCollider>();

        if (!collider.isTrigger)
        {
            collider.isTrigger = true;
            collider.isTrigger = true;
        }
        this.transform.gameObject.SetActive(true);
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public virtual void Final()
    {

        this.transform.gameObject.SetActive(false);
    }

    public virtual void Fire(Vector3 _instncePos,Vector3 _direction)
    {
        this.transform.position = _instncePos;
        m_instanceOrigin = this.transform.position;
        Quaternion rotation = Quaternion.LookRotation(_direction);
        this.transform.rotation = rotation;
    }

    /// <summary>
    /// Update処理のまとめ
    /// </summary>
    /// <param name="_deltaTime"></param>
    public virtual void ThisObjectUpdate(float _deltaTime)
    {
        Move(_deltaTime);
        ThisObjectShowCheck();
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="_deltaTime"></param>
    public virtual void Move(float _deltaTime)
    {
        m_rb.MovePosition(this.transform.position + this.transform.forward * _deltaTime * m_bulletData.BulletSpeed);
    }

    /// <summary>
    /// 表示できるかのチェック
    /// </summary>
    public virtual void ThisObjectShowCheck()
    {
        float nowDistance = Vector3.Distance(this.transform.position, m_instanceOrigin);
        if(nowDistance < m_bulletData.InstanceDistance) { return; }
        Final();
    }

    /// <summary>
    /// ダメージを与える関数
    /// </summary>
    /// <param name="_commonProcessing"></param>
    protected virtual void GiveDamege(CommonProcessing _commonProcessing)
    {
        _commonProcessing.Damege(m_bulletData.Damege);

        // エフェクトがあれば発生させる

        Final();
    }

    private void OnTriggerEnter(Collider _other)
    {
        CommonProcessing commonProcessing = null;
        commonProcessing = _other.GetComponent<CommonProcessing>();
        if (commonProcessing == null) { return; }
        GiveDamege(commonProcessing);
    }

}
