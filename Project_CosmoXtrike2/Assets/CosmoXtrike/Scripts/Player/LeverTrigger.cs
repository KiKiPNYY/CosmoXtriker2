using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager playerManager = other.GetComponent<PlayerManager>();
        if(playerManager == null) { return; }
        playerManager.LeverOperation = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        PlayerManager playerManager = collision.transform.GetComponent<PlayerManager>();
        if (playerManager == null) { return; }
        playerManager.LeverOperation = true;
    }
}
