using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    #region シングルトン
    private static EnemyHpBar m_instance = null;

    public static EnemyHpBar Instance
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

    private void Awake()
    {
        CreateInstance();
    }

    #endregion

    private AlphaTestEnemy m_alphaTestEnemy = null;
    private GameObject m_player = null;
    [SerializeField]private Slider m_slider = null;
    [SerializeField] private Vector3 m_offset = Vector3.zero;

    public EnemyHpBar()
    {
        m_alphaTestEnemy = null;
        m_player = null;
    }

    void Start()
    {
        m_slider.value = 1;
    }

    public void SetEnemy(AlphaTestEnemy _alphaTestEnemy, GameObject _player)
    {
        if(_alphaTestEnemy == null || _player == null) { return; }

        m_alphaTestEnemy = _alphaTestEnemy;
        float nowHpVal = (float)m_alphaTestEnemy.NowHP / (float)m_alphaTestEnemy.MaxHP;
        
        Debug.Log(nowHpVal);
        m_slider.value = nowHpVal;
        m_player = _player;

        this.transform.position = m_alphaTestEnemy.transform.position + m_offset;
        this.transform.LookAt(m_player.transform);

    }
    
    void Update()
    {
        if(m_alphaTestEnemy == null) { return; }
        float nowHpVal = m_alphaTestEnemy.NowHP / m_alphaTestEnemy.MaxHP;
        m_slider.value = nowHpVal;

        this.transform.position = m_alphaTestEnemy.transform.position + m_offset;
        this.transform.LookAt(m_player.transform);
    }
}
