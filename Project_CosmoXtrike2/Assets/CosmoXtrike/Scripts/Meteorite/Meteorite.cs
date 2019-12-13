using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Meteorite : MonoBehaviour
{
    [SerializeField] private MeteoriteData m_meteoriteData = null;
    private int m_hp = 0;

    /// <summary>
    /// ダメージを与える関数
    /// </summary>
    /// <param name="_commonProcessing"></param>
    protected virtual void GiveDamege(CommonProcessing _commonProcessing)
    {
        _commonProcessing.Damege(m_meteoriteData.PlayerDamege);
        m_hp -= _commonProcessing.MeteoriteDamege();

        // エフェクトがあれば発生させる
        if (m_hp > 0) { return; }
        
        CallDestory();
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    protected void CallDestory()
    {
        m_meteoriteData = null;
        m_hp = 0;
    }

    #region Unity関数

    private void OnDestroy()
    {
        CallDestory();
    }

    void Start()
    {
        Rigidbody rb = this.transform.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 setVector = new Vector3(Random.Range(m_meteoriteData.MinRotation.x, m_meteoriteData.MaxRotation.x), Random.Range(m_meteoriteData.MinRotation.y, m_meteoriteData.MaxRotation.y), Random.Range(m_meteoriteData.MinRotation.z, m_meteoriteData.MaxRotation.z));
        m_hp = m_meteoriteData.MaxHp;
        rb.AddTorque(this.transform.forward * setVector.z + this.transform.right * setVector.x + this.transform.up * setVector.y, m_meteoriteData.ForceMode);
    }

    private void OnTriggerEnter(Collider _other)
    {
        CommonProcessing commonProcessing = null;
        commonProcessing = _other.GetComponent<CommonProcessing>();
        if (commonProcessing == null) { return; }
        
        GiveDamege(commonProcessing);
    }

    private void OnCollisionEnter(Collision _collision)
    {
        CommonProcessing commonProcessing = null;
        commonProcessing = _collision.gameObject.GetComponent<CommonProcessing>();
        if (commonProcessing == null) { return; }

        GiveDamege(commonProcessing);
    }
    #endregion
}
