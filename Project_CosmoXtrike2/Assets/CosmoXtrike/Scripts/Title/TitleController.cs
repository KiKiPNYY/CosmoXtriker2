using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private bool isChange = false;
    
    void Start()
    {
        isChange = false;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneChangeGame();
        }
    }

    void SceneChangeGame()
    {
        if (!isChange)
        {
            isChange = true;
            SceneManager.LoadScene("Game");
        }
    }
}
