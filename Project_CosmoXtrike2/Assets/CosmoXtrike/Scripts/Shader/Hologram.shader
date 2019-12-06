Shader "Unlit/Hologram"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		[HDR]_Emission("Emission", Color) = (0.0, 0.0, 0.0, 0.0)

		_Speed("Scroll Speed", float) = .5
		_Space("Space", Range(0,1)) = .1
		_Division("Division Count", float) = 150
	}
	SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			fixed3 worldPos;
			fixed3 viewDir;
		};

		fixed4 _Color;
		fixed4 _Emission;
		fixed _Speed;
		fixed _Space;
		fixed _Division;

		void surf(Input IN, inout SurfaceOutputStandard o) 
		{
			clip(frac((IN.worldPos.y + _Time.r * _Speed) * _Division) - _Space);
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Emission = _Emission * (1.0 - saturate(dot(normalize(IN.viewDir), o.Normal)));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
