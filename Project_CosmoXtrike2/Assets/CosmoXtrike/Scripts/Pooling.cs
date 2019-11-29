using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling
{
    private GameObject m_gameObject = null;
    private List<GameObject> m_poolLists = null;


    public Pooling (GameObject _gameObject)
    {
        if (m_gameObject != null) { return; }
        m_gameObject = _gameObject;
        m_poolLists = new List<GameObject>();
        m_poolLists.Add(CreateObject());
    }

    /// <summary>
    /// Listの中で使用していないGameObjectを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        for (int i = 0; i < m_poolLists.Count; i++)
        {
            if (m_poolLists[i].activeSelf) { continue; }
            return m_poolLists[i];
        }

        m_poolLists.Add(CreateObject());
        return m_poolLists[m_poolLists.Count - 1];
    }

    /// <summary>
    /// 新しくプーリングするオブジェクト作成
    /// </summary>
    /// <returns></returns>
    private GameObject CreateObject()
    {
        GameObject newObject = Object.Instantiate(m_gameObject);
        newObject.name = m_gameObject.name + m_poolLists.Count + 1;
        return newObject;
    }
}
