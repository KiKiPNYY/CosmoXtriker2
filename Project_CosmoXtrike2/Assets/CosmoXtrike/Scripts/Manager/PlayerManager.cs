using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    [SerializeField] private Transform[] m_bulletFire;

    [SerializeField] private Transform[] m_gunTrans;
    [SerializeField] private Bullet m_bullet;
    [SerializeField] private Effect m_fireEffect;

    [SerializeField] private float m_acceleTime = 0;
    [SerializeField] private float m_defaultSpeed = 0;
    [SerializeField] private float m_maxSpeed = 0;
    private float speed = 0;
    private float timer = 0;
    private bool m_accele = false;

    void Start()
    {
        for(int i =0 ; i < m_bulletFire.Length; i++)
        {
            BulletManager.Instnce.AddBullet(m_bullet);
        }
        speed = m_defaultSpeed;
        m_accele = false;
        timer = 0;
    }

    
    void Update()
    {
        float x = Input.GetAxis("Left_Horizontal");
        float y = Input.GetAxis("Left_Vertical");
        
        Vector3 vector = Vector3.zero;
        Quaternion loockRotation;
        bool ishoge = false;
        // for(int i =0 ; i < m_gunTrans.Length; i++)
        // {
            
        //     if((m_gunTrans[i].localEulerAngles.y > 30 && y > 0) || (m_gunTrans[i].localEulerAngles.y < -30 && y < 0))
        //     {
        //         m_gunTrans[i].rotation = Quaternion.Euler(m_gunTrans[i].rotation.x, m_gunTrans[i].rotation.y > 0 ? 30:-30,m_gunTrans[i].rotation.z);
        //         ishoge = true;
        //     }
        //     if((m_gunTrans[i].localEulerAngles.x > 10 && x > 0) || (m_gunTrans[i].localEulerAngles.x < -10 && x < 0))
        //     {
        //         m_gunTrans[i].rotation = Quaternion.Euler(m_gunTrans[i].rotation.x > 0 ? 10:-10, m_gunTrans[i].rotation.y,m_gunTrans[i].rotation.z);
        //     }

        //     vector = m_gunTrans[i].right * x + m_gunTrans[i].up * y + m_gunTrans[i].forward;
        //     loockRotation = Quaternion.LookRotation((m_gunTrans[i].position + vector) - m_gunTrans[i].position);
        //     m_gunTrans[i].rotation = Quaternion.Slerp(m_gunTrans[i].rotation, loockRotation, Time.deltaTime);

        // }


        // x = Input.GetAxis("Right_Horizontal");
        // y = Input.GetAxis("Right_Vertical");
        if(Input.GetButtonDown("accele") && !m_accele)
        {
            m_accele = true;
        }
        if(Input.GetButtonUp("accele") && m_accele)
        {
            Debug.Log(timer);
            m_accele = false;
        }

        if(m_accele )
        {
            timer += Time.deltaTime / m_acceleTime;
            timer = Mathf.Clamp(timer,0,1);
            float addAccele = m_maxSpeed - m_defaultSpeed;
            speed = m_defaultSpeed + (addAccele * timer);
        }
        if(!m_accele )
        {
            timer -= Time.deltaTime / m_acceleTime;
            timer = Mathf.Clamp(timer,0,1);
        //     Debug.Log(timer);
            float addAccele = m_maxSpeed - m_defaultSpeed;
            speed = m_defaultSpeed + (addAccele * timer);
        }
        speed = Mathf.Clamp(speed, m_defaultSpeed, m_maxSpeed);
        Debug.Log(speed);

        vector = m_player.transform.right * x *-1 + m_player.transform.up * y + m_player.transform.forward;
        loockRotation = Quaternion.LookRotation((m_player.transform.position + vector) - m_player.transform.position);
        m_player.transform.rotation = Quaternion.Slerp(m_player.transform.rotation, loockRotation, Time.deltaTime);
        m_player.transform.position += m_player.transform.forward * speed;

       
        if(Input.GetButtonDown("BulletFire"))
        {
            
            for(int i =0 ; i < m_bulletFire.Length; i++)
            {
                EffectManager.Instnce.EffectPlay(m_fireEffect,m_bulletFire[i]);
                BulletManager.Instnce.Fire(m_bullet, m_bulletFire[i].position + m_bulletFire[i].forward, m_bulletFire[i].forward,ThisType.Player);
            }
        }

        // if(Input.anyKey)
        // {
        //     for(int i =0 ; i < m_bulletFire.Length; i++)
        //     {
        //         Debug.Log("hoge");
        //         return;
        //         BulletManager.Instnce.Fire(m_bullet, m_bulletFire[i].position, m_bulletFire[i].forward,ThisType.Player);
        //     }
        // }
    }
}
