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
    
  private Transform m_cameraOffset = null;
  public CameraManager()
  {
    m_cameraOffset = null;
  }

  private void Awake() {
    CreateInstance();
  }
  public void CameraOffset(Transform _offsetTrans)
  {
    if(m_cameraOffset != null){return;}
    if(_offsetTrans == null){return;}
    m_cameraOffset = _offsetTrans;
  }

    void Update()
    {
      if(m_cameraOffset == null){return;}
        this.transform.position = m_cameraOffset.position;
        this.transform.rotation = m_cameraOffset.transform.rotation;
    }
}
