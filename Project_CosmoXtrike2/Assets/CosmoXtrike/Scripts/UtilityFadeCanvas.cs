using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UtilityFadeCanvas : MonoBehaviour
{
    Canvas faceCanvas;

    Image facePanel;

    Camera faceCamera;

    Coroutine coroutine;

    GameObject camera_G;

    [Tooltip("フェードする色")]
    public Color panelColor = new Color(0, 0, 0, 0);

    [Tooltip("フェードの時間")]
    public float fadeTime = 1;

    [Tooltip("0～1の値で透明度を設定")]
    public float alpha_Panel = 0.5f;

    const float FIXED_UPDATE_DELTATIME = 0.02f;

    float startTime;

    void Awake()
    {
        //シーンをロードするたびに新しいカメラを生成
        if (GameObject.Find("OnlyUIRenderingCamera"))
        {
            Destroy(GameObject.Find("OnlyUIRenderingCamera"));
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        //カメラ自動生成
        camera_G = new GameObject("OnlyUIRenderingCamera");
        camera_G.transform.parent = gameObject.transform;
        faceCamera = camera_G.AddComponent<Camera>();
        faceCamera.clearFlags = CameraClearFlags.Depth;
        faceCamera.cullingMask = (1 << LayerMask.NameToLayer("UI"));

        //キャンバス生成＆設定
        GameObject canvas_G = new GameObject("FaceCanvas");
        canvas_G.transform.parent = gameObject.transform;
        faceCanvas = canvas_G.AddComponent<Canvas>();
        canvas_G.AddComponent<CanvasRenderer>();


        //キャンバスのポジションを調整
        Vector3 canvasPosition = canvas_G.transform.position;
        canvasPosition.x = 0;
        canvasPosition.y = 0;
        canvasPosition.z = 0.1f;
        canvas_G.transform.localPosition = canvasPosition;

        //レンダリングをfaceCameraに
        faceCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        faceCanvas.worldCamera = faceCamera;

        //パネル生成＆設定
        GameObject panel_G = new GameObject("FacePanel");
        facePanel = panel_G.AddComponent<Image>();

        Color tmpColor = facePanel.color;
        tmpColor.a = 0f;
        facePanel.color = tmpColor;

        //パネルをキャンバスの子に設定
        panel_G.transform.parent = canvas_G.transform;

        //パネルのポジションを正面に調整
        Vector3 panelPosition = panel_G.transform.localPosition;
        panelPosition.x = 0;
        panelPosition.y = 0;
        panelPosition.z = 0;
        panel_G.transform.localPosition = panelPosition;


        //キャンバスをカメラの子に設定
        canvas_G.transform.parent = faceCamera.transform;

        //Layerを変更
        canvas_G.layer = LayerMask.NameToLayer("UI");
        panel_G.layer = LayerMask.NameToLayer("UI");

    }

    void Update()
    {
        //Fixed Timestepを固定
        Time.fixedDeltaTime = FIXED_UPDATE_DELTATIME;

    }

    void FixedUpdate()
    {
        //キー押してない間はreturn
        if (Input.anyKey == false)
        {
            return;
        }

        if (coroutine == null)
        {
            //テスト用　フェードアウト
            if (Input.GetKeyDown(KeyCode.O) && panelColor.a == 0)
            {
                FadeOut();
            }

            if (Input.GetKeyDown(KeyCode.I) && panelColor.a == alpha_Panel)
            {
                FadeIn();
            }
        }
    }


    public void FadeOut()
    {
        if (panelColor.a == 0)
        {
            //スタートの時間記録
            startTime = Time.time;
            print("フェードアウト開始");

            coroutine = StartCoroutine(FadeOutCoroutine());
        }
    }


    public void FadeIn()
    {
        if (panelColor.a == alpha_Panel)
        {
            //スタートの時間記録
            startTime = Time.time;
            print("フェードイン停止");

            coroutine = StartCoroutine(FadeInCoroutine());
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        int count = 0;

        yield return new WaitForFixedUpdate();
        while (facePanel.color.a < alpha_Panel - 0.00005f)
        {
            yield return new WaitForFixedUpdate();
            panelColor.a += alpha_Panel / (fadeTime * 50);
            facePanel.color = panelColor;

            count++;

            //フェード中の時間、Alphaを確認
            print(Time.time - startTime);
            print("アルファの値:" + panelColor.a + ":" + count + "回目");

        }
        print("フェードアウト停止");
        panelColor.a = alpha_Panel;

        StopCoroutine(coroutine);
        coroutine = null;
    }


    IEnumerator FadeInCoroutine()
    {
        int count = 0;

        yield return new WaitForFixedUpdate();
        while (panelColor.a > 0 + 0.00005f)
        {
            yield return new WaitForFixedUpdate();
            panelColor.a -= alpha_Panel / (fadeTime * 50);
            facePanel.color = panelColor;

            count++;

            //フェード中の時間、Alphaを確認
            print(Time.time - startTime);
            print("アルファの値:" + panelColor.a + ":" + count + "回目");

        }
        print("フェードイン停止");
        panelColor.a = 0;

        StopCoroutine(coroutine);
        coroutine = null;
    }
}