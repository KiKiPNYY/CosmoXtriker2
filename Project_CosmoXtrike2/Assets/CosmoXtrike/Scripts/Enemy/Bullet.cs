using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    protected float speed = 1.0f;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
           
    }

    private void FixedUpdate(){
        Fright();
    }

    virtual protected void Fright() {

        this.transform.Translate(transform.forward * speed);

    }
    
}
