using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMove : MonoBehaviour
{
    public float speed = 1;
    private Rigidbody rb = null;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        if(rb == null)
        {
            rb = this.transform.gameObject.AddComponent<Rigidbody>(); 
        }

        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if(rb == null)
        {
            rb = this.GetComponent<Rigidbody>();
        }
        rb.AddForce(transform.forward * speed, ForceMode.Force);
    }
}