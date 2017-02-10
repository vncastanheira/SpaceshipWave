Shader "Vinicius/ScrollingVisibleOnIluminated" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Transparency("Global Transparency", Range(0.0, 1.0)) = 1.0
		_ScrollXSpeed ("X Scroll Speed", Range(0,100)) = 2
		_ScrollYSpeed ("Y Scroll Speed", Range(0,100)) = 2
	}
	SubShader {
		Tags 
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		LOD 200
		Cull Back
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf SimpleLambert alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;

		half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			half4 c;
			half4 result;
			result.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
			if (NdotL > 0.7) {
				c.rgb = result.rgb;
				c.a = s.Alpha;			
			}
			else {
				c.rgb = 0;
				c.a = 0;
			}
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		float _Transparency;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) 
		{
			// Create a separate variable to store our UVs
			// before we pass them to the text2d() function
			fixed2 scrolledUV = IN.uv_MainTex;
				
			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;

			// Apply the final UV offset
			scrolledUV += fixed2(xScrollValue, yScrollValue);

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, scrolledUV) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a * _Transparency;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
