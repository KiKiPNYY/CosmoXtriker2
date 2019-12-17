using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Vector3 p1 = transform.position;
        float distanceToObstacle = 0;
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(p1, 10, transform.forward, out hit, 10,layerMask))
        {
            distanceToObstacle = hit.distance;
            Debug.Log(distanceToObstacle);
        }
    }
}
