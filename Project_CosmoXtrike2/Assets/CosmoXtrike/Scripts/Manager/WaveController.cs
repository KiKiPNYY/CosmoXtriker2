using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    private FlagManager flagManager;
    [SerializeField]
    private WaveData data;
    [SerializeField]
    private Transform waveSpawnPos = default;

    IEnumerator spawnCorutine;

    void Start()
    {
        spawnCorutine = WaveSpawning();
    }

    void Update()
    {
        if (flagManager.IsPlaying && Input.GetKeyDown(KeyCode.A))
        {
            //  敵の生成
            StartCoroutine(spawnCorutine);
        }

        if (!flagManager.IsPlaying || Input.GetKeyDown(KeyCode.B))
        {
            StopCoroutine(spawnCorutine);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopAllCoroutines();
            spawnCorutine = null;
            spawnCorutine = WaveSpawning();
            StartCoroutine(spawnCorutine);
        }
    }

    IEnumerator WaveSpawning()
    {
        Debug.Log("start");
        string s = System.DateTime.Now.Second.ToString();
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1.0f);
            Debug.Log(s + " : Count : " + i);
        }
        Debug.Log("finish");
        yield break;
    }
}
