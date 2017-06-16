// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Parabole/Unlit/Car"
{
	Properties
	{
		_MainTex ("Base", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" "BW"="True" }
		
		Pass
		{
//			Name "FORWARD"
//			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM
				#include "UnityCG.cginc"
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile LIGHTMAP_ON LIGHTMAP_OFF
				 
				struct v2f{
					//half4 color : COLOR;
					float4 pos : SV_POSITION;
					fixed2 uv : TEXCOORD0;
					#ifdef LIGHTMAP_ON
					fixed2  uv2 : TEXCOORD1;
					#endif
				};
				
				sampler2D _MainTex;
				fixed4 _MainTex_ST;
				
				#ifdef LIGHTMAP_ON
				fixed4 unity_LightmapST;
				// sampler2D unity_Lightmap;
				#endif
				
				v2f vert(appdata_full v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					
					#ifdef LIGHTMAP_ON
					o.uv2 = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
//					#else
//					fixed3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);
//					o.vlight = ShadeSH9 (float4(worldN,1.0));
					#endif
					
					//o.color = v.color;
					return o;
				}
				fixed4 frag(v2f i) : COLOR
				{
					fixed4 c = tex2D(_MainTex, i.uv);// * i.color;
					
					#ifdef LIGHTMAP_ON
					c.rgb *= DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv2));
//					#else
//					c.rgb *= i.vlight;
					#endif
					
					return c;
				}
			ENDCG
		}
	}
	fallback "Mobile/Unlit (Supports Lightmap)"
}