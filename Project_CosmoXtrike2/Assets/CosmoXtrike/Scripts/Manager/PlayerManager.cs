﻿using System.Collections;
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
    [SerializeField] private GameObject[] m_model = null;

    [SerializeField] private PlayerData m_normalData = null;
    [SerializeField] private PlayerData m_missileData = null;
    private PlayerData m_playerData = null;
    [SerializeField] private EnemyHpBar m_enemyHpBar = null;

    [SerializeField] private Transform m_cameraOffsetTrans = null;

    [SerializeField] private Transform thisObjectTrans = null;

    [SerializeField] private GameObject par;



    private Rigidbody m_rb = null;
    private float m_moveSpeed = 0;
    private float m_acceleTimer = 0;
    private float m_bulletIntervalTimer = 0;
    private int m_Hp = 0;
    private bool m_accele = false;
    private bool m_bulletTrigger = false;
    private bool m_moveStart = false;
    private GameObject m_bulletTarger = null;
    private PlayerLookCursor m_playerLookCursor = null;
    private bool m_search = false;
    private bool m_death = false;
    private float m_fadeTime = 0;
    private bool m_deathEffect = false;
    private bool m_bulletOn = false;

    public bool LeverOperation { get; set; }

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
        m_playerLookCursor = null;
        m_playerData = null;
        m_search = true;
        m_death = false;
        m_fadeTime = 0;
        m_deathEffect = false;
        m_bulletOn = false;
    }

    /// <summary>
    /// 移動Update
    /// </summary>
    /// <param name="_deltaTime"></param>
    private void MoveUpdate(float _deltaTime)
    {
        if (m_death) { return; }
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
        MotionBlurEffect.Instance.EffectMagnification = m_moveSpeed / m_playerData.MaxSpeed;
        m_rb.MovePosition(this.transform.position + this.transform.forward * m_moveSpeed);
    }

    /// <summary>
    /// バレットのUpdate
    /// </summary>
    /// <param name="_deltaTime"></param>
    private void BulletUpdate(float _deltaTime)
    {
        if (!LeverOperation) { return; }
        if (m_death) { return; }

        m_bulletIntervalTimer += _deltaTime;
        if (!m_bulletTrigger)
        {
            if (m_bulletIntervalTimer < m_playerData.BulletInterval) { return; }
            m_bulletOn = false;
            m_bulletIntervalTimer = 0;
            return;
        }

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
        if (m_death) { return; }
        m_Hp = Mathf.Clamp(m_Hp - _addDamege, 0, m_playerData.MaxHp);
        if (m_Hp > 0) { return; }

        //MainGameController.Instnce.MainGameEnd();
        m_death = true;
        CameraManager.Instance.PlayerDeath = true;
        m_fadeTime = 0;
    }

    /// <summary>
    /// 移動開始
    /// </summary>
    public void MoveStart()
    {
        m_moveStart = true;
    }

    public void SearchUpdate()
    {
        if (!LeverOperation) { return; }
        if (m_death) { return; }
        if (!m_search) { return; }

        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Enemy");

        bool search = false;
        ///gameObjects.transform.position = this.transform.position + this.transform.forward * m_playerData.SphereCastDistance;
        //if (Physics.SphereCast(this.transform.position, m_playerData.SphereCastRadius, this.transform.forward, out hit, m_playerData.SphereCastDistance, layerMask))
        //{
        //    if (hit.transform.gameObject.activeSelf)
        //    {
        //        m_bulletTarger = hit.transform.gameObject;
        //        if (!m_playerLookCursor.gameObject.activeSelf)
        //        {

        //            m_playerLookCursor.gameObject.SetActive(true);
        //            Enemy enemy = m_bulletTarger.GetComponent<Enemy>();
        //            AlphaTestEnemy alphaTestEnemy = m_bulletTarger.GetComponent<AlphaTestEnemy>();
        //            if (enemy != null)
        //            {
        //                m_playerLookCursor.TargetSet(m_bulletTarger, this.transform.gameObject, enemy.OffSet);
        //            }
        //            if (alphaTestEnemy != null)
        //            {
        //                m_playerLookCursor.TargetSet(m_bulletTarger, this.transform.gameObject, alphaTestEnemy.OffSet);
        //            }

        //            m_playerLookCursor.transform.parent = m_bulletTarger.transform;
        //        }
        //        search = true;
        //        //m_playerLookCursor.TargetSet(m_bulletTarger, this.transform.gameObject);
        //        // m_enemyHpBar.SetEnemy(hit.transform.GetComponent<AlphaTestEnemy>(), this.transform.gameObject);
        //    }
        //    else
        //    {
        //        m_bulletTarger = null;
        //        if (m_playerLookCursor.gameObject.activeSelf)
        //        {
        //            m_playerLookCursor.gameObject.SetActive(false);
        //        }
        //    }

        //}
        //else
        //{
        //    m_bulletTarger = null;
        //    if (m_playerLookCursor.gameObject.activeSelf)
        //    {
        //        m_playerLookCursor.gameObject.SetActive(false);
        //    }
        //}

        for (int i = 0; i < m_gunTrans.Length; i++)
        {
            if (Physics.SphereCast(m_gunTrans[i].position, m_playerData.SphereCastRadius, m_gunTrans[i].forward, out hit, m_playerData.SphereCastDistance, layerMask))
            {
                if (hit.transform.gameObject.activeSelf)
                {
                    m_bulletTarger = hit.transform.gameObject;
                    if (m_playerLookCursor != null)
                    {

                        m_playerLookCursor.gameObject.SetActive(true);
                        Enemy enemy = m_bulletTarger.GetComponent<Enemy>();
                        AlphaTestEnemy alphaTestEnemy = m_bulletTarger.GetComponent<AlphaTestEnemy>();
                        if (enemy != null)
                        {
                            m_playerLookCursor.TargetSet(m_bulletTarger, this.transform.gameObject, enemy.OffSet);
                        }
                        if (alphaTestEnemy != null)
                        {
                            m_playerLookCursor.TargetSet(m_bulletTarger, this.transform.gameObject, alphaTestEnemy.OffSet);
                        }

                        m_playerLookCursor.transform.parent = m_bulletTarger.transform;
                    }
                    search = true;
                    //m_playerLookCursor.TargetSet(m_bulletTarger, this.transform.gameObject);
                    // m_enemyHpBar.SetEnemy(hit.transform.GetComponent<AlphaTestEnemy>(), this.transform.gameObject);
                }
                else
                {
                    m_bulletTarger = null;
                    if (m_playerLookCursor.gameObject.activeSelf)
                    {
                        m_playerLookCursor.gameObject.SetActive(false);
                    }
                }

            }
            else
            {
                m_bulletTarger = null;
                if (m_playerLookCursor.gameObject.activeSelf)
                {
                    m_playerLookCursor.gameObject.SetActive(false);
                }
            }

            if (search) { break; }
        }


    }

    private void DeathUpdate(float _deltaTime)
    {
        if (!m_death) { return; }
        m_fadeTime += _deltaTime;
        if (m_fadeTime > m_playerData.ExplosionTime && !m_deathEffect)
        {
            EffectManager.Instnce.EffectPlay(m_playerData.Explosion, this.transform);
            m_deathEffect = true;
            m_fadeTime = 0;

            for (int i = 0; i < m_model.Length; i++)
            {
                m_model[i].SetActive(false);
            }

            //this.transform.
            return;
        }

        //Debug.Log(m_fadeTime + " : " + m_deathEffect);
        if (m_fadeTime > m_playerData.FadeTime && m_deathEffect)
        {
            MainGameController.Instnce.MainGameEnd();
        }

        //m_deathEffect = true;

    }

    #region Unity関数

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        if (CosmoXtrikerController.PlayerUseShip == UseShip.normal)
        {
            m_playerData = m_normalData;
            m_search = false;

        }
        else
        {
            m_playerData = m_missileData;

            m_search = true;
        }
        for (int i = 0; i < m_bulletFire.Length; i++)
        {
            BulletManager.Instnce.AddBullet(m_playerData.Bullet);
        }
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
        m_moveSpeed = m_playerData.DefaultSpeed;
        m_accele = false;
        par.SetActive(m_accele);
        m_moveStart = false;
        m_acceleTimer = 0;
        m_Hp = m_playerData.MaxHp;
        GameObject cursor = Instantiate(m_playerData.PlayerLookCursor.gameObject);
        m_playerLookCursor = cursor.GetComponent<PlayerLookCursor>();
        CameraManager.Instance.CameraOffset(m_cameraOffsetTrans);
        cursor.SetActive(false);
        LeverOperation = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damege(10000);
        }

        if (!m_moveStart) { return; }

        // 加速
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("LeftTrigger")) && !m_accele)
        {

            m_accele = true;
            par.SetActive(m_accele);
        }
        if (Input.GetKeyUp(KeyCode.A) || (Input.GetButtonUp("LeftTrigger")) && m_accele)
        {
            m_accele = false;
            par.SetActive(m_accele);
        }

        if (!LeverOperation) { return; }

        // 

        float x = Input.GetAxis("Right_Vertical") * -1;
        float y = Input.GetAxis("Right_Horizontal") * -1;


        Vector3 vector = Vector3.zero;
        Quaternion loockRotation = Quaternion.identity;
        for (int i = 0; i < m_gunTrans.Length; i++)
        {

            if (Mathf.Abs(x) <= 0 && Mathf.Abs(y) <= 0) { break; }

            if (Mathf.Abs(x) > 0 && Mathf.Abs(y) <= 0)
            {
                loockRotation = Quaternion.Euler(10 * x, m_gunTrans[i].localEulerAngles.y, m_gunTrans[i].localEulerAngles.z);
            }
            else if (Mathf.Abs(x) <= 0 && Mathf.Abs(y) > 0)
            {
                loockRotation = Quaternion.Euler(m_gunTrans[i].localEulerAngles.x, 30 * y, m_gunTrans[i].localEulerAngles.z);
            }
            else
            {
                loockRotation = Quaternion.Euler(10 * x, 30 * y, m_gunTrans[i].localEulerAngles.z);
            }


            m_gunTrans[i].localRotation = Quaternion.Slerp(m_gunTrans[i].localRotation, loockRotation, Time.deltaTime);

        }

        loockRotation = Quaternion.identity;
        x = 0;
        y = 0;

        x = Input.GetAxis("Left_Horizontal") * -1;
        y = Input.GetAxis("Left_Vertical");

        //vector = this.transform.right * x * -1 + this.transform.up * y + this.transform.forward;
        //loockRotation = Quaternion.LookRotation((vector).normalized);
        //this.transform.RotateAround(this.transform.position, this.transform.right * -y +this.transform.up * x, 90 * Time.deltaTime);
        //this.transform.Rotate(new Vector3(90 * -y, 90 * x, 0) * Time.deltaTime,Space.World);

        float rotationX = Mathf.Clamp(this.transform.localEulerAngles.x < 0 ? 360 - this.transform.localEulerAngles.x : this.transform.localEulerAngles.x, 0, 360);
        float rotationY = Mathf.Clamp(this.transform.localEulerAngles.y < 0 ? 360 - this.transform.localEulerAngles.y : this.transform.localEulerAngles.y, 0, 360);
        float rotationZ = Mathf.Clamp(this.transform.localEulerAngles.z > 180 ? this.transform.localEulerAngles.z - 360 : this.transform.localEulerAngles.z, -180, 180);


        rotationX += 90 * -y * Time.deltaTime;
        rotationY += 90 * x * Time.deltaTime;
        rotationZ = 45 * -x * Time.deltaTime;//Mathf.Clamp(rotationZ + 45 * -x * Time.deltaTime, -45,45);


        this.transform.rotation = Quaternion.Euler(rotationX, rotationY, this.transform.localEulerAngles.z);
        loockRotation *= Quaternion.Euler(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, 45 * -x);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, loockRotation, Time.deltaTime * 10);
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, loockRotation, Time.deltaTime);
        //this.transform.rotation = Quaternion.Euler(this.transform.localEulerAngles.x + 90 * -y * Time.deltaTime, this.transform.localEulerAngles.y + 90 * x * Time.deltaTime, 45 * -x * Time.deltaTime);

        //m_bulletIntervalTimer += Time.deltaTime;
        if (Input.GetButtonUp("RightTrigger") || Input.GetKeyUp(KeyCode.Space))
        {
            m_bulletTrigger = false;
        }
        if (m_bulletOn) { return; }

        if (Input.GetButtonDown("RightTrigger") || Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < m_bulletFire.Length; i++)
            {
                EffectManager.Instnce.EffectPlay(m_playerData.Effect, m_bulletFire[i]);
                BulletManager.Instnce.Fire(m_playerData.Bullet, m_bulletFire[i].position + m_bulletFire[i].forward, m_bulletFire[i].forward, ThisType.Player, m_bulletTarger);
            }
            SoundManager.Instnce.SEPlay("BeamBrun", m_bulletFire[0]);
            m_bulletTrigger = true;
            m_bulletOn = true;
        }

        
    }

    private void FixedUpdate()
    {
        if (!m_moveStart) { return; }
        m_rb.velocity = Vector3.zero;
        float deltatime = Time.deltaTime;
        SearchUpdate();
        MoveUpdate(deltatime);
        BulletUpdate(deltatime);
        DeathUpdate(deltatime);
    }

    #endregion

}
