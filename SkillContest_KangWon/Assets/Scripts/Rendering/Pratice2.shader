Shader "Custom/Pratice2"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Threshold("_Threshold", Range(0, 1)) = 0
		_Intensity("Intensity", Range(0, 1)) = 0

	}
		SubShader
		{
			Tags { "Queue" = "Transparent" }
			Pass
			{
				CGPROGRAM

					#pragma vertex vert
					#pragma fragment frag
					#include "UnityCG.cginc"


							struct appdata_t {
								float4 vertex : POSITION;
								float2 uv : TEXCOORD0;
								};

							struct v2f {
								float4 vertex : SV_POSITION;
								float2 uv : TEXCOORD0;
								};

							sampler2D _MainTex;
							float _Threshold;
							float _Intensity;

							v2f vert(appdata_t v) {
								v2f t;
								t.vertex = UnityObjectToClipPos(v.vertex);
								t.uv = v.uv;
								return t;
							}

							half4 frag(v2f i) : SV_Target{
								half4 originalColor = tex2D(_MainTex, i.uv);
								half bright = (originalColor.a + originalColor.g + originalColor.b) / 3.0f;

								if (bright < _Threshold) {
									return originalColor;
								}

								half4 bloom = 0;
								for (int x = -1; x <= 1; x++)
								{
									for (int y = -1; y <= 1; y++)
									{
										bloom += tex2D(_MainTex, i.uv + float2(x, y) * 0.005) * _Intensity * 0.05;
									}
								}
								return originalColor + bloom;
							}


							ENDCG
		}
		}
}