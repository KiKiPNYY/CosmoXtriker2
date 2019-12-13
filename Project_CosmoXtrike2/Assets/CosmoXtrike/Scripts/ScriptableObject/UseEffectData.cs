using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/Create UseEffectData")]
public class UseEffectData : ScriptableObject
{
    [SerializeField] private Effect[] m_effects = null;

    public Effect[] Effects { get => m_effects; }
}
