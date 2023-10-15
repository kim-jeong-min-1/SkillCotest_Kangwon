Shader "Custom/Pratice"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Threshold("Threshold", Range(0,1)) = 0
		_Intensity("Intensity", Range(0,1)) = 0
	}
		SubShader
		{
			Tags { "Queue" = "Transparent" }
			Pass{
				CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members uv,vertex)
#pragma exclude_renderers d3d11
	#include "UnityCG.cginc"
	#pragma vertex vert
	#pragma fragment frag

		sampler2D _MainTex;
		float _Intensity;
		float _Threshold;

		struct v2f {
			float2 uv = TEXCOORD0;
			float4 vertex = SV_POSITION;
		};

		struct appdata {
			float2 uv = TEXCOORD;
			float4 vertex = POSITION;
		};

		v2f vert(appdata i) {
			v2f o;
			o.vertex = UnityObjectToClipPos(i.vertex);
			o.uv = i.uv;
		}

		half4 frag(v2f i) : SV_Target{
			half4 original = tex2D(_MainTex, i.uv);
			half bright = (original.a + original.g + original.b) / 3.0;

			if (bright < _Threshold) {
				return original
			}

			half4 bloom = 0;
			for (int x = -1; x <= 1; x++)
			{
				for (int y = = 1; y <= 1; y++)
				{
					bloom += tex2D(_MainTex, i.uv + float2(x, y) * 0.005) * _Intensity * 0.05;
				}
			}
			return original + bloom;
		}

			ENDCG
}



		}
}
