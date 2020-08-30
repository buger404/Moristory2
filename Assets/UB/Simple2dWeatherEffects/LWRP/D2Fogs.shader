// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/UB/Simple2dWeatherEffects/LWRP/D2Fogs" {

    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        //float _Blend;
        float _Size = 2.0;
        float _CameraSpeedMultiplier = 1.0;
        float _UVChangeX = 1.0;
        float _UVChangeY = 1.0;
        float _Speed = 0.2;
        float _VSpeed = 0;
        float _Density = 1;
        //_MainTex("Base (RGB)", 2D) = "white" {}
        float _DarkMode = 0;
        float _DarkMultiplier = 1;
        half4 _Color = (1, 1, 1, 1);
        
        float hash(float n) 
        { 
            return frac(sin(n)*753.5453123); 
        }

        float noise(in float3 x)
        {
            float3 p = floor(x);
            float3 f = frac(x);
            f = f*f*(3.0 - 2.0*f);

            float n = p.x + p.y*157.0 + 113.0*p.z;
            return lerp(
                        lerp(
                            lerp(hash(n + 0.0),   hash(n + 1.0),   f.x),
                            lerp(hash(n + 157.0), hash(n + 158.0), f.x), 
                            f.y),
                        lerp(
                            lerp(hash(n + 113.0), hash(n + 114.0), f.x),
                            lerp(hash(n + 270.0), hash(n + 271.0), f.x), 
                            f.y),
                        f.z);
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
                color += noise(float3((uv.x * _Size + direction * (i+1.0)*0.2) * k, 
                              (uv.y * _Size + Vdirection * (i + 1.0)*0.2) * k,
                              0.0)) 
                              / k; 
                total += 1.0/k;
            }
            color /= total;
            
            return clamp(color, 0.0, 1.0);

        }

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float2 fogUV = float2 (i.texcoord.x + _UVChangeX*_CameraSpeedMultiplier, i.texcoord.y + _UVChangeY*_CameraSpeedMultiplier);
            float f = fog(fogUV);
            float m = min(f*_Density, 1.);
            
            float4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            
            if(_DarkMode==1){ 
                half lum = tex.r*.3 + tex.g*.59 + tex.b*.11;
                //return tex*(1-_DarkMultiplier) + lum*m*_Color;
                //return tex*(1-lum) + lum*m*_Color*_DarkMultiplier;
                //return (tex*(1-m) + m*_Color)*tex*_DarkMultiplier;
                //float light = lum*m*_DarkMultiplier;
                //if(light >= 0.)
                //    light = lum*m;
                //else
                    //light = lum*m*_DarkMultiplier;
                return tex*(1-m*_Color.a-_DarkMultiplier) + m*_Color.a*(_Color+_DarkMultiplier);
            } 
            else{
                return tex*(1-m*_Color.a) + m*_Color.a*_Color;
            }
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}