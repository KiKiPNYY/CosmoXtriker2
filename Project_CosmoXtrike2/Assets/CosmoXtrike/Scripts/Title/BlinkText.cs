using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    private float _nextTime;
    private readonly float _interval = 0.6f;
    
    void Start () {
        _nextTime = Time.time;
    }

    
    void Update () {
        if (Time.time > _nextTime) {
            float alpha = gameObject.GetComponent<CanvasRenderer>().GetAlpha();
            if (alpha == 1.0f) {
                gameObject.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            } else {
                gameObject.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            }
            _nextTime += _interval;
        }
    }
}