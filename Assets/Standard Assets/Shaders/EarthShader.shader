Shader "Custom/EarthShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass{
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			
			struct VertexInput {
				float4 position : POSITION;
				float4 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct VertexOutput {
				float4 position : SV_POSITION;
				
				//yep. that is self explooni torrie.
				float4 positionInWorld;
				//pull through NORMAL
				float3 normal;
				//pull through TEXCOORD0
				float4 tex;
			};
			
			VertexOutput vert(VertexInput i) {
				VertexOutput o;
				o.normal = i.normal;
				o.tex = i.texcoord;
				o.positionInWorld = mul(_World2Object, i.position);
				o.position = mul(UNITY_MATRIX_MVP, i.position);
				return o;
			}
			
			float4 frag(VertexOutput o) : COLOR {
				float4 color = tex2D(_MainTex, o.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				
				//_WorldSpaceLightPos0
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float intensity = dot(lightDirection.xyz, o.normal.xyz);
				intensity = pow(intensity, 0.3f) * (intensity/abs(intensity));
				
				return float4(intensity * color.xyz, 1.0);
			}
			
			ENDCG
		}
	}
}
