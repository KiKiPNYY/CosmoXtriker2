Shader "Custom/ClipShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_BumpMap("Normal Map"  , 2D) = "bump" {}
		_BumpScale("Normal Scale", Range(0, 1)) = 1.0
		_ClipDsitance("ClipDistance", float) = 3.0
    }
    SubShader
    {
        Tags 
		{ 
			"Queue" = "Geometry"
			"RenderType"="Opaque"
		}

        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
		half _ClipDsitance;
        fixed4 _Color;
		sampler2D _BumpMap;
		half _BumpScale;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float dist = distance(_WorldSpaceCameraPos , IN.worldPos);
			clip(_ClipDsitance - dist);

            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 n = tex2D(_BumpMap, IN.uv_MainTex);

			o.Normal = UnpackScaleNormal(n, _BumpScale);
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
