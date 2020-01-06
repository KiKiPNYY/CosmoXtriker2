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
			  uniform float _BlurMinRange;
			  uniform float _BlurMaxRange;
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
				  fixed2 setTex = i.texcoord;
				  setTex -= fixed2(0.5, 0.5);
				  setTex.x *= 16.0 / 9.0;
				  float dist = distance(setTex, fixed2(0, 0));
				  float magnification = 1 - (smoothstep(_BlurMinRange, _BlurMaxRange, dist));
				  float maxVal = 1 - _BlurAmount;
				  return half4(tex2D(_MainTex, i.texcoord).rgb, _BlurAmount + (maxVal * magnification));
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
