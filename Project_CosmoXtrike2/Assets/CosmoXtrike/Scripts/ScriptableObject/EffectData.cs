using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create EffectData")]
public class EffectData : ScriptableObject
{
    [SerializeField] private float m_activeTime = 0;
    [SerializeField] private Vector3 m_activeOffset = Vector3.zero;
    [SerializeField] private bool m_rotationStandardWorld = false;
    [SerializeField] private Vector3 m_activeRotation = Vector3.zero;

    [SerializeField] private bool m_parent = false;

    public float ActiveTime { get => m_activeTime; }
    public Vector3 ActiveOffset { get => m_activeOffset; }
    public bool RotationStandardWorld { get => m_rotationStandardWorld; }
    public Vector3 ActiveRotation { get => m_activeRotation; }

    public bool SetParent{get => m_parent;}
}
