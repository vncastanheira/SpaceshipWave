Shader "Custom/VHS"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
				
			sampler2D _MainTex;
			float _Magnitude;

			fixed4 frag (v2f i) : SV_Target
			{				
				float2 uv = i.uv;					
				float4 c = tex2D(_MainTex, uv);
				float higher = c.r > c.b ? c.r : c.g > c.b ? c.g : c.b; 
				return float4(higher, higher, higher, 0);
			}
			ENDCG
		}
	}
}
