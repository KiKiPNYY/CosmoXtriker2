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

    public void TargetSet(GameObject _enemy, GameObject _player, Vector3 _offset)
    {
        if(_enemy == null || _player == null) { return; }
        m_player = _player;
        m_enemy = _enemy;
        Vector3 setPosition = Vector3.zero;//m_enemy.transform.right * _offset.x + m_enemy.transform.up * _offset.y + m_enemy.transform.forward * _offset.z;
        this.transform.position = m_enemy.transform.position + setPosition;
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
