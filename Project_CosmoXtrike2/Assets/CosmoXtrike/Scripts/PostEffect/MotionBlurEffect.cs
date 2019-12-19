using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionBlurEffect : MonoBehaviour
{
    #region シングルトン
    private static MotionBlurEffect m_instance = null;

    public static MotionBlurEffect Instance
    {
        get
        {
            return m_instance;
        }
    }

    private void CreateInstance()
    {
        if (m_instance == null)
        {
            m_instance = this;
            if (m_instance == null)
            {
                Debug.LogError("PlayerManagerがありません");
            }

        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }

    #endregion

    [SerializeField, Range(0.0f, 0.95f)] private float m_blurAmount = 0.8f;

    [SerializeField] Shader m_curShader;

    private Material m_curMaterial;
    private RenderTexture m_tempRT;

    private float m_effectMagnification = 0;

    public float EffectMagnification { set => m_effectMagnification = value; }

    private void Awake()
    {
        CreateInstance();
    }

    Material material
    {

        get
        {
            if (m_curMaterial == null)
            {
                m_curMaterial = new Material(m_curShader);
                m_curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_curMaterial;
        }
    }


    void OnDisable()
    {
        if (m_curMaterial)
        {
            DestroyImmediate(m_curMaterial);
        }
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (m_curShader != null)
        {

            if (m_tempRT == null || m_tempRT.width != source.width || m_tempRT.height != source.height)
            {
                DestroyImmediate(m_tempRT);
                m_tempRT = new RenderTexture(source.width, source.height, 0);
                m_tempRT.hideFlags = HideFlags.HideAndDontSave;
                Graphics.Blit(source, m_tempRT);
            }


            //RenderTexture blurBuffer = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
            //m_tempRT.MarkRestoreExpected();
            //Graphics.Blit(m_tempRT, blurBuffer);
            //Graphics.Blit(blurBuffer, m_tempRT);

            //RenderTexture.ReleaseTemporary(blurBuffer);


            material.SetTexture("_MainTex", m_tempRT);
            material.SetFloat("_BlurAmount", 1 - (m_blurAmount * m_effectMagnification));


            Graphics.Blit(source, m_tempRT, material);
            Graphics.Blit(m_tempRT, destination);
        }
        else
        {

            Graphics.Blit(source, destination);
        }

    }
}
