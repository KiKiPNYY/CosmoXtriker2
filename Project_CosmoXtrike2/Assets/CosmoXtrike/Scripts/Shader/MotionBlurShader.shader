Shader "Unlit/MotionBlurShader"
{
	SubShader{

		  ZTest Always Cull Off ZWrite Off

		  Pass {

			  Blend SrcAlpha OneMinusSrcAlpha

			  ColorMask RGB

			  BindChannels {
				  Bind "vertex", vertex
				  Bind "texcoord", texcoord
			  }

			  CGPROGRAM

			  #pragma vertex vert
			  #pragma fragment frag

			  #include "UnityCG.cginc"


			  struct appdata_t {
				  float4 vertex : POSITION;
				  float2 texcoord : TEXCOORD;
			  };


			  struct v2f {
				  float4 vertex : SV_POSITION;
				  float2 texcoord : TEXCOORD;
			  };

			  float4 _MainTex_ST;
			  uniform float _BlurAmount;
			  uniform sampler2D _MainTex;

			  v2f vert(appdata_t v)
			  {

				  v2f o;

				  o.vertex = UnityObjectToClipPos(v.vertex);

				  o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				  return o;
			  }


			  half4 frag(v2f i) : SV_Target
			  {
				  return half4(tex2D(_MainTex, i.texcoord).rgb, _BlurAmount);
			  }
			  ENDCG
		  }


		  Pass {

			  Blend One Zero

			  ColorMask A

			  BindChannels {
				  Bind "vertex", vertex
				  Bind "texcoord", texcoord
			  }

			  CGPROGRAM
			  #pragma vertex vert
			  #pragma fragment frag

			  #include "UnityCG.cginc"

			  struct appdata_t {
				  float4 vertex : POSITION;
				  float2 texcoord : TEXCOORD;
			  };

			  struct v2f {
				  float4 vertex : SV_POSITION;
				  float2 texcoord : TEXCOORD;
			  };

			  float4 _MainTex_ST;

			  v2f vert(appdata_t v)
			  {
				  v2f o;
				  o.vertex = UnityObjectToClipPos(v.vertex);
				  o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				  return o;
			  }

			  sampler2D _MainTex;

			  half4 frag(v2f i) : SV_Target
			  {
				  return tex2D(_MainTex, i.texcoord);
			  }

			  ENDCG
		  }

	}
}
