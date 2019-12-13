using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMove : MonoBehaviour
{
    public float speed = 1;

    void FixedUpdate()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Force);
    }
}