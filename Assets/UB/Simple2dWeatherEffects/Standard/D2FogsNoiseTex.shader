// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UB/Simple2dWeatherEffects/Standard/D2FogsNoiseTex" {
    Properties{
        [HideInInspector]_CameraSpeedMultiplier("Camera Speed Multiplier", float) = 1.0
        [HideInInspector]_UVChangeX("UV Change X", float) = 1.0
        [HideInInspector]_UVChangeY("UV Change Y", float) = 1.0
        [HideInInspector]_Size("Size", float) = 2.0
		[HideInInspector]_Speed("Horizontal Speed", float) = 0.2
		[HideInInspector]_VSpeed("Vertical Speed", float) = 0
        [HideInInspector]_Density("Density", float) = 1
        [HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
        [HideInInspector]_Color("Color", Color) = (1, 1, 1, 1)
        [HideInInspector]_DarkMode("Dark Mode", float) = 0
        [HideInInspector]_DarkMultiplier("Dark Multiplier", float) = 1
		[HideInInspector]_NoiseTex("Noise", 2D) = "white" {}
    }

    Subshader{

        Pass{
            Tags{ "Queue" = "Opaque" }
            Cull Off ZWrite Off ZTest Always
            //Tags{ "Queue" = "Opaque" }

            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
            float _Size;
            float _CameraSpeedMultiplier;
            float _UVChangeX;
            float _UVChangeY;
            float _Speed;
			float _VSpeed;
            float _Density;
            float4 _Color;
            float _DarkMode;
            float _DarkMultiplier;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertexOutput {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            vertexOutput vert(appdata v)
            {
                vertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			float texNoise(float2 uv) {
				return tex2D(_NoiseTex, uv.xy).r;
			}
   
            float fog(in float2 uv)
            {
				float direction = _Time.y * _Speed;
				float Vdirection = _Time.y * _VSpeed;
                float color = 0.0;
                float total = 0.0;
                float k = 0.0;

                for (float i=0; i<6; i++)
                {
					k = pow(2.0, i);
					color += texNoise(
										float2(
												(uv.x * _Size + direction * (i + 1.0)*0.2) * k,
												(uv.y * _Size + Vdirection * (i + 1.0)*0.2) * k
											  )
					                 ) / k;
                    total += 1.0/k;
                }
                color /= total;
                
                return clamp(color, 0.0, 1.0);

            }

            half4 frag(vertexOutput i) : SV_Target
            {
                float2 fogUV = float2 (i.uv.x + _UVChangeX*_CameraSpeedMultiplier, i.uv.y + _UVChangeY*_CameraSpeedMultiplier);
                float f = fog(fogUV);
                float m = min(f*_Density, 1.);
                
                float4 tex = tex2D(_MainTex, i.uv);
                
                if(_DarkMode==1){ 
                    half lum = tex.r*.3 + tex.g*.59 + tex.b*.11;
                    //if(lum<0.2)
                    //    return tex;
                    //else
                    //return tex*(1-lum) + lum*m*_Color*_DarkMultiplier;//0*(1-lum);
                    //float4 color = (tex*(1-m) + m*_Color)*tex;
                    return tex*(1-m-_DarkMultiplier) + m*(_Color+_DarkMultiplier);
                } 
                else{
                    return tex*(1-m) + m*_Color;
                }
            }
            ENDCG
        }
    }
}