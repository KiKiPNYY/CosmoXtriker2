using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    #region グローバル変数
    [SerializeField]
    private bool isPlaying;
    [SerializeField]
    private bool isEventing;
    [SerializeField]
    private bool isFadeing;

    [SerializeField]
    private ShipController shipController;

    public bool IsPlaying
    {
        get => isPlaying;
        set => isPlaying = value;
    }
    #endregion



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
