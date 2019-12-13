using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2 : MonoBehaviour
{
    public float adRotate = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,adRotate)*Time.deltaTime);
    }
}
