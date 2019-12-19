using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour, CommonProcessing
{

    #region シングルトン
    private static PlayerManager m_instance = null;

    public static PlayerManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    private void CreateInstance()
    {
        if (m_instance == null)
        {
            m_instance = this;
            if (m_instance == null)
            {
                Debug.LogError("PlayerManagerがありません");
            }

        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }

    #endregion

    [SerializeField] private Transform[] m_bulletFire;
    [SerializeField] private Transform[] m_gunTrans;
    [SerializeField] private PlayerData m_playerData;

    private Rigidbody m_rb = null;
    private float m_moveSpeed = 0;
    private float m_acceleTimer = 0;
    private float m_bulletIntervalTimer = 0;
    private int m_Hp = 0;
    private bool m_accele = false;
    private bool m_bulletTrigger = false;
    private bool m_moveStart = false;
    //private Vector3 m_bulletTargerPos = Vector3.zero;
    private GameObject m_bulletTarger = null;

    /// <summary>
    /// 初期化
    /// </summary>
    public PlayerManager()
    {
        m_moveSpeed = 0;
        m_acceleTimer = 0;
        m_bulletIntervalTimer = 0;
        m_Hp = 0;
        m_accele = false;
        m_bulletTrigger = false;
        m_moveStart = false;
        m_bulletTarger = null;
        m_rb = null;
    }

    /// <summary>
    /// 移動Update
    /// </summary>
    /// <param name="_deltaTime"></param>
    private void MoveUpdate(float _deltaTime)
    {
        if (m_accele)
        {
            m_acceleTimer += _deltaTime / m_playerData.AcceleTime;
            m_acceleTimer = Mathf.Clamp(m_acceleTimer, 0, 1);
            float addAccele = m_playerData.MaxSpeed - m_playerData.DefaultSpeed;
            m_moveSpeed = m_playerData.DefaultSpeed + (addAccele * m_acceleTimer);
        }
        if (!m_accele)
        {
            m_acceleTimer -= _deltaTime / m_playerData.AcceleTime;
            m_acceleTimer = Mathf.Clamp(m_acceleTimer, 0, 1);
            float addAccele = m_playerData.MaxSpeed - m_playerData.DefaultSpeed;
            m_moveSpeed = m_playerData.DefaultSpeed + (addAccele * m_acceleTimer);
        }

        m_moveSpeed = Mathf.Clamp(m_moveSpeed, m_playerData.DefaultSpeed, m_playerData.MaxSpeed);
        m_rb.MovePosition(this.transform.position + this.transform.forward * m_moveSpeed);
    }

    /// <summary>
    /// バレットのUpdate
    /// </summary>
    /// <param name="_deltaTime"></param>
    private void BulletUpdate(float _deltaTime)
    {
        if (!m_bulletTrigger) { return; }
        m_bulletIntervalTimer += _deltaTime;
        if (m_bulletIntervalTimer < m_playerData.BulletInterval) { return; }
        for (int i = 0; i < m_bulletFire.Length; i++)
        {
            EffectManager.Instnce.EffectPlay(m_playerData.Effect, m_bulletFire[i]);
            Vector3 targetPos = m_bulletFire[i].position + m_bulletFire[i].forward * 1.1f; //m_bulletTarger == null ? m_bulletFire[i].position + m_bulletFire[i].forward * 1.1f : m_bulletTarger.transform.position;
            Vector3 direction = (targetPos - m_bulletFire[i].position + m_bulletFire[i].forward).normalized;
            BulletManager.Instnce.Fire(m_playerData.Bullet, m_bulletFire[i].position + m_bulletFire[i].forward, direction, ThisType.Player, m_bulletTarger);
        }
        SoundManager.Instnce.SEPlay("BeamBrun", m_bulletFire[0]);
        m_bulletIntervalTimer = 0;
    }

    public ThisType ReturnMyType()
    {
        return ThisType.Player;
    }

    public int MeteoriteDamege()
    {
        return m_playerData.MeteoriteDamege;
    }

    /// <summary>
    /// ダメージを受ける関数
    /// </summary>
    /// <param name="_addDamege"></param>
    public void Damege(int _addDamege)
    {
        m_Hp = Mathf.Clamp(m_Hp - _addDamege, 0, m_playerData.MaxHp);
        if (m_Hp > 0) { return; }
        MainGameController.Instnce.MainGameEnd();
    }

    /// <summary>
    /// 移動開始
    /// </summary>
    public void MoveStart()
    {
        m_moveStart = true;
    }

    #region Unity関数

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        for (int i = 0; i < m_bulletFire.Length; i++)
        {
            BulletManager.Instnce.AddBullet(m_playerData.Bullet);
        }
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
        m_moveSpeed = m_playerData.DefaultSpeed;
        m_accele = false;
        m_moveStart = false;
        m_acceleTimer = 0;
        m_Hp = m_playerData.MaxHp;
    }

    private void Update()
    {
        if (!m_moveStart) { return; }
        float x = Input.GetAxis("Right_Horizontal");
        float y = Input.GetAxis("Right_Vertical");

        Vector3 vector = Vector3.zero;
        Quaternion loockRotation;
        // for (int i = 0; i < m_gunTrans.Length; i++)
        // {

        //    if ((m_gunTrans[i].localEulerAngles.y > 30 && y > 0) || (m_gunTrans[i].localEulerAngles.y < -30 && y < 0))
        //    {
        //        m_gunTrans[i].rotation = Quaternion.Euler(m_gunTrans[i].rotation.x, m_gunTrans[i].rotation.y > 0 ? 30 : -30, m_gunTrans[i].rotation.z);
        //    }
        //   if ((m_gunTrans[i].localEulerAngles.x > 10 && x > 0) || (m_gunTrans[i].localEulerAngles.x < -10 && x < 0))
        //   {
        //       m_gunTrans[i].rotation = Quaternion.Euler(m_gunTrans[i].rotation.x > 0 ? 10 : -10, m_gunTrans[i].rotation.y, m_gunTrans[i].rotation.z);
        //   }

        //   vector = m_gunTrans[i].right * x + m_gunTrans[i].up * y + m_gunTrans[i].forward;
        //   loockRotation = Quaternion.LookRotation((m_gunTrans[i].position + vector) - m_gunTrans[i].position);
        //   m_gunTrans[i].rotation = Quaternion.Slerp(m_gunTrans[i].rotation, loockRotation, Time.deltaTime);

        //}

        x = Input.GetAxis("Left_Horizontal");
        y = Input.GetAxis("Left_Vertical");

        if (Input.GetButtonDown("LeftTrigger") && !m_accele)
        {
            m_accele = true;
        }
        if (Input.GetButtonUp("LeftTrigger") && m_accele)
        {
            m_accele = false;
        }

        vector = Vector3.zero;

        vector = this.transform.right * x * -1 + this.transform.up * y + this.transform.forward;
        loockRotation = Quaternion.LookRotation((this.transform.position + vector) - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, loockRotation, Time.deltaTime);

        if (Input.GetButtonDown("RightTrigger") || Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < m_bulletFire.Length; i++)
            {
                EffectManager.Instnce.EffectPlay(m_playerData.Effect, m_bulletFire[i]);
                BulletManager.Instnce.Fire(m_playerData.Bullet, m_bulletFire[i].position + m_bulletFire[i].forward, m_bulletFire[i].forward, ThisType.Player, m_bulletTarger);
            }
            SoundManager.Instnce.SEPlay("BeamBrun", m_bulletFire[0]);
            m_bulletTrigger = true;
        }

        if (Input.GetButtonUp("RightTrigger") || Input.GetKeyUp(KeyCode.Space))
        {
            m_bulletIntervalTimer = 0;
            m_bulletTrigger = false;
        }
    }

    private void FixedUpdate()
    {
        if (!m_moveStart) { return; }
        m_rb.velocity = Vector3.zero;

        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        if (Physics.SphereCast(this.transform.position, m_playerData.RayCastRadius, transform.forward, out hit, m_playerData.RayCastDistance, layerMask))
        {
            if (hit.transform.gameObject.activeSelf)
            {
                m_bulletTarger = hit.transform.gameObject;
            }
            else
            {
                m_bulletTarger = null;
            }
            
        }
        else
        {
            m_bulletTarger = null;
        }

        float deltatime = Time.deltaTime;
        MoveUpdate(deltatime);
        BulletUpdate(deltatime);
    }

    #endregion

}
