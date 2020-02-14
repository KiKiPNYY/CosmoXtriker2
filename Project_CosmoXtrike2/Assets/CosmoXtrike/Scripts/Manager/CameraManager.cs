using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region シングルトン
    private static CameraManager m_instance = null;

    public static CameraManager Instance
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

    [SerializeField] private Vector3 m_playerDeathDirection = Vector3.zero;

    [SerializeField] private float m_moveSpeed = 0;

    private Transform m_cameraOffset = null;

    public bool PlayerDeath { get; set; }

    [SerializeField] private GameOver m_gameOver = null;

    private void Start()
    {
        PlayerDeath = false;
    }

    public CameraManager()
    {
        m_cameraOffset = null;
    }

    private void Awake()
    {
        CreateInstance();
    }
    public void CameraOffset(Transform _offsetTrans)
    {
        if (m_cameraOffset != null) { return; }
        if (_offsetTrans == null) { return; }
        m_cameraOffset = _offsetTrans;
    }

    void Update()
    {
        if (m_cameraOffset == null) { return; }

        if(PlayerDeath)
        {
            m_gameOver.TextDisplay();
            this.transform.position += m_playerDeathDirection.normalized * m_moveSpeed * Time.deltaTime;
            return;
        }

        this.transform.position = m_cameraOffset.position;
        this.transform.rotation = m_cameraOffset.transform.rotation;
    }
}
