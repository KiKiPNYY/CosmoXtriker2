using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class AntiAliasingCreate
{
    [MenuItem("Assets/Create AntiAliasing Texture")]
    private static void AntiAliasing()
    {
        // RenderTextureにUnityのAntiAliasingテクスチャをレンダリングする
        var rt = RenderTexture.GetTemporary(512, 512);
        Graphics.Blit(rt, rt, Resources.Load("ShaderMaterial/AntiAliasing_Mt", typeof(Material)) as Material);
        
        // RenderTextureからテクスチャを作る
        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;
        var texture = new Texture2D(rt.width, rt.height);
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();

        // テクスチャを保存する
        var bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/CosmoXtrike/Textures/ShaderTexture/AntiAliasing_tex.png", bytes);
        AssetDatabase.Refresh();

        // 元に戻す
        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(rt);
    }
}

#endif