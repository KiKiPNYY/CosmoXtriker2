using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CreateScriptable/Create WaveData")]
public class WaveData : ScriptableObject
{
    public Wave[] waves;
}

[System.Serializable]
public struct Wave
{
    //  生成するオブジェクト
    public GameObject spawnObject;
    //  ウェーブのタイム
    public float waveTime;
}