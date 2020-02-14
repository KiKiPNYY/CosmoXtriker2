
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 100;
    public GameObject impactEffect;

    [SerializeField] private Effect m_effect = null;

    private void Start()
    {
        Destroy(gameObject, 2);
    }
    public void Seek(Transform _target)
    {
        target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //if(dir.magnitude <= distanceThisFrame)
        //{
        //    HitTarget();
        //    return;
        //}
        //transform.Translate(dir.normalized * distanceThisFrame,Space.World);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward.normalized * speed,ForceMode.Force);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Ally"))
        {
            HitTarget();
            return;
        }
        else if (col.CompareTag("Player"))
        {
            HitTarget();
            return;
        }
    }

    void HitTarget()
    {
        EffectManager.Instnce.EffectPlay(m_effect, this.transform);
        // GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);

        Destroy(gameObject,3);
    }
}
