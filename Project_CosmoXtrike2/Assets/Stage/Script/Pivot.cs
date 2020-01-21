using System.Collections;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public float pivotSize = 0.3f;

    // Start is called before the first frame update
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pivotSize);
    }
}