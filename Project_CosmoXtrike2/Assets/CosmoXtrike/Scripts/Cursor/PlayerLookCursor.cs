using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookCursor : MonoBehaviour
{
    private GameObject m_player = null;
    private GameObject m_enemy = null;

    [SerializeField] private Vector3 m_offset = Vector3.zero;

    /// <summary>
    /// 初期化
    /// </summary>
    public PlayerLookCursor()
    {
        m_player = null;
    }

    public void TargetSet(GameObject _enemy, GameObject _player)
    {
        if(_enemy == null || _player == null) { return; }
        m_player = _player;
        m_enemy = _enemy;

        this.transform.position = m_enemy.transform.position;
        //this.transform.localPosition = Vector3.zero;
        this.transform.LookAt(m_player.transform);
        
        //
    }

    void Update()
    {
        if (m_player == null) { return; }
        this.transform.LookAt(m_player.transform);
    }
}
