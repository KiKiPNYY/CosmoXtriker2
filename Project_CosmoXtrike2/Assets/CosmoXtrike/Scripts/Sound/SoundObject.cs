using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private float m_aoudioLength = 0;
    private AudioSource m_audioSource = null;
    public SoundObject(AudioSource m_audioSource)
    {
        m_aoudioLength = 0;
    }

}
