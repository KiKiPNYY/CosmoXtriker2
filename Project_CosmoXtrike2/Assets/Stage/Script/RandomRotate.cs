using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    public float adRotate = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,adRotate,0)*Time.deltaTime);
    }
}
