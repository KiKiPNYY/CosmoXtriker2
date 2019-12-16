using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] private float m_fadeTime = 1f;
    [SerializeField] [Range(0, 1)] private float m_changeFadeTime = 0.6f;
    [SerializeField] private Text m_text;

    private float m_timer;
    private bool m_isChange = false;

    private void ChangeScene()
    {
        if (m_isChange) { return; }
        if (Input.GetButtonDown("LeftTrigger") || Input.GetButtonDown("RightTrigger") || Input.GetKeyDown(KeyCode.Space))
        {
            m_isChange = true;
            m_timer = 0;
            SceneLoadManager.Instnce.LoadScene("Game");
        }
    }

    private void TextUpdate()
    {
        
        if (m_isChange)
        {
            m_timer += Time.deltaTime / m_changeFadeTime;
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, Mathf.Round(Mathf.Abs(Mathf.Sin(m_timer))));
            return;
        }
        m_timer += Time.deltaTime / m_fadeTime;
        m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, Mathf.Abs(Mathf.Sin(m_timer)));

    }

    void Start()
    {
        m_isChange = false;
    }

    void Update()
    {
        TextUpdate();
        ChangeScene();
    }
}
