using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    #region シングルトン
    private static MeteoriteManager m_instance = null;

    public static MeteoriteManager Instnce
    {
        get
        {
            if (m_instance == null)
            {
                Debug.LogError("MeteoriteManagerがありません");
            }
            return m_instance;
        }
    }

    /// <summary>
    /// シングルトン作成
    /// </summary>
    private void CreateInstnce()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }
    #endregion

    [SerializeField] private MeteoriteSetPositionData[] m_meteoriteSetPositionData = null;

    void Start()
    {
        Meteorite meteorite = null;
        Meteorite[] meteorites = null;
        int meteoriteNum = 0;
        for (int i = 0; i < m_meteoriteSetPositionData.Length; i++)
        {
            meteorites = m_meteoriteSetPositionData[i].Meteorites;
            meteoriteNum = Random.Range(0, meteorites.Length);
            meteorite = Instantiate(meteorites[meteoriteNum]);
        }
    }

    private void Awake()
    {
        CreateInstnce();
    }

}
