Shader "Custom/HeightDependentTint"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_HeightMin("Height Min", Float) = -1
		_HeightMax("Height Max", Float) = 1
		_ColorMin("Tint Color At Min", Color) = (0,0,0,1)
		_ColorMax("Tint Color At Max", Color) = (1,1,1,1)
	}
	SubShader
    {
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		fixed4 _ColorMin;
		fixed4 _ColorMax;
		float _HeightMin;
		float _HeightMax;

		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
			float3 localPos;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.localPos = v.vertex.xyz;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);

			// use world y-axis https://answers.unity.com/questions/561900/get-local-position-in-surface-shader.html <= https://answers.unity.com/questions/882134/shader-based-on-vertex-height.html
			// float3 localPos = IN.worldPos - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;
			// float h = (_HeightMax - localPos.y) / (_HeightMax - _HeightMin);

			// use local y-axis
			float h = (_HeightMax - IN.localPos.y) / (_HeightMax - _HeightMin);
			fixed4 tintColor = lerp(_ColorMax.rgba, _ColorMin.rgba, h);
			o.Albedo = c.rgb * tintColor.rgb;
			o.Alpha = c.a * tintColor.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}
