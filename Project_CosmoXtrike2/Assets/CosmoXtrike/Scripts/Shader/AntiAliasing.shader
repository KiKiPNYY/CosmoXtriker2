Shader "Unlit/AntiAliasing"
{
    Properties
    {
		_Radius("Radius",float) = 0.1
		_AntiAliasing("AntiAliasing", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

			half _Radius;
			half _AntiAliasing;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed r = distance(i.uv, fixed2(0.5,0.5));
                return smoothstep(_Radius + _AntiAliasing,_Radius, r);
            }
            ENDCG
        }
    }
}
