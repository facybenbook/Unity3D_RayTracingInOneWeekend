Shader "Hidden/BlendAccum"
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
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float uWidth;
			float uHeight;
			float uSampleCount;
			
			sampler2D _MainTex;

			struct RayTracerLayout
			{
				float4 accumColor;
			};
			StructuredBuffer<RayTracerLayout> uRayTraceLayout;

			float4 frag (v2f i) : SV_Target
			{
				float3 col = float3(0., 0., 0.);
				
				uint j;
				for (j = 0; j < uSampleCount; j++)
				{
					uint bufferIndex = uint(i.uv.x * uWidth)
						+ uint(i.uv.y * uHeight) * uint(uWidth)
						+ j * uint(uHeight) * uint(uWidth);
					
					col += uRayTraceLayout[bufferIndex].accumColor.rgb;
				}
				col /= uSampleCount;
				
				return float4(col, 1.);
			}
			ENDCG
		}
	}
}
