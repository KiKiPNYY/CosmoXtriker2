using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData m_bulletData = null;
    [SerializeField] private Effect m_effect = null;
    private ParticleSystem m_particleSystem = null;
    private Rigidbody m_rb = null;
    protected Vector3 m_instanceOrigin = Vector3.zero;
    protected ThisType m_targetType = ThisType.Enemy;
    protected float m_activeTimer = 0;

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
            m_rb.useGravity = false;
        }

        BoxCollider collider = this.transform.gameObject.GetComponent<BoxCollider>();

        if (!collider.isTrigger)
        {
            collider.isTrigger = true;
            collider.isTrigger = true;
        }

        if (m_particleSystem == null)
        {
            m_particleSystem = this.transform.gameObject.GetComponent<ParticleSystem>();
        }

        m_activeTimer = 0;

        this.transform.gameObject.SetActive(true);
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public virtual void Hidden()
    {
        this.transform.gameObject.SetActive(false);
    }

    /// <summary>
    /// 弾発射
    /// </summary>
    /// <param name="_instncePos"></param>
    /// <param name="_direction"></param>
    /// <param name="_thisType"></param>
    /// <param name="_target"></param>
    public virtual void Fire(Vector3 _instncePos,Vector3 _direction, ThisType _thisType, GameObject _target = null)
    {
        Quaternion rotation = Quaternion.LookRotation(_direction);
        this.transform.rotation = rotation;
        this.transform.position = _instncePos;
        m_instanceOrigin = this.transform.position;
        m_targetType = _thisType;

        if(m_particleSystem != null)
        {
            m_particleSystem.Play();
        }
    }

    /// <summary>
    /// Update処理のまとめ
    /// </summary>
    /// <param name="_deltaTime"></param>
    public virtual void ThisObjectUpdate(float _deltaTime)
    {
        Move(_deltaTime);
        ThisObjectShowCheck(_deltaTime);
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="_deltaTime"></param>
    protected virtual void Move(float _deltaTime)
    {
        // m_rb.MovePosition(this.transform.position + this.transform.forward * _deltaTime * m_bulletData.BulletSpeed);
        m_rb.velocity = ((this.transform.position + this.transform.forward) - this.transform.position) * _deltaTime * m_bulletData.BulletSpeed;
    }

    /// <summary>
    /// 表示できるかのチェック
    /// </summary>
    public virtual void ThisObjectShowCheck(float _deltaTime)
    {
        m_activeTimer += _deltaTime;
        if(m_activeTimer < m_bulletData.ActiveTime) { return; }
        Hidden();
    }

    /// <summary>
    /// ダメージを与える関数
    /// </summary>
    /// <param name="_commonProcessing"></param>
    protected virtual void GiveDamege(CommonProcessing _commonProcessing)
    {
        _commonProcessing.Damege(m_bulletData.Damege);
        
        // エフェクトがあれば発生させる
        if (m_effect != null)
        {
            EffectManager.Instnce.EffectPlay(m_effect, this.transform);

        }
        Hidden();
    }

    /// <summary>
    /// 破棄処理
    /// </summary>
    public virtual void CallDestroy()
    {
        m_particleSystem = null;
        m_bulletData = null;
        m_instanceOrigin = Vector3.zero;
        m_rb = null;
        m_targetType = ThisType.Enemy;
    }

    #region Unity関数

    private void OnTriggerEnter(Collider _other)
    {
        CommonProcessing commonProcessing = null;
        commonProcessing = _other.GetComponent<CommonProcessing>();
        if (commonProcessing == null) { return; }
        if (commonProcessing.ReturnMyType() == m_targetType) { return; }
        GiveDamege(commonProcessing);
    }
    #endregion
}
